namespace CyberbotGUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _userMemory?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.chatTabPage = new System.Windows.Forms.TabPage();
            this.chatRichTextBox = new System.Windows.Forms.RichTextBox();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.tasksTabPage = new System.Windows.Forms.TabPage();
            this.taskListView = new System.Windows.Forms.ListView();
            this.addTaskButton = new System.Windows.Forms.Button();
            this.completeTaskButton = new System.Windows.Forms.Button();
            this.deleteTaskButton = new System.Windows.Forms.Button();
            this.refreshTasksButton = new System.Windows.Forms.Button();
            this.quizTabPage = new System.Windows.Forms.TabPage();
            this.startQuizButton = new System.Windows.Forms.Button();
            this.quizPanel = new System.Windows.Forms.Panel();
            this.progressLabel = new System.Windows.Forms.Label();
            this.nextQuestionButton = new System.Windows.Forms.Button();
            this.exitQuizButton = new System.Windows.Forms.Button();
            this.answer4RadioButton = new System.Windows.Forms.RadioButton();
            this.answer3RadioButton = new System.Windows.Forms.RadioButton();
            this.answer2RadioButton = new System.Windows.Forms.RadioButton();
            this.answer1RadioButton = new System.Windows.Forms.RadioButton();
            this.questionLabel = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.chatTabPage.SuspendLayout();
            this.tasksTabPage.SuspendLayout();
            this.quizTabPage.SuspendLayout();
            this.quizPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.chatTabPage);
            this.tabControl.Controls.Add(this.tasksTabPage);
            this.tabControl.Controls.Add(this.quizTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 450);
            this.tabControl.TabIndex = 0;
            // 
            // chatTabPage
            // 
            this.chatTabPage.Controls.Add(this.chatRichTextBox);
            this.chatTabPage.Controls.Add(this.inputTextBox);
            this.chatTabPage.Location = new System.Drawing.Point(4, 22);
            this.chatTabPage.Name = "chatTabPage";
            this.chatTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.chatTabPage.Size = new System.Drawing.Size(792, 424);
            this.chatTabPage.TabIndex = 0;
            this.chatTabPage.Text = "Chat";
            this.chatTabPage.UseVisualStyleBackColor = true;
            // 
            // chatRichTextBox
            // 
            this.chatRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatRichTextBox.Location = new System.Drawing.Point(6, 6);
            this.chatRichTextBox.Name = "chatRichTextBox";
            this.chatRichTextBox.ReadOnly = true;
            this.chatRichTextBox.Size = new System.Drawing.Size(780, 380);
            this.chatRichTextBox.TabIndex = 0;
            this.chatRichTextBox.Text = "";
            // 
            // inputTextBox
            // 
            this.inputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputTextBox.Location = new System.Drawing.Point(6, 392);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(780, 20);
            this.inputTextBox.TabIndex = 1;
            this.inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputTextBox_KeyDown);
            // 
            // tasksTabPage
            // 
            this.tasksTabPage.Controls.Add(this.taskListView);
            this.tasksTabPage.Controls.Add(this.addTaskButton);
            this.tasksTabPage.Controls.Add(this.completeTaskButton);
            this.tasksTabPage.Controls.Add(this.deleteTaskButton);
            this.tasksTabPage.Controls.Add(this.refreshTasksButton);
            this.tasksTabPage.Location = new System.Drawing.Point(4, 22);
            this.tasksTabPage.Name = "tasksTabPage";
            this.tasksTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.tasksTabPage.Size = new System.Drawing.Size(792, 424);
            this.tasksTabPage.TabIndex = 1;
            this.tasksTabPage.Text = "Tasks";
            this.tasksTabPage.UseVisualStyleBackColor = true;
            // 
            // taskListView
            // 
            this.taskListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskListView.HideSelection = false;
            this.taskListView.Location = new System.Drawing.Point(6, 6);
            this.taskListView.Name = "taskListView";
            this.taskListView.Size = new System.Drawing.Size(780, 360);
            this.taskListView.TabIndex = 0;
            this.taskListView.UseCompatibleStateImageBehavior = false;
            // 
            // addTaskButton
            // 
            this.addTaskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addTaskButton.Location = new System.Drawing.Point(6, 372);
            this.addTaskButton.Name = "addTaskButton";
            this.addTaskButton.Size = new System.Drawing.Size(75, 23);
            this.addTaskButton.TabIndex = 1;
            this.addTaskButton.Text = "Add Task";
            this.addTaskButton.UseVisualStyleBackColor = true;
            this.addTaskButton.Click += new System.EventHandler(this.AddTaskButton_Click);
            // 
            // completeTaskButton
            // 
            this.completeTaskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.completeTaskButton.Location = new System.Drawing.Point(87, 372);
            this.completeTaskButton.Name = "completeTaskButton";
            this.completeTaskButton.Size = new System.Drawing.Size(90, 23);
            this.completeTaskButton.TabIndex = 2;
            this.completeTaskButton.Text = "Complete Task";
            this.completeTaskButton.UseVisualStyleBackColor = true;
            this.completeTaskButton.Click += new System.EventHandler(this.CompleteTaskButton_Click);
            // 
            // deleteTaskButton
            // 
            this.deleteTaskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteTaskButton.Location = new System.Drawing.Point(183, 372);
            this.deleteTaskButton.Name = "deleteTaskButton";
            this.deleteTaskButton.Size = new System.Drawing.Size(75, 23);
            this.deleteTaskButton.TabIndex = 3;
            this.deleteTaskButton.Text = "Delete Task";
            this.deleteTaskButton.UseVisualStyleBackColor = true;
            this.deleteTaskButton.Click += new System.EventHandler(this.DeleteTaskButton_Click);
            // 
            // refreshTasksButton
            // 
            this.refreshTasksButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshTasksButton.Location = new System.Drawing.Point(711, 372);
            this.refreshTasksButton.Name = "refreshTasksButton";
            this.refreshTasksButton.Size = new System.Drawing.Size(75, 23);
            this.refreshTasksButton.TabIndex = 4;
            this.refreshTasksButton.Text = "Refresh";
            this.refreshTasksButton.UseVisualStyleBackColor = true;
            this.refreshTasksButton.Click += new System.EventHandler(this.RefreshTasksButton_Click);
            // 
            // quizTabPage
            // 
            this.quizTabPage.Controls.Add(this.startQuizButton);
            this.quizTabPage.Controls.Add(this.quizPanel);
            this.quizTabPage.Location = new System.Drawing.Point(4, 22);
            this.quizTabPage.Name = "quizTabPage";
            this.quizTabPage.Size = new System.Drawing.Size(792, 424);
            this.quizTabPage.TabIndex = 2;
            this.quizTabPage.Text = "Quiz";
            this.quizTabPage.UseVisualStyleBackColor = true;
            // 
            // startQuizButton
            // 
            this.startQuizButton.Location = new System.Drawing.Point(3, 3);
            this.startQuizButton.Name = "startQuizButton";
            this.startQuizButton.Size = new System.Drawing.Size(75, 23);
            this.startQuizButton.TabIndex = 0;
            this.startQuizButton.Text = "Start Quiz";
            this.startQuizButton.UseVisualStyleBackColor = true;
            this.startQuizButton.Click += new System.EventHandler(this.StartQuizButton_Click);
            // 
            // quizPanel
            // 
            this.quizPanel.Controls.Add(this.progressLabel);
            this.quizPanel.Controls.Add(this.nextQuestionButton);
            this.quizPanel.Controls.Add(this.exitQuizButton);
            this.quizPanel.Controls.Add(this.answer4RadioButton);
            this.quizPanel.Controls.Add(this.answer3RadioButton);
            this.quizPanel.Controls.Add(this.answer2RadioButton);
            this.quizPanel.Controls.Add(this.answer1RadioButton);
            this.quizPanel.Controls.Add(this.questionLabel);
            this.quizPanel.Location = new System.Drawing.Point(3, 32);
            this.quizPanel.Name = "quizPanel";
            this.quizPanel.Size = new System.Drawing.Size(786, 389);
            this.quizPanel.TabIndex = 1;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(3, 200);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(51, 13);
            this.progressLabel.TabIndex = 7;
            this.progressLabel.Text = "Question";
            // 
            // nextQuestionButton
            // 
            this.nextQuestionButton.Enabled = false;
            this.nextQuestionButton.Location = new System.Drawing.Point(600, 200);
            this.nextQuestionButton.Name = "nextQuestionButton";
            this.nextQuestionButton.Size = new System.Drawing.Size(90, 23);
            this.nextQuestionButton.TabIndex = 6;
            this.nextQuestionButton.Text = "Next Question";
            this.nextQuestionButton.UseVisualStyleBackColor = true;
            this.nextQuestionButton.Click += new System.EventHandler(this.NextQuestionButton_Click);
            // 
            // exitQuizButton
            // 
            this.exitQuizButton.Location = new System.Drawing.Point(500, 200);
            this.exitQuizButton.Name = "exitQuizButton";
            this.exitQuizButton.Size = new System.Drawing.Size(75, 23);
            this.exitQuizButton.TabIndex = 5;
            this.exitQuizButton.Text = "Exit Quiz";
            this.exitQuizButton.UseVisualStyleBackColor = true;
            this.exitQuizButton.Click += new System.EventHandler(this.ExitQuizButton_Click);
            // 
            // answer4RadioButton
            // 
            this.answer4RadioButton.AutoSize = true;
            this.answer4RadioButton.Location = new System.Drawing.Point(10, 170);
            this.answer4RadioButton.Name = "answer4RadioButton";
            this.answer4RadioButton.Size = new System.Drawing.Size(85, 17);
            this.answer4RadioButton.TabIndex = 4;
            this.answer4RadioButton.TabStop = true;
            this.answer4RadioButton.Text = "Answer 4";
            this.answer4RadioButton.UseVisualStyleBackColor = true;
            this.answer4RadioButton.CheckedChanged += new System.EventHandler(this.AnswerRadioButton_CheckedChanged);
            // 
            // answer3RadioButton
            // 
            this.answer3RadioButton.AutoSize = true;
            this.answer3RadioButton.Location = new System.Drawing.Point(10, 140);
            this.answer3RadioButton.Name = "answer3RadioButton";
            this.answer3RadioButton.Size = new System.Drawing.Size(85, 17);
            this.answer3RadioButton.TabIndex = 3;
            this.answer3RadioButton.TabStop = true;
            this.answer3RadioButton.Text = "Answer 3";
            this.answer3RadioButton.UseVisualStyleBackColor = true;
            this.answer3RadioButton.CheckedChanged += new System.EventHandler(this.AnswerRadioButton_CheckedChanged);
            // 
            // answer2RadioButton
            // 
            this.answer2RadioButton.AutoSize = true;
            this.answer2RadioButton.Location = new System.Drawing.Point(10, 110);
            this.answer2RadioButton.Name = "answer2RadioButton";
            this.answer2RadioButton.Size = new System.Drawing.Size(85, 17);
            this.answer2RadioButton.TabIndex = 2;
            this.answer2RadioButton.TabStop = true;
            this.answer2RadioButton.Text = "Answer 2";
            this.answer2RadioButton.UseVisualStyleBackColor = true;
            this.answer2RadioButton.CheckedChanged += new System.EventHandler(this.AnswerRadioButton_CheckedChanged);
            // 
            // answer1RadioButton
            // 
            this.answer1RadioButton.AutoSize = true;
            this.answer1RadioButton.Location = new System.Drawing.Point(10, 80);
            this.answer1RadioButton.Name = "answer1RadioButton";
            this.answer1RadioButton.Size = new System.Drawing.Size(85, 17);
            this.answer1RadioButton.TabIndex = 1;
            this.answer1RadioButton.TabStop = true;
            this.answer1RadioButton.Text = "Answer 1";
            this.answer1RadioButton.UseVisualStyleBackColor = true;
            this.answer1RadioButton.CheckedChanged += new System.EventHandler(this.AnswerRadioButton_CheckedChanged);
            // 
            // questionLabel
            // 
            this.questionLabel.AutoSize = true;
            this.questionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionLabel.Location = new System.Drawing.Point(7, 20);
            this.questionLabel.MaximumSize = new System.Drawing.Size(760, 0);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(68, 16);
            this.questionLabel.TabIndex = 0;
            this.questionLabel.Text = "Question";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.Text = "Cyberbot - Cybersecurity Assistant";
            this.tabControl.ResumeLayout(false);
            this.chatTabPage.ResumeLayout(false);
            this.chatTabPage.PerformLayout();
            this.tasksTabPage.ResumeLayout(false);
            this.quizTabPage.ResumeLayout(false);
            this.quizPanel.ResumeLayout(false);
            this.quizPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage chatTabPage;
        private System.Windows.Forms.RichTextBox chatRichTextBox;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.TabPage tasksTabPage;
        private System.Windows.Forms.ListView taskListView;
        private System.Windows.Forms.Button addTaskButton;
        private System.Windows.Forms.Button completeTaskButton;
        private System.Windows.Forms.Button deleteTaskButton;
        private System.Windows.Forms.Button refreshTasksButton;
        private System.Windows.Forms.TabPage quizTabPage;
        private System.Windows.Forms.Button startQuizButton;
        private System.Windows.Forms.Panel quizPanel;
        private System.Windows.Forms.Label questionLabel;
        private System.Windows.Forms.RadioButton answer4RadioButton;
        private System.Windows.Forms.RadioButton answer3RadioButton;
        private System.Windows.Forms.RadioButton answer2RadioButton;
        private System.Windows.Forms.RadioButton answer1RadioButton;
        private System.Windows.Forms.Button nextQuestionButton;
        private System.Windows.Forms.Button exitQuizButton;
        private System.Windows.Forms.Label progressLabel;
    }
}