namespace student_testing_system.Services.UserAnswers.DTOs
{
    public class CreateUserAnswerDTO
    {
        public Guid TestSessionId { get; set; }
        public Guid AssignedQuestionId { get; set; }
        public Guid AnswerId { get; set; }
    }
}
