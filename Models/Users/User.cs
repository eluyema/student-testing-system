using System;
using System.ComponentModel.DataAnnotations;

namespace student_testing_system.Models.Users
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
    }
}
