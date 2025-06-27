using CyberbotGUI.Models;
using CyberbotGUI.Services.Display;
using CyberbotGUI.Services.Memory;
using CyberbotGUI.Services.ResponseHandlers;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace CyberbotGUI
{
    public partial class MainForm : Form
    {
        private readonly UserMemory _userMemory;
        private readonly CyberbotResponseHandler _responseHandler;
        private readonly GuiDisplay _display;

        public MainForm()
        {
            InitializeComponent();

            // Initialize components
            _userMemory = new UserMemory();
            _display = new GuiDisplay(this);
            _responseHandler = new CyberbotResponseHandler(_display, _userMemory);

            // Configure UI
            ConfigureChatInterface();
            ConfigureTaskManagement();
            ConfigureQuizInterface();

            // Start greeting
            StartGreeting();
        }

        private void ConfigureChatInterface()
        {
            chatRichTextBox.BackColor = Color.FromArgb(30, 30, 30);
            chatRichTextBox.ForeColor = Color.White;
            chatRichTextBox.Font = new Font("Consolas", 10);

            inputTextBox.KeyDown += InputTextBox_KeyDown;
        }

        private void ConfigureTaskManagement()
        {
            taskListView.View = View.Details;
            taskListView.Columns.Add("Title", 200);
            taskListView.Columns.Add("Description", 300);
            taskListView.Columns.Add("Due Date", 100);
            taskListView.Columns.Add("Status", 80);

            taskListView.FullRowSelect = true;
            taskListView.GridLines = true;
            taskListView.BackColor = Color.FromArgb(40, 40, 40);
            taskListView.ForeColor = Color.White;

            addTaskButton.Click += AddTaskButton_Click;
            completeTaskButton.Click += CompleteTaskButton_Click;
            deleteTaskButton.Click += DeleteTaskButton_Click;
            refreshTasksButton.Click += RefreshTasksButton_Click;

            RefreshTaskList();
        }

        private void ConfigureQuizInterface()
        {
            quizPanel.Visible = false;

            startQuizButton.Click += StartQuizButton_Click;
            nextQuestionButton.Click += NextQuestionButton_Click;
            exitQuizButton.Click += ExitQuizButton_Click;

            answer1RadioButton.CheckedChanged += AnswerRadioButton_CheckedChanged;
            answer2RadioButton.CheckedChanged += AnswerRadioButton_CheckedChanged;
            answer3RadioButton.CheckedChanged += AnswerRadioButton_CheckedChanged;
            answer4RadioButton.CheckedChanged += AnswerRadioButton_CheckedChanged;
        }

        private void StartGreeting()
        {
            _display.ShowTypingEffect("Hello! I'm Cyberbot, your personal cybersecurity assistant.");
            _display.ShowTypingEffect("What's your name?");
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(inputTextBox.Text))
            {
                string input = inputTextBox.Text;
                inputTextBox.Clear();

                // Add user message to chat
                AddMessageToChat(_userMemory.Name ?? "You", input, isUser: true);

                // Process through chatbot
                _responseHandler.HandleResponse(input);

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        public void AddMessageToChat(string sender, string message, bool isUser)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => AddMessageToChat(sender, message, isUser)));
                return;
            }

            Color color = isUser ? Color.LightBlue : Color.LightGreen;

            chatRichTextBox.SelectionColor = color;
            chatRichTextBox.AppendText($"{sender}: {message}\n");
            chatRichTextBox.ScrollToCaret();
        }

        public void ShowWithColor(string message, Color color)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowWithColor(message, color)));
                return;
            }

            chatRichTextBox.SelectionColor = color;
            chatRichTextBox.AppendText($"Cyberbot: {message}\n");
            chatRichTextBox.ScrollToCaret();
        }

        private void RefreshTaskList()
        {
            taskListView.Items.Clear();
            foreach (var task in _userMemory.Tasks)
            {
                var item = new ListViewItem(task.Title);
                item.SubItems.Add(task.Description ?? "");
                item.SubItems.Add(task.DueDate?.ToShortDateString() ?? "No due date");
                item.SubItems.Add(task.IsCompleted ? "Completed" : "Pending");
                item.Tag = task;

                if (task.IsCompleted)
                {
                    item.ForeColor = Color.Gray;
                }
                else if (task.DueDate.HasValue && task.DueDate.Value < DateTime.Now)
                {
                    item.ForeColor = Color.OrangeRed;
                }

                taskListView.Items.Add(item);
            }
        }

        private void AddTaskButton_Click(object sender, EventArgs e)
        {
            var taskForm = new TaskForm();
            if (taskForm.ShowDialog() == DialogResult.OK)
            {
                _userMemory.Tasks.Add(taskForm.Task);
                _userMemory.LogActivity($"Task added: {taskForm.Task.Title}");
                RefreshTaskList();

                AddMessageToChat("Cyberbot", $"Task '{taskForm.Task.Title}' has been added.", false);
            }
        }

        private void CompleteTaskButton_Click(object sender, EventArgs e)
        {
            if (taskListView.SelectedItems.Count == 0) return;

            var task = (CyberTask)taskListView.SelectedItems[0].Tag;
            task.IsCompleted = true;
            _userMemory.LogActivity($"Task completed: {task.Title}");
            RefreshTaskList();

            AddMessageToChat("Cyberbot", $"Task '{task.Title}' marked as completed!", false);
        }

        private void DeleteTaskButton_Click(object sender, EventArgs e)
        {
            if (taskListView.SelectedItems.Count == 0) return;

            var task = (CyberTask)taskListView.SelectedItems[0].Tag;
            _userMemory.Tasks.Remove(task);
            _userMemory.LogActivity($"Task deleted: {task.Title}");
            RefreshTaskList();

            AddMessageToChat("Cyberbot", $"Task '{task.Title}' has been deleted.", false);
        }

        private void RefreshTasksButton_Click(object sender, EventArgs e)
        {
            RefreshTaskList();
        }

        private void StartQuizButton_Click(object sender, EventArgs e)
        {
            try
            {
                _userMemory.CurrentQuiz = new QuizSession();
                var questions = _responseHandler.GetQuizQuestions();
                if (questions != null && questions.Count > 0)
                {
                    _userMemory.CurrentQuiz.Questions.AddRange(questions);
                    _userMemory.InQuizMode = true;
                    _userMemory.LogActivity("Started cybersecurity quiz");

                    ShowQuizQuestion(_userMemory.CurrentQuiz.GetCurrentQuestion());
                    quizPanel.Visible = true;
                }
                else
                {
                    MessageBox.Show("No quiz questions available.", "Quiz Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting quiz: {ex.Message}", "Quiz Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowQuizQuestion(QuizQuestion question)
        {
            if (question == null) return;

            questionLabel.Text = question.QuestionText;
            answer1RadioButton.Text = question.Options[0];
            answer2RadioButton.Text = question.Options[1];
            answer3RadioButton.Text = question.Options[2];
            answer4RadioButton.Text = question.Options[3];

            answer1RadioButton.Checked = false;
            answer2RadioButton.Checked = false;
            answer3RadioButton.Checked = false;
            answer4RadioButton.Checked = false;

            progressLabel.Text = $"Question {_userMemory.CurrentQuiz.CurrentQuestionIndex + 1} of {_userMemory.CurrentQuiz.Questions.Count}";
        }

        private void NextQuestionButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_userMemory.CurrentQuiz == null)
                {
                    MessageBox.Show("Quiz session is not active.", "Quiz Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    quizPanel.Visible = false;
                    _userMemory.InQuizMode = false;
                    return;
                }

                int selectedAnswer = -1;
                if (answer1RadioButton.Checked) selectedAnswer = 0;
                else if (answer2RadioButton.Checked) selectedAnswer = 1;
                else if (answer3RadioButton.Checked) selectedAnswer = 2;
                else if (answer4RadioButton.Checked) selectedAnswer = 3;

                // Remove the check for selectedAnswer == -1 since the button is only enabled when an answer is selected
                var currentQuestion = _userMemory.CurrentQuiz.GetCurrentQuestion();
                if (currentQuestion == null)
                {
                    MessageBox.Show("No current question available.", "Quiz Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool isCorrect = _userMemory.CurrentQuiz.AnswerCurrentQuestion(selectedAnswer);

                if (isCorrect)
                {
                    AddMessageToChat("Cyberbot", "✓ Correct! " + currentQuestion.Explanation, false);
                }
                else
                {
                    AddMessageToChat("Cyberbot", "✗ Incorrect. " + currentQuestion.Explanation, false);
                }

                if (_userMemory.CurrentQuiz.IsCompleted)
                {
                    double scorePercent = (_userMemory.CurrentQuiz.Score * 100.0) / _userMemory.CurrentQuiz.Questions.Count;
                    string feedback;

                    if (scorePercent >= 80) feedback = "Excellent work! You're a cybersecurity expert!";
                    else if (scorePercent >= 60) feedback = "Good job! You have solid cybersecurity knowledge.";
                    else if (scorePercent >= 40) feedback = "Not bad! Keep learning to improve your score.";
                    else feedback = "Keep practicing! Cybersecurity is important to learn.";

                    AddMessageToChat("Cyberbot", $"Quiz completed! Your score: {_userMemory.CurrentQuiz.Score}/{_userMemory.CurrentQuiz.Questions.Count}", false);
                    AddMessageToChat("Cyberbot", feedback, false);

                    _userMemory.InQuizMode = false;
                    quizPanel.Visible = false;
                    _userMemory.CurrentQuiz = null;
                }
                else
                {
                    ShowQuizQuestion(_userMemory.CurrentQuiz.GetCurrentQuestion());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing question: {ex.Message}", "Quiz Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                quizPanel.Visible = false;
                _userMemory.InQuizMode = false;
                _userMemory.CurrentQuiz = null;
            }
        }

        private void ExitQuizButton_Click(object sender, EventArgs e)
        {
            try
            {
                _userMemory.InQuizMode = false;
                _userMemory.CurrentQuiz = null;
                quizPanel.Visible = false;

                AddMessageToChat("Cyberbot", "Quiz exited. What would you like to discuss now?", false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exiting quiz: {ex.Message}", "Quiz Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AnswerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            nextQuestionButton.Enabled = answer1RadioButton.Checked ||
                                         answer2RadioButton.Checked ||
                                         answer3RadioButton.Checked ||
                                         answer4RadioButton.Checked;
        }
    }
}