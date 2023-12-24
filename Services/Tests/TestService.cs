using student_testing_system.Services.Tests.DTOs;
using student_testing_system.Models.Tests;

namespace student_testing_system.Services.Tests
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;

        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<TestDTO> CreateTestAsync(CreateTestDTO createTestDto)
        {
            var test = new Test
            {
                Title = createTestDto.Title,
                AllowedAttempts = createTestDto.AllowedAttempts,
                MinutesDuration = createTestDto.MinutesDuration,
                QuestionsPerAttempt = createTestDto.QuestionsPerAttempt
            };

            await _testRepository.CreateAsync(test);
            return MapToTestDTO(test);
        }

        public async Task<TestDTO> GetTestByIdAsync(Guid testId)
        {
            var test = await _testRepository.GetByIdAsync(testId);
            if (test == null)
            {
                throw new KeyNotFoundException($"Test with ID {testId} not found.");
            }
            return MapToTestDTO(test);
        }

        public async Task UpdateTestAsync(Guid testId, UpdateTestDTO updateTestDto)
        {
            var test = await _testRepository.GetByIdAsync(testId);
            if (test == null)
            {
                throw new KeyNotFoundException($"Test with ID {testId} not found.");
            }

            test.Title = updateTestDto.Title;
            test.AllowedAttempts = updateTestDto.AllowedAttempts;
            test.MinutesDuration = updateTestDto.MinutesDuration;
            test.QuestionsPerAttempt = updateTestDto.QuestionsPerAttempt;

            await _testRepository.UpdateAsync(test);
        }

        public async Task DeleteTestAsync(Guid testId)
        {
            var test = await _testRepository.GetByIdAsync(testId);
            if (test == null)
            {
                throw new KeyNotFoundException($"Test with ID {testId} not found.");
            }
            await _testRepository.DeleteAsync(testId);
        }

        public async Task<TestsListDTO> GetAllTestsBySubjectIdAsync(Guid subjectId) {
            var result = await _testRepository.GetAllTestsBySubjectIdAsync(subjectId);

            return new TestsListDTO
            {
                Tests = result.Select(t => new TestDTO { TestId = t.TestId, Title = t.Title }).ToList(),
            };
        }

        private TestDTO MapToTestDTO(Test test)
        {
            return new TestDTO
            {
                TestId = test.TestId,
                Title = test.Title,
                AllowedAttempts = test.AllowedAttempts,
                MinutesDuration = test.MinutesDuration,
                QuestionsPerAttempt = test.QuestionsPerAttempt
            };
        }
    }
}
