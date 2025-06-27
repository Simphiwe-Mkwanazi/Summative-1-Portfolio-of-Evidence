namespace CyberbotGUI
{
    partial class QuizForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.questionLabel = new System.Windows.Forms.Label();
            this.answer1RadioButton = new System.Windows.Forms.RadioButton();
            this.answer2RadioButton = new System.Windows.Forms.RadioButton();
            this.answer3RadioButton = new System.Windows.Forms.RadioButton();
            this.answer4RadioButton = new System.Windows.Forms.RadioButton();
            this.submitButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.progressLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // questionLabel
            // 
            this.questionLabel.AutoSize = true;
            this.questionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionLabel.Location = new System.Drawing.Point(12, 20);
            this.questionLabel.MaximumSize = new System.Drawing.Size(460, 0);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(68, 16);
            this.questionLabel.TabIndex = 0;
            this.questionLabel.Text = "Question";
            // 
            // answer1RadioButton
            // 
            this.answer1RadioButton.AutoSize = true;
            this.answer1RadioButton.Location = new System.Drawing.Point(15, 60);
            this.answer1RadioButton.Name = "answer1RadioButton";
            this.answer1RadioButton.Size = new System.Drawing.Size(85, 17);
            this.answer1RadioButton.TabIndex = 1;
            this.answer1RadioButton.TabStop = true;
            this.answer1RadioButton.Text = "Answer 1";
            this.answer1RadioButton.UseVisualStyleBackColor = true;
            this.answer1RadioButton.CheckedChanged += new System.EventHandler(this.AnswerRadioButton_CheckedChanged);
            // 
            // answer2RadioButton
            // 
            this.answer2RadioButton.AutoSize = true;
            this.answer2RadioButton.Location = new System.Drawing.Point(15, 90);
            this.answer2RadioButton.Name = "answer2RadioButton";
            this.answer2RadioButton.Size = new System.Drawing.Size(85, 17);
            this.answer2RadioButton.TabIndex = 2;
            this.answer2RadioButton.TabStop = true;
            this.answer2RadioButton.Text = "Answer 2";
            this.answer2RadioButton.UseVisualStyleBackColor = true;
            this.answer2RadioButton.CheckedChanged += new System.EventHandler(this.AnswerRadioButton_CheckedChanged);
            // 
            // answer3RadioButton
            // 
            this.answer3RadioButton.AutoSize = true;
            this.answer3RadioButton.Location = new System.Drawing.Point(15, 120);
            this.answer3RadioButton.Name = "answer3RadioButton";
            this.answer3RadioButton.Size = new System.Drawing.Size(85, 17);
            this.answer3RadioButton.TabIndex = 3;
            this.answer3RadioButton.TabStop = true;
            this.answer3RadioButton.Text = "Answer 3";
            this.answer3RadioButton.UseVisualStyleBackColor = true;
            this.answer3RadioButton.CheckedChanged += new System.EventHandler(this.AnswerRadioButton_CheckedChanged);
            // 
            // answer4RadioButton
            // 
            this.answer4RadioButton.AutoSize = true;
            this.answer4RadioButton.Location = new System.Drawing.Point(15, 150);
            this.answer4RadioButton.Name = "answer4RadioButton";
            this.answer4RadioButton.Size = new System.Drawing.Size(85, 17);
            this.answer4RadioButton.TabIndex = 4;
            this.answer4RadioButton.TabStop = true;
            this.answer4RadioButton.Text = "Answer 4";
            this.answer4RadioButton.UseVisualStyleBackColor = true;
            this.answer4RadioButton.CheckedChanged += new System.EventHandler(this.AnswerRadioButton_CheckedChanged);
            // 
            // submitButton
            // 
            this.submitButton.Enabled = false;
            this.submitButton.Location = new System.Drawing.Point(300, 180);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 5;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(200, 180);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 6;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(12, 185);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(51, 13);
            this.progressLabel.TabIndex = 7;
            this.progressLabel.Text = "Question";
            // 
            // QuizForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 211);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.answer4RadioButton);
            this.Controls.Add(this.answer3RadioButton);
            this.Controls.Add(this.answer2RadioButton);
            this.Controls.Add(this.answer1RadioButton);
            this.Controls.Add(this.questionLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuizForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cybersecurity Quiz";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label questionLabel;
        private System.Windows.Forms.RadioButton answer1RadioButton;
        private System.Windows.Forms.RadioButton answer2RadioButton;
        private System.Windows.Forms.RadioButton answer3RadioButton;
        private System.Windows.Forms.RadioButton answer4RadioButton;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label progressLabel;
    }
}