using student_testing_system.Services.Questions.DTOs;

namespace student_testing_system.Services.TestSessions.DTOs
{
    public class QuestionDTO
    {
        public Guid AssignedQuestionId { get; set; }
        public string Text { get; set; }
        public List<AnswerDTO> Answers { get; set; }
    }
}
