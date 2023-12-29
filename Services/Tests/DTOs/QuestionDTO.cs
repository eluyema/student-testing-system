using student_testing_system.Services.Questions.DTOs;

namespace student_testing_system.Services.Tests.DTOs
{
    public class QuestionDTO
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public Guid TestId { get; set; }
        public List<AnswerDTO> Answers { get; set; }
    }
}
