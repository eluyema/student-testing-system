using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using student_testing_system.Models;
using student_testing_system.Services.TestSessions;
using student_testing_system.Services.TestSessions.DTOs;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace student_testing_system.Controllers
{
    [ApiController]
    [Route("api/v1/test-session")]
    public class TestSessionController : ControllerBase
    {
        private ITestSessionService _testSessionService;

        public TestSessionController(ITestSessionService testSessionService)
        {
            _testSessionService = testSessionService;
        }
        [Authorize(Roles = "Student")]
        [HttpPost("start")]
        public async Task<IActionResult> StartTestSession([FromBody] StartTestSessionDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var testId = dto.TestId;
                TestSessionDTO responseDto = await _testSessionService.StartTestSession(testId, userId);
                return Ok(responseDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Student")]
        [HttpGet("{testSessionId}/questions")]
        public async Task<IActionResult> GetAssignedQuestions(Guid testSessionId)
        {
            try
            {
                var questions = await _testSessionService.GetAssignedQuestionsAsync(testSessionId);
                if (questions == null)
                {
                    return NotFound($"No questions found for Test Session ID {testSessionId}.");
                }
                return Ok(questions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [Authorize(Roles = "Student")]
        [HttpGet("test/{testId}")]
        public async Task<IActionResult> GetMyTestSessionsByTestId(Guid testId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                TestSessionsListDTO dto = await _testSessionService.GetTestSessionsByUserAndTestIdAsync(userId, testId);
                return Ok(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Student")]
        [HttpGet("{testSessionId}")]
        public async Task<IActionResult> GetTestSession(Guid testSessionId)
        {
            try
            {
                var testSessionDto = await _testSessionService.GetTestSessionByIdAsync(testSessionId);
                return Ok(testSessionDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
