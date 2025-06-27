using System;
using System.Collections.Generic;
using System.Linq;
using CyberbotGUI.Models;

namespace CyberbotGUI.Services.Memory
{
    public class UserMemory : IDisposable
    {
        private bool _disposed = false;
        private const int MaxInputLength = 500;
        private const int MaxActivityLogSize = 200;
        private const int MaxConversationHistorySize = 100;

        // User properties
        public string Name { get; set; }
        public List<string> Interests { get; } = new List<string>();
        public string CurrentTopic { get; set; }
        public string Sentiment { get; set; } = "neutral";
        public List<string> ConversationHistory { get; } = new List<string>();
        public Dictionary<string, int> TopicFrequency { get; } = new Dictionary<string, int>();
        public Dictionary<string, string> PersonalDetails { get; } = new Dictionary<string, string>();

        // Task management
        public List<CyberTask> Tasks { get; } = new List<CyberTask>();
        public CyberTask CurrentTask { get; set; }

        // Quiz state
        public QuizSession CurrentQuiz { get; set; }
        public bool InQuizMode { get; set; }

        // Conversation state flags
        public bool AwaitingTaskTitle { get; set; }
        public bool AwaitingTaskDescription { get; set; }
        public bool AwaitingTaskDescriptionInput { get; set; }
        public bool AwaitingTaskDueDate { get; set; }
        public bool AwaitingTaskDueDateInput { get; set; }
        public bool AwaitingFollowUpResponse { get; set; }

        // Activity log
        private List<string> _activityLog = new List<string>();

        public void AddInterest(string interest)
        {
            if (string.IsNullOrWhiteSpace(interest)) return;

            var lowerInterest = interest.ToLower();
            if (!Interests.Contains(lowerInterest))
            {
                Interests.Add(lowerInterest);
            }

            if (TopicFrequency.ContainsKey(lowerInterest))
            {
                TopicFrequency[lowerInterest]++;
            }
            else
            {
                TopicFrequency[lowerInterest] = 1;
            }
        }

        public bool AddSecureInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            // Input length validation
            if (input.Length > MaxInputLength)
            {
                LogActivity($"Input truncated from {input.Length} characters");
                input = input.Substring(0, MaxInputLength);
            }

            // Basic XSS protection
            input = System.Web.HttpUtility.HtmlEncode(input);

            ConversationHistory.Add($"User: {input}");
            MaintainCollectionSize(ConversationHistory, MaxConversationHistorySize);
            return true;
        }

        public void AddConversation(string input, string response)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                ConversationHistory.Add($"User: {input}");
            }
            if (!string.IsNullOrWhiteSpace(response))
            {
                ConversationHistory.Add($"Cyberbot: {response}");
            }
            MaintainCollectionSize(ConversationHistory, MaxConversationHistorySize);
        }

        public void LogActivity(string activity, string category = "General")
        {
            if (string.IsNullOrWhiteSpace(activity)) return;

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var entry = $"[{timestamp}] [{category}] {activity}";

            _activityLog.Add(entry);
            MaintainCollectionSize(_activityLog, MaxActivityLogSize);
        }

        public List<string> GetRecentActivities(int count = 10, string category = null)
        {
            var query = _activityLog.AsEnumerable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(a => a.Contains($"[{category}]"));
            }

            return query
                .TakeLast(count)
                .ToList();
        }

        public void AddPersonalDetail(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
                return;

            PersonalDetails[key.ToLower()] = value;
            LogActivity($"Personal detail updated: {key}");
        }

        public string GetPersonalDetail(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return null;

            return PersonalDetails.TryGetValue(key.ToLower(), out var value) ? value : null;
        }

        private void MaintainCollectionSize<T>(List<T> collection, int maxSize)
        {
            if (collection.Count > maxSize)
            {
                collection.RemoveRange(0, collection.Count - maxSize);
            }
        }

        public string GetFavoriteTopic()
        {
            return TopicFrequency.OrderByDescending(x => x.Value)
                .FirstOrDefault().Key ?? "No topics discussed";
        }

        public void ClearSensitiveData()
        {
            PersonalDetails.Clear();
            LogActivity("All sensitive data cleared");
        }

        public void ClearConversationHistory()
        {
            ConversationHistory.Clear();
            LogActivity("Conversation history cleared");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ClearSensitiveData();
                    ClearConversationHistory();
                }
                _disposed = true;
            }
        }

        ~UserMemory()
        {
            Dispose(false);
        }
    }
}