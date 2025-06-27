namespace CyberbotGUI.Models
{
    public class QuizQuestion
    {
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; }
        public string Category { get; set; } // Added for future categorization
        public DifficultyLevel Difficulty { get; set; } // Added for difficulty tracking

        public QuizQuestion()
        {
            Options = new List<string>();
            Difficulty = DifficultyLevel.Medium;
        }
    }

    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }
}