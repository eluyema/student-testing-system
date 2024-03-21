using student_testing_system.Services.Tests.DTOs;

namespace student_testing_system.Services.Tests
{
    public interface ITestService
    {
        Task<TestDTO> CreateTestAsync(CreateTestDTO createTestDto);
        Task<TestDTO> GetTestByIdAsync(Guid testId);
        Task UpdateTestAsync(Guid testId, UpdateTestDTO updateTestDto);
        Task DeleteTestAsync(Guid testId);

        Task<TestsListDTO> GetAllTestsBySubjectIdAsync(Guid subjectId);
    }
}
