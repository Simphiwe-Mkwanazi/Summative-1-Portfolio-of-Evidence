using CyberbotGUI.Models;
using CyberbotGUI.Services.Display;
using CyberbotGUI.Services.Memory;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CyberbotGUI
{
    public partial class TaskForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CyberTask Task { get; private set; }

        // Made these optional to maintain backward compatibility
        private readonly UserMemory _userMemory;
        private readonly GuiDisplay _display;

        /// <summary>
        /// Initializes a new instance of the TaskForm class.
        /// </summary>
        public TaskForm() : this(null, null) { }

        /// <summary>
        /// Initializes a new instance with dependencies.
        /// </summary>
        public TaskForm(UserMemory userMemory, GuiDisplay display)
        {
            InitializeComponent();
            _userMemory = userMemory;
            _display = display;

            Task = new CyberTask();
            dueDateTimePicker.MinDate = DateTime.Today;
            ConfigureFormUI();
        }

        private void ConfigureFormUI()
        {
            // Form styling
            this.BackColor = Color.FromArgb(45, 45, 55);
            this.ForeColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Control styling (unchanged from previous version)
            titleLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            descriptionLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            titleTextBox.BackColor = Color.FromArgb(60, 60, 70);
            titleTextBox.ForeColor = Color.White;
            titleTextBox.BorderStyle = BorderStyle.FixedSingle;

            descriptionTextBox.BackColor = Color.FromArgb(60, 60, 70);
            descriptionTextBox.ForeColor = Color.White;
            descriptionTextBox.BorderStyle = BorderStyle.FixedSingle;

            setReminderCheckBox.ForeColor = Color.LightGray;

            dueDateTimePicker.CalendarMonthBackground = Color.FromArgb(60, 60, 70);
            dueDateTimePicker.CalendarTitleBackColor = Color.FromArgb(50, 50, 60);
            dueDateTimePicker.CalendarForeColor = Color.White;
            dueDateTimePicker.Enabled = false;

            saveButton.BackColor = Color.FromArgb(70, 130, 180);
            saveButton.ForeColor = Color.White;
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.FlatAppearance.BorderSize = 0;

            cancelButton.BackColor = Color.FromArgb(70, 70, 80);
            cancelButton.ForeColor = Color.White;
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.FlatAppearance.BorderSize = 0;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleTextBox.Text))
            {
                MessageBox.Show("Please enter a title for the task.", "Missing Title",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Task.Title = titleTextBox.Text;
            Task.Description = descriptionTextBox.Text;

            if (setReminderCheckBox.Checked)
            {
                Task.DueDate = dueDateTimePicker.Value;
                Task.HasReminder = true;
            }

            // Only log if userMemory is available
            if (_userMemory != null)
            {
                _userMemory.LogActivity($"Task saved: {Task.Title}");
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void setReminderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            dueDateTimePicker.Enabled = setReminderCheckBox.Checked;
        }

        // Kept all other methods but made them null-safe
        public void LoadTaskForEditing(CyberTask task)
        {
            if (task == null) return;

            Task = task;
            titleTextBox.Text = task.Title;
            descriptionTextBox.Text = task.Description;

            if (task.DueDate.HasValue)
            {
                setReminderCheckBox.Checked = true;
                dueDateTimePicker.Value = task.DueDate.Value;
            }

            this.Text = "Edit Task";

            if (_userMemory != null)
            {
                _userMemory.LogActivity($"Editing task: {task.Title}");
            }
        }
    }
}