namespace student_testing_system.Configurations
{
    public class AuthSettings
    {
        public string SecretKey { get; set; }
        public int ExpiryInMinutes { get; set; }
        public int RefreshTokenExpiryInDays { get; set; }
    }
}
