namespace student_testing_system.Services.UserAnswers.DTOs
{
    public class UserAnswerDTO
    {
        public Guid UserAnswerId { get; set; }
        public Guid TestSessionId { get; set; }
        public Guid AssignedQuestionId { get; set; }
        public Guid AnswerId { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
    }
}
