namespace student_testing_system.Services.Questions.DTOs
{
    public class QuestionDTO
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public Guid TestId { get; set; }
        public string? FileName { get; set; } = null;
        public List<AnswerDTO> Answers { get; set; }
    }

}
