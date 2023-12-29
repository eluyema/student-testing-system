using student_testing_system.Models;
using student_testing_system.Services.TestSessions;
using student_testing_system.Services.UserAnswers.DTOs;
using student_testing_system.Services.Answers;
using student_testing_system.Services.Questions;
using System;
using System.Threading.Tasks;

namespace student_testing_system.Services.UserAnswers
{
    public class UserAnswerService : IUserAnswerService
    {
        private readonly IUserAnswerRepository _userAnswerRepository;
        private readonly ITestSessionRepository _testSessionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;

        public UserAnswerService(
            IUserAnswerRepository userAnswerRepository,
            ITestSessionRepository testSessionRepository,
            IAnswerRepository answerRepository,
            IQuestionRepository questionRepository)
        {
            _userAnswerRepository = userAnswerRepository;
            _testSessionRepository = testSessionRepository;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
        }

        public async Task<bool> AddUserAnswer(CreateUserAnswerDTO dto, string userId)
        {
            var testSession = await _testSessionRepository.GetByIdAsync(dto.TestSessionId);
            if (testSession == null || testSession.UserId != userId)
            {
                throw new InvalidOperationException("Invalid test session or user.");
            }

            if (testSession.EndAt.HasValue && testSession.EndAt.Value < DateTime.UtcNow)
            {
                throw new InvalidOperationException("Cannot add an answer to a completed test session.");
            }

            var questionExistsInSession = await _questionRepository.ExistsInTestSession(dto.AssignedQuestionId, dto.TestSessionId);
            if (!questionExistsInSession)
            {
                throw new InvalidOperationException("The question is not part of the test session.");
            }

            var answerIsValid = await _answerRepository.IsValidAnswerForAssignedQuestion(dto.AssignedQuestionId, dto.AnswerId);
            if (!answerIsValid)
            {
                throw new InvalidOperationException("The provided answer is not valid for the specified question.");
            }

            var existingUserAnswer = await _userAnswerRepository.FindByAssignedQuestionIdAsync(dto.AssignedQuestionId);
            if (existingUserAnswer != null)
            {
                await _userAnswerRepository.DeleteAsync(existingUserAnswer.UserAnswerId);
            }

            var isAnswerCorrect = await _answerRepository.IsCorrectAnswer(dto.AnswerId);
            var userAnswer = new UserAnswer
            {
                TestSessionId = dto.TestSessionId,
                AssignedQuestionId = dto.AssignedQuestionId,
                AnswerId = dto.AnswerId,
                IsCorrect = isAnswerCorrect
            };

            await _userAnswerRepository.CreateAsync(userAnswer);

            return isAnswerCorrect;
        }

        public async Task<IEnumerable<UserAnswerDTO>> GetAllUserAnswersForTestSession(Guid testSessionId)
        {
            var userAnswers = await _userAnswerRepository.GetAllUserAnswersForTestSession(testSessionId);

            return userAnswers.Select(ua => new UserAnswerDTO
            {
                UserAnswerId = ua.UserAnswerId,
                TestSessionId = ua.TestSessionId,
                AssignedQuestionId = ua.AssignedQuestionId,
                AnswerId = ua.AnswerId,
                QuestionText = ua.AssignedQuestion.Question.Text,
                AnswerText = ua.Answer.Text
            });
        }



    }
}
