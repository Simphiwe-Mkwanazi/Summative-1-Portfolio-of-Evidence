using CyberbotGUI.Models;
using CyberbotGUI.Services.Display;
using CyberbotGUI.Services.Memory;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberbotGUI
{
   
    public partial class QuizForm : Form
    {
        private readonly QuizSession _quizSession;
        private int _selectedAnswer = -1;
        private readonly UserMemory _userMemory;
        private readonly GuiDisplay _display;

       
        public QuizForm(QuizSession quizSession, UserMemory userMemory, GuiDisplay display)
        {
            InitializeComponent();
            _quizSession = quizSession ?? throw new ArgumentNullException(nameof(quizSession));
            _userMemory = userMemory ?? throw new ArgumentNullException(nameof(userMemory));
            _display = display ?? throw new ArgumentNullException(nameof(display));

            ConfigureQuizUI();
            DisplayQuestion();
        }

        private void ConfigureQuizUI()
        {
            // Form styling
            this.BackColor = Color.FromArgb(40, 40, 50);
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Question label styling
            questionLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            questionLabel.ForeColor = Color.LightSkyBlue;
            questionLabel.MaximumSize = new Size(450, 0);

            // Answer options styling
            var answerFont = new Font("Segoe UI", 9);
            answer1RadioButton.Font = answerFont;
            answer2RadioButton.Font = answerFont;
            answer3RadioButton.Font = answerFont;
            answer4RadioButton.Font = answerFont;

            // Progress label styling
            progressLabel.Font = new Font("Segoe UI", 8, FontStyle.Italic);
            progressLabel.ForeColor = Color.LightGray;

            // Button styling
            submitButton.BackColor = Color.FromArgb(70, 130, 180);
            submitButton.ForeColor = Color.White;
            submitButton.FlatStyle = FlatStyle.Flat;
            submitButton.FlatAppearance.BorderSize = 0;

            exitButton.BackColor = Color.FromArgb(70, 70, 80);
            exitButton.ForeColor = Color.White;
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.FlatAppearance.BorderSize = 0;
        }

        private void DisplayQuestion()
        {
            var question = _quizSession.GetCurrentQuestion();
            if (question == null)
            {
                Close();
                return;
            }

            questionLabel.Text = $"Question {_quizSession.CurrentQuestionIndex + 1}:\n\n{question.QuestionText}";
            answer1RadioButton.Text = $"A) {question.Options[0]}";
            answer2RadioButton.Text = $"B) {question.Options[1]}";
            answer3RadioButton.Text = $"C) {question.Options[2]}";
            answer4RadioButton.Text = $"D) {question.Options[3]}";

            answer1RadioButton.Checked = false;
            answer2RadioButton.Checked = false;
            answer3RadioButton.Checked = false;
            answer4RadioButton.Checked = false;
            _selectedAnswer = -1;

            progressLabel.Text = $"Progress: {_quizSession.CurrentQuestionIndex + 1}/{_quizSession.Questions.Count} | " +
                               $"Score: {_quizSession.Score}";

            submitButton.Enabled = false;
        }

        private void AnswerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (answer1RadioButton.Checked) _selectedAnswer = 0;
            else if (answer2RadioButton.Checked) _selectedAnswer = 1;
            else if (answer3RadioButton.Checked) _selectedAnswer = 2;
            else if (answer4RadioButton.Checked) _selectedAnswer = 3;

            submitButton.Enabled = _selectedAnswer != -1;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            bool isCorrect = _quizSession.AnswerCurrentQuestion(_selectedAnswer);
            var currentQuestion = _quizSession.GetCurrentQuestion();

            string resultMessage = isCorrect
                ? $"✓ Correct! {currentQuestion.Explanation}"
                : $"✗ Incorrect. {currentQuestion.Explanation}";

            MessageBox.Show(resultMessage,
                isCorrect ? "Correct!" : "Incorrect",
                MessageBoxButtons.OK,
                isCorrect ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (_quizSession.IsCompleted)
            {
                ShowQuizResults();
                Close();
            }
            else
            {
                DisplayQuestion();
            }
        }

        private void ShowQuizResults()
        {
            double scorePercent = (_quizSession.Score * 100.0) / _quizSession.Questions.Count;
            string feedback = GetPersonalizedFeedback(scorePercent);

            string resultMessage = $"Quiz completed, {_userMemory.Name}!\n" +
                                 $"Your score: {_quizSession.Score}/{_quizSession.Questions.Count}\n\n" +
                                 feedback;

            MessageBox.Show(resultMessage, "Quiz Complete",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            _userMemory.LogActivity($"Completed quiz with score {_quizSession.Score}/{_quizSession.Questions.Count}");
            _display.Show($"Quiz completed! Final score: {_quizSession.Score}/{_quizSession.Questions.Count}");
            _display.Show(feedback);
        }

        private string GetPersonalizedFeedback(double scorePercent)
        {
            if (scorePercent >= 80)
                return $"Excellent work, {_userMemory.Name}! You're a cybersecurity expert!";
            if (scorePercent >= 60)
                return $"Good job, {_userMemory.Name}! You have solid cybersecurity knowledge.";
            if (scorePercent >= 40)
                return $"Not bad, {_userMemory.Name}! Keep learning to improve your score.";

            return $"{_userMemory.Name}, keep practicing! Cybersecurity is important to learn.";
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit the quiz? Your progress will be lost.",
                "Exit Quiz", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _userMemory.LogActivity("Exited quiz before completion");
                Close();
            }
        }

    }
}