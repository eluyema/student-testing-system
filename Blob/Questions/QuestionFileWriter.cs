

using student_testing_system.Services.Questions.DTOs;

namespace student_testing_system.Blob.Questions
{
    public static class QuestionFileWriter
    {
        public static string WriteToFile(CreateQuestionWithAnswersDTO questionDto, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = $"Question_{Guid.NewGuid()}.txt";
            var filePath = Path.Combine(directoryPath, fileName);

            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Title: {questionDto.Text}");
                writer.WriteLine("Correct: " + GetCorrectAnswerLetter(questionDto.Answers));

                char answerLetter = 'A';
                foreach (var answer in questionDto.Answers)
                {
                    writer.WriteLine($"{answerLetter}. {answer.Text}");
                    answerLetter++;
                }
            }

            return filePath;
        }

        private static string GetCorrectAnswerLetter(List<CreateAnswerDTO> answers)
        {
            int correctIndex = answers.FindIndex(a => a.IsCorrect);
            return correctIndex >= 0 ? ((char)('A' + correctIndex)).ToString() : "N/A";
        }
    }
}
