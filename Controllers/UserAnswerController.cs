using Microsoft.AspNetCore.Mvc;
using student_testing_system.Services.UserAnswers.DTOs;
using student_testing_system.Services.UserAnswers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace student_testing_system.Controllers
{
    [ApiController]
    [Route("api/v1/user-answers")]
    public class UserAnswerController : ControllerBase
    {
        private readonly IUserAnswerService _userAnswerService;

        public UserAnswerController(IUserAnswerService userAnswerService)
        {
            _userAnswerService = userAnswerService;
        }
        [Authorize(Roles = "Student")]
        [HttpPost("add")]
        public async Task<IActionResult> AddUserAnswer([FromBody] CreateUserAnswerDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = await _userAnswerService.AddUserAnswer(dto, userId);

                if (result)
                {
                    return Ok(new { message = "Answer added successfully." });
                }

                return BadRequest(new { message = "Failed to add answer." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Student")]
        [HttpGet("{testSessionId}/user-answers")]
        public async Task<IActionResult> GetAllUserAnswersForTestSession(Guid testSessionId)
        {
            var userAnswers = await _userAnswerService.GetAllUserAnswersForTestSession(testSessionId);
            return Ok(userAnswers);
        }
    }
}
