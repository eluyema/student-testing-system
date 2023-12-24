using student_testing_system.Models.Subjects;
using student_testing_system.Models.Tests;
using student_testing_system.Services.Subjects.DTOs;

namespace student_testing_system.Services.Subjects
{
    public class SubjectService : ISubjectService
    {
        private readonly SubjectRepository _subjectRepository;

        public SubjectService(SubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<IEnumerable<SubjectDTO>> GetAllSubjectsAsync()
        {
            var subjects = await _subjectRepository.GetAllAsync();
            return subjects.Select(s => new SubjectDTO { SubjectId = s.SubjectId, Name = s.Name });
        }

        public async Task<SubjectDTO> GetSubjectByIdAsync(Guid id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject == null)
            {
                throw new KeyNotFoundException($"Subject with ID {id} not found");
            }
            return new SubjectDTO { SubjectId = subject.SubjectId, Name = subject.Name };
        }

        public async Task<SubjectDTO> CreateSubjectAsync(CreateSubjectDTO createSubjectDto)
        {
            var subject = new Subject { Name = createSubjectDto.Name };
            await _subjectRepository.CreateAsync(subject);
            return new SubjectDTO { SubjectId = subject.SubjectId, Name = subject.Name };
        }

        public async Task UpdateSubjectAsync(Guid id, UpdateSubjectDTO updateSubjectDto)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject == null)
            {
                throw new KeyNotFoundException($"Subject with ID {id} not found");
            }

            subject.Name = updateSubjectDto.Name;
            await _subjectRepository.UpdateAsync(subject);
        }

        public async Task DeleteSubjectAsync(Guid id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject == null)
            {
                throw new KeyNotFoundException($"Subject with ID {id} not found");
            }
            await _subjectRepository.DeleteAsync(id);
        }

        public async Task AddTestToSubjectAsync(Guid subjectId, CreateTestDTO createTestDto)
        {
            var subject = await _subjectRepository.GetByIdAsync(subjectId);
            if (subject == null)
            {
                throw new KeyNotFoundException($"Subject with ID {subjectId} not found");
            }
            var test = new Test
            {
                Title = createTestDto.Title,
                AllowedAttempts = createTestDto.AllowedAttempts,
                MinutesDuration = createTestDto.MinutesDuration,
                QuestionsPerAttempt = createTestDto.QuestionsPerAttempt
            };

            await _subjectRepository.AddTestToSubjectAsync(subjectId, test);
        }



        public async Task<IEnumerable<SubjectWithTestsDTO>> GetAllSubjectsWithTestsAsync()
        {
            var subjects = await _subjectRepository.GetAllSubjectsWithTestsAsync();

            return subjects.Select(s => new SubjectWithTestsDTO
            {
                SubjectId = s.SubjectId,
                Name = s.Name,
                Tests = s.Tests.Select(t => new TestDTO
                {
                    TestId = t.TestId,
                    Title = t.Title,
                    AllowedAttempts = t.AllowedAttempts,
                    MinutesDuration = t.MinutesDuration,
                    QuestionsPerAttempt = t.QuestionsPerAttempt
                }).ToList()
            });
        }
    }


}
