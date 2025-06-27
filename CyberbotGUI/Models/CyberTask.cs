using System;

namespace CyberbotGUI.Models
{
    public class CyberTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool HasReminder { get; set; }

        public override string ToString()
        {
            return $"{Title} - {(IsCompleted ? "Completed" : "Pending")}";
        }
    }
}