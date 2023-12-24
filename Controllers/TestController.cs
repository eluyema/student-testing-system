using Microsoft.AspNetCore.Mvc;
using student_testing_system.Services.Tests.DTOs;
using student_testing_system.Services.Tests;
using student_testing_system.Services.Questions;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace student_testing_system.Controllers
{
    [ApiController]
    [Route("api/v1/tests")]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly IQuestionService _questionService;

        public TestController(ITestService testService, IQuestionService questionService)
        {
            _testService = testService;
            _questionService = questionService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTest(Guid id)
        {
            try
            {
                var testDto = await _testService.GetTestByIdAsync(id);
                return Ok(testDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize(Roles = "Teacher")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTest(Guid id, [FromBody] UpdateTestDTO updateTestDto)
        {
            try
            {
                await _testService.UpdateTestAsync(id, updateTestDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize(Roles = "Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(Guid id)
        {
            try
            {
                await _testService.DeleteTestAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
