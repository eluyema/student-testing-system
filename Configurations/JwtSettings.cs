namespace student_testing_system.Configurations
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int ExpiryInMinutes { get; set; }
    }
}
