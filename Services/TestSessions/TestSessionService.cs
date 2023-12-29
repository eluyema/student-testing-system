using student_testing_system.Models;
using student_testing_system.Models.Tests;
using student_testing_system.Services.Questions;
using student_testing_system.Services.Tests;
using student_testing_system.Services.TestSessions.DTOs;
using student_testing_system.Services.UserAnswers;
using student_testing_system.Services.Users;

namespace student_testing_system.Services.TestSessions
{
    public class TestSessionService : ITestSessionService
    {
        private readonly ITestRepository _testRepository;
        private readonly ITestSessionRepository _testSessionRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserAnswerRepository _userAnswerRepository;

        public TestSessionService(ITestRepository testRepository,
            ITestSessionRepository testSessionRepository,
            IQuestionRepository questionRepository,
            IUserRepository userRepository,
            IUserAnswerRepository userAnswerRepository
            ) {
            _testRepository = testRepository;
            _testSessionRepository = testSessionRepository;
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _userAnswerRepository = userAnswerRepository;
        }

        private async Task<double> CalculateScore(Guid testSessionId)
        {
            var userAnswers = await _userAnswerRepository.GetByTestSessionIdAsync(testSessionId);
            var correctAnswersCount = userAnswers.Count(ua => ua.IsCorrect);

            var totalQuestions = await _testSessionRepository.GetTotalQuestionsCountAsync(testSessionId);

            if (totalQuestions > 0)
            {
                return (double)correctAnswersCount / totalQuestions * 100;
            }
            else
            {
                return 0;
            }
        }


        public async Task<List<QuestionDTO>> GetAssignedQuestionsAsync(Guid testSessionId)
        {
            var assignedQuestions = await _testSessionRepository.GetAssignedQuestionsAsync(testSessionId);
            var questionDtos = assignedQuestions.Select(aq => new QuestionDTO
            {
                AssignedQuestionId = aq.AssignedQuestionId,
                Text = aq.Question.Text,
                Answers = aq.Question.Answers.Select(ans => new AnswerDTO
                {
                    AnswerId = ans.AnswerId,
                    Text = ans.Text
                }).ToList()
            }).ToList();

            return questionDtos;
        }

        public async Task<TestSessionDTO> StartTestSession(Guid testId, string userId)
        {
            var userExists = await _userRepository.ExistsAsync(userId);
            if (!userExists)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            var test = await _testRepository.GetByIdAsync(testId);
            if (test == null)
            {
                throw new KeyNotFoundException($"Test with ID {testId} not found.");
            }

            int attemptsAmount = await _testSessionRepository.CountByUserIdAsync(userId);
            if (test.AllowedAttempts <= attemptsAmount)
            {
                throw new InvalidOperationException("You have exceeded your allowed attempts.");
            }

            int questionsAmount = await _questionRepository.CountQuestionsByTestIdAsync(testId);
            if (questionsAmount < test.QuestionsPerAttempt)
            {
                throw new InvalidOperationException("Unable to start the test due to insufficient questions.");
            }

            var now = DateTime.UtcNow;
            var testDurationInMinutes = test.MinutesDuration;

            TestSession newTestSession = new TestSession
            {
                StartAt = now,
                EndAt = now.AddMinutes(testDurationInMinutes),
                TestId = testId,
                UserId = userId
            };

            var selectedRandomQuestions = await _questionRepository.GetRandomQuestionsByTestIdAsync(testId, test.QuestionsPerAttempt);
            newTestSession.AssignedQuestions = selectedRandomQuestions.Select(q => new AssignedQuestion
            {
                QuestionId = q.QuestionId,
                TestSessionId = newTestSession.TestSessionId
            }).ToList();

            await _testSessionRepository.CreateAsync(newTestSession);

            var score = await CalculateScore(newTestSession.TestSessionId);
            var testSessionDto = new TestSessionDTO
            {
                TestSessionId = newTestSession.TestSessionId,
                TestId = newTestSession.TestId,
                UserId = newTestSession.UserId,
                StartAt = newTestSession.StartAt,
                EndAt = newTestSession.EndAt,
                IsCompleted = newTestSession.EndAt <= DateTime.UtcNow,
                Score = score
            };

            return testSessionDto;
        }

        public async Task<TestSessionsListDTO> GetTestSessionsByUserAndTestIdAsync(string userId, Guid testId)
        {
            var testSessions = await _testSessionRepository.GetByUserAndTestIdAsync(userId, testId);
            var now = DateTime.UtcNow;

            List<TestSessionDTO> testSessionDTOs = new List<TestSessionDTO>();
            foreach (var ts in testSessions)
            {
                var score = ts.EndAt <= DateTime.UtcNow ? await CalculateScore(ts.TestSessionId) : 0;
                testSessionDTOs.Add(new TestSessionDTO
                {
                    TestSessionId = ts.TestSessionId,
                    StartAt = ts.StartAt,
                    EndAt = ts.EndAt,
                    TestId = ts.TestId,
                    UserId = ts.UserId,
                    IsCompleted = ts.EndAt <= DateTime.UtcNow,
                    Score = score
                });
            }

            return new TestSessionsListDTO { TestSessions = testSessionDTOs };
        }

        public async Task<TestSessionDTO> GetTestSessionByIdAsync(Guid testSessionId)
        {
            var testSession = await _testSessionRepository.GetByIdAsync(testSessionId);
            if (testSession == null)
            {
                throw new KeyNotFoundException($"Test session with ID {testSessionId} not found.");
            }

            var score = testSession.EndAt <= DateTime.UtcNow ? await CalculateScore(testSessionId) : 0;

            return new TestSessionDTO
            {
                TestSessionId = testSession.TestSessionId,
                StartAt = testSession.StartAt,
                EndAt = testSession.EndAt,
                TestId = testSession.TestId,
                UserId = testSession.UserId,
                IsCompleted = testSession.EndAt <= DateTime.UtcNow,
                Score = score
            };
        }


    }
}
