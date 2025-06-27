using System;
using System.Collections.Generic;

namespace CyberbotGUI.Models
{
    public class QuizSession
    {
        public List<QuizQuestion> Questions { get; set; }
        public int CurrentQuestionIndex { get; set; }
        public int Score { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsCompleted { get; set; }

        public QuizSession()
        {
            Questions = new List<QuizQuestion>();
            StartTime = DateTime.Now;
        }

        public QuizQuestion GetCurrentQuestion()
        {
            if (CurrentQuestionIndex < Questions.Count)
                return Questions[CurrentQuestionIndex];
            return null;
        }

        public bool AnswerCurrentQuestion(int answerIndex)
        {
            var current = GetCurrentQuestion();
            if (current == null) return false;

            bool isCorrect = answerIndex == current.CorrectAnswerIndex;
            if (isCorrect) Score++;

            CurrentQuestionIndex++;
            if (CurrentQuestionIndex >= Questions.Count)
                IsCompleted = true;

            return isCorrect;
        }
    }
}