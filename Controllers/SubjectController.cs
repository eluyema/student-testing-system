﻿using Microsoft.AspNetCore.Mvc;
using student_testing_system.Services.Subjects;
using student_testing_system.Services.Subjects.DTOs;

namespace student_testing_system.Controllers
{
    [ApiController]
    [Route("api/v1/subject")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            return Ok(subjects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(Guid id)
        {
            try
            {
                var subject = await _subjectService.GetSubjectByIdAsync(id);
                return Ok(subject);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject(CreateSubjectDTO createSubjectDto)
        {
            var createdSubject = await _subjectService.CreateSubjectAsync(createSubjectDto);
            return CreatedAtAction(nameof(GetSubject), new { id = createdSubject.SubjectId }, createdSubject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(Guid id, UpdateSubjectDTO updateSubjectDto)
        {
            try
            {
                await _subjectService.UpdateSubjectAsync(id, updateSubjectDto);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            try
            {
                await _subjectService.DeleteSubjectAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("{subjectId}/tests")]
        public async Task<IActionResult> AddTestToSubject(Guid subjectId, [FromBody] CreateTestDTO createTestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _subjectService.AddTestToSubjectAsync(subjectId, createTestDto);
                return Ok("Test added to subject successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("with-tests")]
        public async Task<IActionResult> GetAllSubjectsWithTests()
        {
            var subjectsWithTests = await _subjectService.GetAllSubjectsWithTestsAsync();
            return Ok(subjectsWithTests);
        }
    }
}