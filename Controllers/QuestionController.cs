using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_testing_system.Models.Answers;
using student_testing_system.Services.Questions;
using student_testing_system.Services.Questions.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace student_testing_system.Controllers
{
    [ApiController]
    [Route("api/v1/tests/{testId}/questions")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> CreateQuestion(Guid testId, [FromBody] CreateQuestionWithAnswersDTO createQuestionDto)
        {
            try
            {
                var createdQuestion = await _questionService.CreateQuestionWithAnswersAsync(testId, createQuestionDto);
                return CreatedAtAction(nameof(GetQuestionWithAnswers), new { testId = testId, questionId = createdQuestion.QuestionId }, createdQuestion);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionsByTestId(Guid testId)
        {
            try
            {
                var questionsListDto = await _questionService.GetQuestionsByTestIdAsync(testId);
                return Ok(questionsListDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{questionId}")]
        public async Task<IActionResult> GetQuestion(Guid testId, Guid questionId)
        {
            try
            {
                var question = await _questionService.GetQuestionByIdAsync(questionId);
                if (question == null)
                {
                    return NotFound($"Question with ID {questionId} not found.");
                }

                return Ok(question);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Question with ID {questionId} not found.");
            }
        }

        [HttpGet("{questionId}/answers")]
        public async Task<IActionResult> GetQuestionWithAnswers(Guid testId, Guid questionId)
        {
            try
            {
                var question = await _questionService.GetQuestionWithAnswersByIdAsync(questionId);
                if (question == null)
                {
                    return NotFound($"Question with ID {questionId} not found.");
                }

                return Ok(question);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Question with ID {questionId} not found.");
            }
        }
        [Authorize(Roles = "Teacher")]
        [HttpPut("{questionId}")]
        public async Task<IActionResult> UpdateQuestion(Guid testId, Guid questionId, [FromBody] UpdateQuestionDTO updateQuestionDto)
        {
            try
            {
                await _questionService.UpdateQuestionAsync(questionId, updateQuestionDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Question with ID {questionId} not found.");
            }
        }
        [Authorize(Roles = "Teacher")]
        [HttpDelete("{questionId}")]
        public async Task<IActionResult> DeleteQuestion(Guid testId, Guid questionId)
        {
            try
            {
                await _questionService.DeleteQuestionAsync(questionId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Question with ID {questionId} not found.");
            }
        }
        [Authorize(Roles = "Teacher")]
        [HttpPost("{questionId}/answers")]
        public async Task<IActionResult> AddAnswer(Guid testId, Guid questionId, [FromBody] CreateAnswerDTO createAnswerDto)
        {
            try
            {
                await _questionService.AddAnswerToQuestionAsync(questionId, createAnswerDto);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Question with ID {questionId} not found.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
