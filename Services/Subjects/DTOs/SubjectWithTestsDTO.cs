namespace student_testing_system.Services.Subjects.DTOs
{
    public class SubjectWithTestsDTO
    {
        public Guid SubjectId { get; set; }
        public string Name { get; set; }
        public ICollection<TestDTO> Tests { get; set; }
    }
}
