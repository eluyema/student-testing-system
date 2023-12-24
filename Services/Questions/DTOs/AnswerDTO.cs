namespace student_testing_system.Services.Questions.DTOs
{
    public class AnswerDTO
    {
        public Guid AnswerId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }

}
