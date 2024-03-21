namespace student_testing_system.Services.TestSessions.DTOs
{
    public class TestSessionDTO
    {
        public Guid TestSessionId { get; set; }
        public Guid TestId { get; set; }
        public string UserId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public bool IsCompleted { get; set; }
        public double? Score { get; set; }
    }
}
