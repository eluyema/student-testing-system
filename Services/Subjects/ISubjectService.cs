using student_testing_system.Models.Subjects;
using student_testing_system.Models.Tests;
using student_testing_system.Services.Subjects.DTOs;

namespace student_testing_system.Services.Subjects
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDTO>> GetAllSubjectsAsync();
        Task<SubjectDTO> GetSubjectByIdAsync(Guid id);
        Task<SubjectDTO> CreateSubjectAsync(CreateSubjectDTO createSubjectDto);
        Task UpdateSubjectAsync(Guid id, UpdateSubjectDTO updateSubjectDto);
        Task DeleteSubjectAsync(Guid id);
        Task AddTestToSubjectAsync(Guid subjectId, CreateInnerTestDTO testDto);
    }

}
