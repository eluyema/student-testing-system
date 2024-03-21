using System.ComponentModel.DataAnnotations;

namespace student_testing_system.Services.Questions.DTOs
{
    public class CreateQuestionWithAnswersDTO
    {
        public string Text { get; set; }

        [MinLength(2, ErrorMessage = "There must be at least 2 answers.")]
        [MaxLength(10, ErrorMessage = "There cannot be more than 10 answers.")]
        public List<CreateAnswerDTO> Answers { get; set; }
    }
}
