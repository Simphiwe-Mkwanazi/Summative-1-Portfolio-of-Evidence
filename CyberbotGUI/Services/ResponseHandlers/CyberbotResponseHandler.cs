using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using CyberbotGUI.Models;
using CyberbotGUI.Services.Display;
using CyberbotGUI.Services.Memory;

namespace CyberbotGUI.Services.ResponseHandlers
{
    public class CyberbotResponseHandler
    {
        protected readonly GuiDisplay _display;
        protected readonly UserMemory _userMemory;
        protected readonly Random _random = new Random();

        // Topic responses
        private readonly Dictionary<string, List<string>> _topicResponses;
        private readonly Dictionary<string, string> _sentimentResponses;
        private readonly Dictionary<string, List<string>> _followUpQuestions;

        // Quiz questions
        private readonly List<QuizQuestion> _quizQuestions;

        // Conversation history
        private readonly List<string> _conversationHistory = new List<string>();
        private const int MaxHistoryItems = 10;

        public CyberbotResponseHandler(GuiDisplay display, UserMemory userMemory)
        {
            _display = display;
            _userMemory = userMemory;
            _random = new Random();

            // Initialize all collections
            _topicResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            _sentimentResponses = new Dictionary<string, string>();
            _followUpQuestions = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            _quizQuestions = new List<QuizQuestion>();

            // Initialize the conversation history
            _conversationHistory = new List<string>();

            InitializeTopicResponses();
            InitializeSentimentResponses();
            InitializeFollowUpQuestions();
            InitializeQuizQuestions();
        }

        private void InitializeTopicResponses()
        {
            _topicResponses["password"] = new List<string>
            {
                $"Strong passwords should be at least 12 characters long and include a mix of uppercase, lowercase, numbers, and symbols. {GetUserName()} I recommend using a password manager to keep track of them all.",
                $"Consider using a passphrase instead of a password - something like 'PurpleMonkeyDishwasher42!' is both strong and memorable. What do you think, {GetUserName()}?",
                $"As we've discussed before, {GetUserName()}, never reuse passwords across different accounts. A password manager can help you keep track of unique passwords for each site."
            };

            _topicResponses["phishing"] = new List<string>
            {
                $"Phishing emails often create a sense of urgency. {GetUserName()}, always verify requests for personal information through official channels.",
                $"Remember {GetUserName()}, check the sender's email address carefully - scammers often use addresses that look similar to legitimate ones.",
                $"Hover over links before clicking to see the actual URL. If it looks suspicious, don't click! This is especially important for you, {GetUserName()}, as phishing attempts are becoming more sophisticated."
            };

            _topicResponses["privacy"] = new List<string>
            {
                $"{GetUserName()}, I recommend reviewing privacy settings on all your accounts regularly - companies often update their policies and default settings.",
                $"Be cautious about what personal information you share online, {GetUserName()}. Even seemingly harmless details can be used against you.",
                $"{GetUserName()}, since you're concerned about privacy, consider using tools like Signal for messaging or Brave as your browser for better privacy protection."
            };

            _topicResponses["malware"] = new List<string>
            {
                $"Keep your antivirus software updated and run regular scans, {GetUserName()}. This simple habit can prevent most malware infections.",
                $"{GetUserName()}, be very careful with email attachments and downloads from untrusted sources - these are common ways malware spreads.",
                $"Malware often exploits outdated software. {GetUserName()}, enable automatic updates whenever possible to stay protected."
            };

            _topicResponses["vpn"] = new List<string>
            {
                $"{GetUserName()}, a good VPN encrypts your internet traffic, making it much harder for others to spy on your online activities.",
                $"When choosing a VPN provider, look for one with a strict no-logs policy and independent audits. I can recommend some good options if you'd like, {GetUserName()}.",
                $"{GetUserName()}, use a VPN whenever you're on public WiFi to protect your data from potential snoopers. It's one of the easiest ways to boost your security."
            };

            _topicResponses["2fa"] = new List<string>
            {
                $"{GetUserName()}, two-factor authentication adds an extra layer of security beyond just a password. It's one of the most effective security measures you can take.",
                $"For the best security, use an authenticator app rather than SMS for two-factor authentication. What do you currently use, {GetUserName()}?",
                $"{GetUserName()}, consider getting a physical security key for the strongest form of two-factor authentication. It's especially useful for your most important accounts."
            };

            _topicResponses["update"] = new List<string>
            {
                $"{GetUserName()}, software updates often include critical security patches. Don't delay installing them!",
                $"Enable automatic updates for your operating system and important applications whenever possible. It's one less thing to worry about, {GetUserName()}.",
                $"Outdated software is one of the most common ways hackers gain access to systems. {GetUserName()}, when was the last time you checked for updates?"
            };

            _topicResponses["ransomware"] = new List<string>
            {
                $"{GetUserName()}, ransomware encrypts your files and demands payment for their release. Regular backups are your best defense against this threat.",
                $"Never pay ransomware attackers - there's no guarantee you'll get your files back, and it funds criminal activity. {GetUserName()}, do you have a backup strategy in place?",
                $"Educate your family about suspicious email attachments to prevent ransomware infections. {GetUserName()}, would you like some tips on how to explain this to others?"
            };

            _topicResponses["social engineering"] = new List<string>
            {
                $"{GetUserName()}, social engineering manipulates people into revealing confidential information. Always verify identities before sharing sensitive data.",
                $"Common social engineering tactics include pretexting, baiting, and tailgating into secure areas. {GetUserName()}, have you encountered any of these?",
                $"If someone calls claiming to be from IT, ask for verification details and call back on an official number. {GetUserName()}, this simple step can prevent many scams."
            };

            _topicResponses["encryption"] = new List<string>
            {
                $"{GetUserName()}, encryption scrambles your data so only authorized parties can read it. Modern encryption like AES-256 is virtually unbreakable when implemented correctly.",
                $"End-to-end encryption ensures only you and the person you're communicating with can read messages. {GetUserName()}, this is why I recommend apps like Signal for sensitive conversations.",
                $"For file encryption, tools like VeraCrypt can create encrypted containers that protect your sensitive documents. {GetUserName()}, would you like me to walk you through setting this up?"
            };
        }

        private void InitializeSentimentResponses()
        {
            _sentimentResponses["worried"] = $"I understand this can be concerning, {GetUserName()}. Let's work through it together to help you feel more secure.";
            _sentimentResponses["frustrated"] = $"I hear your frustration, {GetUserName()}. Cybersecurity can be complex, but we'll take it step by step to resolve this.";
            _sentimentResponses["confused"] = $"No worries at all, {GetUserName()}! This can be confusing at first. Let me explain it in a simpler way that might help.";
            _sentimentResponses["excited"] = $"That's wonderful enthusiasm, {GetUserName()}! Cybersecurity is indeed fascinating and so important in today's world.";
            _sentimentResponses["happy"] = $"I'm genuinely glad you're feeling positive about this, {GetUserName()}! Your attitude will make learning these concepts much easier.";
            _sentimentResponses["sad"] = $"I'm sorry to hear you're feeling down about this, {GetUserName()}. Let's tackle this challenge together - I'm here to help.";
            _sentimentResponses["angry"] = $"I completely understand your frustration, {GetUserName()}. Cybersecurity issues can be incredibly aggravating. Let's work through this calmly.";
            _sentimentResponses["neutral"] = $"Let me share some helpful information about this, {GetUserName()}.";
            _sentimentResponses["anxious"] = $"I sense you're feeling anxious about this, {GetUserName()}. Take a deep breath - we'll go through this carefully to address your concerns.";
            _sentimentResponses["overwhelmed"] = $"It sounds like you're feeling overwhelmed, {GetUserName()}. Let's break this down into smaller, more manageable steps.";
        }

        private void InitializeFollowUpQuestions()
        {
            _followUpQuestions["password"] = new List<string>
            {
                $"{GetUserName()}, would you like me to recommend some excellent password managers that might work for you?",
                $"Should I explain how to create a strong passphrase in more detail, {GetUserName()}?",
                $"{GetUserName()}, are you currently using any password management tools? I'd love to hear about your experience."
            };

            _followUpQuestions["phishing"] = new List<string>
            {
                $"{GetUserName()}, would it help if I shared some real-world examples of phishing emails to help you recognize them?",
                $"Should I explain how to report phishing attempts, {GetUserName()}? It's an important skill to have.",
                $"{GetUserName()}, are you more concerned about phishing in email or other platforms like text messages?"
            };

            _followUpQuestions["privacy"] = new List<string>
            {
                $"{GetUserName()}, would you like me to recommend some privacy tools that align with your specific needs?",
                $"Should I walk you through checking your current privacy settings step-by-step, {GetUserName()}?",
                $"{GetUserName()}, are you particularly concerned about social media privacy or privacy in general?"
            };

            _followUpQuestions["encryption"] = new List<string>
            {
                $"{GetUserName()}, would you like me to explain how to use encryption for your emails in a practical way?",
                $"Should I recommend some user-friendly encryption tools that might suit your needs, {GetUserName()}?",
                $"{GetUserName()}, are you interested in learning about different encryption algorithms or just how to use encryption tools?"
            };

            _followUpQuestions["general"] = new List<string>
            {
                $"{GetUserName()}, would you like me to go into more detail about this topic?",
                $"Should I provide some real-world examples to make this clearer, {GetUserName()}?",
                $"{GetUserName()}, would practical steps to implement this security measure be helpful right now?",
                $"Is there another aspect of this topic you'd like to explore together, {GetUserName()}?"
            };
        }

        private void InitializeQuizQuestions()
        {
            _quizQuestions.Add(new QuizQuestion
            {
                QuestionText = "What should you do if you receive an email asking for your password?",
                Options = new List<string>
                {
                    "Reply with your password",
                    "Delete the email",
                    "Report the email as phishing",
                    "Ignore it"
                },
                CorrectAnswerIndex = 2,
                Explanation = $"Excellent choice, {GetUserName()}! Reporting phishing emails helps protect others from the same scam. Legitimate companies will never ask for your password via email."
            });

            _quizQuestions.Add(new QuizQuestion
            {
                QuestionText = "Which of these is the most secure password?",
                Options = new List<string>
                {
                    "password123",
                    "P@ssw0rd!",
                    "CorrectHorseBatteryStaple",
                    "12345678"
                },
                CorrectAnswerIndex = 2,
                Explanation = $"Great job, {GetUserName()}! A long passphrase is more secure than complex but short passwords. Did you know it would take about 3 quattuordecillion years to crack 'CorrectHorseBatteryStaple'?"
            });

            _quizQuestions.Add(new QuizQuestion
            {
                QuestionText = "What's the main purpose of a VPN?",
                Options = new List<string>
                {
                    "To make your internet faster",
                    "To encrypt your internet connection",
                    "To block all ads",
                    "To scan for viruses"
                },
                CorrectAnswerIndex = 1,
                Explanation = $"That's correct, {GetUserName()}! VPNs encrypt your connection to protect your privacy. While some VPNs offer additional features, encryption is their primary purpose."
            });

            _quizQuestions.Add(new QuizQuestion
            {
                QuestionText = "When should you install software updates?",
                Options = new List<string>
                {
                    "Only when you have time",
                    "Immediately when available",
                    "Never - they might break things",
                    "Only for your operating system"
                },
                CorrectAnswerIndex = 1,
                Explanation = $"Spot on, {GetUserName()}! Updates often contain critical security patches. In fact, many major cyber attacks exploit vulnerabilities for which updates were already available."
            });

            _quizQuestions.Add(new QuizQuestion
            {
                QuestionText = "What's the safest way to handle public WiFi?",
                Options = new List<string>
                {
                    "Use it for all activities",
                    "Only use websites that don't require login",
                    "Use a VPN when on public WiFi",
                    "Avoid it completely"
                },
                CorrectAnswerIndex = 2,
                Explanation = $"Perfect answer, {GetUserName()}! A VPN protects your data on public networks. While avoiding public WiFi is safest, a VPN makes it much more secure when you need to use it."
            });

            _quizQuestions.Add(new QuizQuestion
            {
                QuestionText = "What does two-factor authentication add to your security?",
                Options = new List<string>
                {
                    "A second password",
                    "An additional verification step",
                    "Automatic password changes",
                    "Encrypted email"
                },
                CorrectAnswerIndex = 1,
                Explanation = $"Exactly right, {GetUserName()}! Two-factor authentication adds an extra verification step, making it much harder for attackers to access your accounts."
            });

            _quizQuestions.Add(new QuizQuestion
            {
                QuestionText = "Why shouldn't you use the same password everywhere?",
                Options = new List<string>
                {
                    "It's harder to remember",
                    "If one site is breached, all accounts are at risk",
                    "Some sites require unique passwords",
                    "It's against the law"
                },
                CorrectAnswerIndex = 1,
                Explanation = $"You got it, {GetUserName()}! Password reuse is dangerous because a breach on one site can compromise all your accounts. This is called credential stuffing."
            });
        }

        private string GetUserName()
        {
            return string.IsNullOrEmpty(_userMemory.Name) ? "" : $"{_userMemory.Name}";
        }

        public void HandleResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                _display.Show($"{GetUserName()}, I didn't catch that. Could you please rephrase your question?");
                return;
            }

            // Add to conversation history
            AddToConversationHistory(input);

            // Detect and respond to user sentiment
            DetectSentiment(input);

            // Handle exit command
            if (input.Contains("exit") || input.Contains("quit") || input.Contains("bye"))
            {
                HandleExit();
                return;
            }

            // Handle help command
            if (input.Contains("help"))
            {
                ShowHelp();
                return;
            }

            // Handle thank you
            if (input.Contains("thank"))
            {
                HandleThanks();
                return;
            }

            // Handle quiz responses
            if (_userMemory.InQuizMode && HandleQuizResponse(input))
            {
                return;
            }

            // Handle task management
            if (HandleTaskManagement(input))
            {
                return;
            }

            // Handle activity log request
            if (HandleActivityLogRequest(input))
            {
                return;
            }

            // Handle yes/no responses to follow-up questions
            if (HandleFollowUpResponses(input))
            {
                return;
            }

            // Handle requests for more information
            if (HandleMoreInfoRequests(input))
            {
                return;
            }

            // Check if user is asking about a remembered interest
            if (CheckRememberedInterests(input))
            {
                return;
            }

            // Handle specific topics and personal details
            if (HandleSpecificTopics(input))
            {
                return;
            }

            // Default response for unrecognized input
            HandleUnknownInput();
        }

        private void AddToConversationHistory(string input)
        {
            _conversationHistory.Add(input);
            if (_conversationHistory.Count > MaxHistoryItems)
            {
                _conversationHistory.RemoveAt(0);
            }
        }

        private bool HandleQuizResponse(string input)
        {
            if (input.Equals("exit quiz", StringComparison.OrdinalIgnoreCase))
            {
                _userMemory.InQuizMode = false;
                _userMemory.CurrentQuiz = null;
                _display.Show($"{GetUserName()}, I've exited the quiz. What would you like to discuss now?");
                return true;
            }

            if (int.TryParse(input, out int answer) && answer >= 1 && answer <= 4)
            {
                var isCorrect = _userMemory.CurrentQuiz.AnswerCurrentQuestion(answer - 1);
                var currentQuestion = _userMemory.CurrentQuiz.GetCurrentQuestion();

                if (isCorrect)
                {
                    _display.ShowWithColor($"✓ Correct, {GetUserName()}! {currentQuestion.Explanation}", Color.Green);
                    _userMemory.LogActivity($"{GetUserName()} answered quiz question correctly: {currentQuestion.QuestionText}");
                }
                else
                {
                    _display.ShowWithColor($"✗ Incorrect, {GetUserName()}. {currentQuestion.Explanation}", Color.Orange);
                    _userMemory.LogActivity($"{GetUserName()} answered quiz question incorrectly: {currentQuestion.QuestionText}");
                }

                if (_userMemory.CurrentQuiz.IsCompleted)
                {
                    double scorePercent = (_userMemory.CurrentQuiz.Score * 100.0) / _userMemory.CurrentQuiz.Questions.Count;
                    string feedback;

                    if (scorePercent >= 80)
                        feedback = $"Outstanding work, {GetUserName()}! You're a cybersecurity expert! 🎉";
                    else if (scorePercent >= 60)
                        feedback = $"Good job, {GetUserName()}! You have solid cybersecurity knowledge. Keep learning!";
                    else if (scorePercent >= 40)
                        feedback = $"Not bad at all, {GetUserName()}! With a bit more practice, you'll be a security pro in no time.";
                    else
                        feedback = $"Keep practicing, {GetUserName()}! Everyone starts somewhere, and cybersecurity is important to learn.";

                    _display.Show($"{GetUserName()}, quiz completed! Your score: {_userMemory.CurrentQuiz.Score}/{_userMemory.CurrentQuiz.Questions.Count}");
                    _display.Show(feedback);
                    _userMemory.InQuizMode = false;
                    _userMemory.LogActivity($"{GetUserName()} completed quiz with score {_userMemory.CurrentQuiz.Score}/{_userMemory.CurrentQuiz.Questions.Count}");
                    _userMemory.CurrentQuiz = null;
                }
                else
                {
                    ShowNextQuizQuestion();
                }

                return true;
            }

            return false;
        }

        private void ShowNextQuizQuestion()
        {
            var question = _userMemory.CurrentQuiz.GetCurrentQuestion();
            _display.ShowWithColor($"{GetUserName()}, here's your next question:", Color.DarkBlue);
            _display.ShowWithColor(question.QuestionText, Color.Blue);

            for (int i = 0; i < question.Options.Count; i++)
            {
                _display.Show($"{i + 1}. {question.Options[i]}");
            }
        }

        private bool HandleTaskManagement(string input)
        {
            input = input.ToLower();

            // Add task
            if (input.Contains("add task") || input.Contains("create task") || input.Contains("new task") ||
                input.Contains("set reminder") || input.Contains("add reminder"))
            {
                string taskTitle = ExtractTaskTitle(input);
                if (string.IsNullOrEmpty(taskTitle))
                {
                    _display.Show($"{GetUserName()}, what would you like to name this task?");
                    _userMemory.AwaitingTaskTitle = true;
                    return true;
                }

                var task = new CyberTask { Title = taskTitle };
                _userMemory.Tasks.Add(task);
                _userMemory.CurrentTask = task;
                _userMemory.LogActivity($"{GetUserName()} added task: {taskTitle}");

                _display.Show($"{GetUserName()}, task '{taskTitle}' created. Would you like to add a description? (yes/no)");
                _userMemory.AwaitingTaskDescription = true;
                return true;
            }

            // List tasks
            if (input.Contains("list tasks") || input.Contains("show tasks") || input.Contains("my tasks"))
            {
                if (_userMemory.Tasks.Count == 0)
                {
                    _display.Show($"{GetUserName()}, you don't have any tasks yet. Would you like to create one?");
                    return true;
                }

                _display.Show($"{GetUserName()}, here are your current tasks:");
                foreach (var task in _userMemory.Tasks)
                {
                    string status = task.IsCompleted ? "[✓]" : "[ ]";
                    string dueDate = task.DueDate.HasValue ? $" (due {task.DueDate.Value.ToShortDateString()})" : "";
                    _display.Show($"{status} {task.Title}{dueDate}");
                }
                return true;
            }

            // Complete task
            if (input.Contains("complete task") || input.Contains("finish task") || input.Contains("mark task as done"))
            {
                string taskTitle = ExtractTaskTitle(input);
                if (string.IsNullOrEmpty(taskTitle))
                {
                    _display.Show($"{GetUserName()}, which task would you like to mark as complete?");
                    return true;
                }

                var task = _userMemory.Tasks.FirstOrDefault(t => t.Title.Equals(taskTitle, StringComparison.OrdinalIgnoreCase));
                if (task != null)
                {
                    task.IsCompleted = true;
                    _display.ShowWithColor($"✓ Task '{task.Title}' marked as completed! Great job, {GetUserName()}!", Color.Green);
                    _userMemory.LogActivity($"{GetUserName()} completed task: {task.Title}");
                    return true;
                }

                _display.Show($"{GetUserName()}, I couldn't find a task with title '{taskTitle}'. Would you like to see your task list?");
                return true;
            }

            // Delete task
            if (input.Contains("delete task") || input.Contains("remove task"))
            {
                string taskTitle = ExtractTaskTitle(input);
                if (string.IsNullOrEmpty(taskTitle))
                {
                    _display.Show($"{GetUserName()}, which task would you like to delete?");
                    return true;
                }

                var task = _userMemory.Tasks.FirstOrDefault(t => t.Title.Equals(taskTitle, StringComparison.OrdinalIgnoreCase));
                if (task != null)
                {
                    _userMemory.Tasks.Remove(task);
                    _display.Show($"Task '{task.Title}' has been deleted, {GetUserName()}.");
                    _userMemory.LogActivity($"{GetUserName()} deleted task: {task.Title}");
                    return true;
                }

                _display.Show($"{GetUserName()}, I couldn't find a task with title '{taskTitle}'. Would you like to see your task list?");
                return true;
            }

            // Handle task follow-ups
            if (_userMemory.AwaitingTaskTitle)
            {
                var task = new CyberTask { Title = input };
                _userMemory.Tasks.Add(task);
                _userMemory.CurrentTask = task;
                _userMemory.AwaitingTaskTitle = false;
                _userMemory.LogActivity($"{GetUserName()} added task: {input}");

                _display.Show($"{GetUserName()}, task '{input}' created. Would you like to add a description? (yes/no)");
                _userMemory.AwaitingTaskDescription = true;
                return true;
            }

            if (_userMemory.AwaitingTaskDescription)
            {
                if (input.Equals("yes", StringComparison.OrdinalIgnoreCase))
                {
                    _display.Show($"{GetUserName()}, please enter the task description:");
                    _userMemory.AwaitingTaskDescription = false;
                    _userMemory.AwaitingTaskDescriptionInput = true;
                    return true;
                }
                else
                {
                    _display.Show($"{GetUserName()}, would you like to set a due date for this task? (yes/no)");
                    _userMemory.AwaitingTaskDescription = false;
                    _userMemory.AwaitingTaskDueDate = true;
                    return true;
                }
            }

            if (_userMemory.AwaitingTaskDescriptionInput)
            {
                _userMemory.CurrentTask.Description = input;
                _userMemory.AwaitingTaskDescriptionInput = false;
                _display.Show($"{GetUserName()}, description added. Would you like to set a due date for this task? (yes/no)");
                _userMemory.AwaitingTaskDueDate = true;
                return true;
            }

            if (_userMemory.AwaitingTaskDueDate)
            {
                if (input.Equals("yes", StringComparison.OrdinalIgnoreCase))
                {
                    _display.Show($"{GetUserName()}, when is this task due? (e.g., 'tomorrow', 'next Monday', 'in 3 days')");
                    _userMemory.AwaitingTaskDueDate = false;
                    _userMemory.AwaitingTaskDueDateInput = true;
                    return true;
                }
                else
                {
                    _userMemory.AwaitingTaskDueDate = false;
                    _userMemory.CurrentTask = null;
                    _display.Show($"{GetUserName()}, task created successfully! You can view your tasks anytime by saying 'list tasks'.");
                    return true;
                }
            }

            if (_userMemory.AwaitingTaskDueDateInput)
            {
                if (DateTime.TryParse(input, out DateTime dueDate))
                {
                    _userMemory.CurrentTask.DueDate = dueDate;
                    _userMemory.CurrentTask.HasReminder = true;
                    _userMemory.LogActivity($"{GetUserName()} set due date for task '{_userMemory.CurrentTask.Title}': {dueDate.ToShortDateString()}");
                    _display.Show($"{GetUserName()}, due date set for {dueDate.ToShortDateString()}. Task created successfully!");
                }
                else
                {
                    _display.Show($"{GetUserName()}, I couldn't understand that date. Please try again with a specific date like 'March 15' or 'next Monday'.");
                    return true;
                }

                _userMemory.AwaitingTaskDueDateInput = false;
                _userMemory.CurrentTask = null;
                return true;
            }

            return false;
        }

        private bool HandleActivityLogRequest(string input)
        {
            if (input.Contains("activity log") || input.Contains("show history") || input.Contains("what have you done") || input.Contains("my activity"))
            {
                var activities = _userMemory.GetRecentActivities();
                if (activities.Count == 0)
                {
                    _display.Show($"{GetUserName()}, your activity log is empty. Let's get started with something!");
                    return true;
                }

                _display.Show($"{GetUserName()}, here's your recent activity:");
                foreach (var activity in activities)
                {
                    _display.Show($"- {activity}");
                }
                return true;
            }

            return false;
        }

        private string ExtractTaskTitle(string input)
        {
            string[] markers = { "add task", "create task", "new task", "set reminder", "add reminder",
                               "complete task", "finish task", "mark task as done",
                               "delete task", "remove task" };

            foreach (var marker in markers)
            {
                if (input.Contains(marker))
                {
                    int start = input.IndexOf(marker) + marker.Length;
                    if (start < input.Length)
                    {
                        string remainder = input.Substring(start).Trim();
                        if (remainder.StartsWith("to ")) remainder = remainder.Substring(3);
                        if (remainder.StartsWith("called ")) remainder = remainder.Substring(7);
                        if (remainder.StartsWith("named ")) remainder = remainder.Substring(6);
                        if (remainder.StartsWith("about ")) remainder = remainder.Substring(6);
                        return remainder;
                    }
                }
            }

            return null;
        }

        private void HandleExit()
        {
            string[] goodbyes = {
                $"Goodbye, {GetUserName()}! Stay safe online and don't hesitate to return if you have more questions.",
                $"Until next time, {GetUserName()}! Remember to keep your software updated for better security.",
                $"Farewell, {GetUserName()}! If you think of any cybersecurity questions later, I'm here to help.",
                $"See you soon, {GetUserName()}! Consider enabling two-factor authentication on your important accounts if you haven't already."
            };

            _display.Show(goodbyes[_random.Next(goodbyes.Length)]);
            Application.Exit();
        }

        public void ShowHelp()
        {
            _display.Show($"{GetUserName()}, here's what I can help you with:");
            _display.Show("- Cybersecurity advice (passwords, phishing, privacy, etc.)");
            _display.Show("- Task management (create, complete, and track security-related tasks)");
            _display.Show("- Interactive cybersecurity quiz (test and improve your knowledge)");
            _display.Show("- Activity log (review your recent security activities)");
            _display.Show($"Just ask me anything about cybersecurity, {GetUserName()}! For example:");
            _display.Show("- 'How can I create a strong password?'");
            _display.Show("- 'What should I do about a suspicious email?'");
            _display.Show("- 'Let's start a cybersecurity quiz'");
        }

        private void HandleThanks()
        {
            string[] responses = {
                $"You're very welcome, {GetUserName()}! I'm always happy to help with your cybersecurity questions.",
                $"My pleasure, {GetUserName()}! Don't hesitate to ask if you have more questions later.",
                $"Glad I could assist, {GetUserName()}! Remember, staying informed is the first step to staying secure.",
                $"Anytime, {GetUserName()}! Your dedication to cybersecurity is truly commendable."
            };
            _display.Show(responses[_random.Next(responses.Length)]);
        }

        private void DetectSentiment(string input)
        {
            input = input.ToLower();
            if (input.Contains("worried") || input.Contains("concerned") || input.Contains("scared") || input.Contains("nervous"))
            {
                _userMemory.Sentiment = "worried";
            }
            else if (input.Contains("happy") || input.Contains("excited") || input.Contains("great") || input.Contains("awesome"))
            {
                _userMemory.Sentiment = "happy";
            }
            else if (input.Contains("angry") || input.Contains("frustrated") || input.Contains("mad") || input.Contains("annoyed"))
            {
                _userMemory.Sentiment = "angry";
            }
            else if (input.Contains("sad") || input.Contains("upset") || input.Contains("depressed") || input.Contains("disappointed"))
            {
                _userMemory.Sentiment = "sad";
            }
            else if (input.Contains("confused") || input.Contains("unsure") || input.Contains("don't know") || input.Contains("dont know"))
            {
                _userMemory.Sentiment = "confused";
            }
            else if (input.Contains("anxious") || input.Contains("stressed") || input.Contains("panicked"))
            {
                _userMemory.Sentiment = "anxious";
            }
            else if (input.Contains("overwhelmed") || input.Contains("too much") || input.Contains("can't handle"))
            {
                _userMemory.Sentiment = "overwhelmed";
            }
            else
            {
                _userMemory.Sentiment = "neutral";
            }

            // Show sentiment response if not neutral
            if (_userMemory.Sentiment != "neutral" && !_userMemory.InQuizMode)
            {
                _display.Show(_sentimentResponses[_userMemory.Sentiment]);
            }
        }

        private bool HandleFollowUpResponses(string input)
        {
            if (_userMemory.AwaitingFollowUpResponse)
            {
                if (input.Equals("yes", StringComparison.OrdinalIgnoreCase) || input.Contains("sure") || input.Contains("please"))
                {
                    if (_topicResponses.ContainsKey(_userMemory.CurrentTopic))
                    {
                        var responses = _topicResponses[_userMemory.CurrentTopic];
                        string response = responses[_random.Next(responses.Count)];

                        // Check if we've discussed this before
                        if (_conversationHistory.Any(h => h.Contains(_userMemory.CurrentTopic, StringComparison.OrdinalIgnoreCase)))
                        {
                            response = $"As we discussed before, {GetUserName()}, {response.ToLower()}";
                        }

                        _display.Show(response);
                    }
                }
                else if (input.Equals("no", StringComparison.OrdinalIgnoreCase) || input.Contains("not now"))
                {
                    _display.Show($"No problem at all, {GetUserName()}. Just let me know if you change your mind.");
                }
                else
                {
                    _display.Show($"{GetUserName()}, I wasn't sure if that was a yes or no. For now, I'll assume you'd like to move on.");
                }

                _userMemory.AwaitingFollowUpResponse = false;
                return true;
            }
            return false;
        }

        private bool HandleMoreInfoRequests(string input)
        {
            if (input.Contains("more info") || input.Contains("tell me more") || input.Contains("explain") || input.Contains("elaborate"))
            {
                if (!string.IsNullOrEmpty(_userMemory.CurrentTopic) && _topicResponses.ContainsKey(_userMemory.CurrentTopic))
                {
                    var responses = _topicResponses[_userMemory.CurrentTopic];
                    string response = responses[_random.Next(responses.Count)];

                    // Personalize based on previous interactions
                    if (_userMemory.Interests.Contains(_userMemory.CurrentTopic))
                    {
                        response = $"Since you're interested in {_userMemory.CurrentTopic}, {GetUserName()}, {response.ToLower()}";
                    }

                    _display.Show(response);
                    return true;
                }

                _display.Show($"{GetUserName()}, I'd be happy to provide more information. What specifically would you like me to explain in more detail?");
                return true;
            }
            return false;
        }

        private bool CheckRememberedInterests(string input)
        {
            foreach (var interest in _userMemory.Interests)
            {
                if (input.Contains(interest, StringComparison.OrdinalIgnoreCase))
                {
                    if (_topicResponses.ContainsKey(interest))
                    {
                        var responses = _topicResponses[interest];
                        _display.Show($"I remember you're interested in {interest}, {GetUserName()}. {responses[_random.Next(responses.Count)]}");
                        return true;
                    }
                }
            }
            return false;
        }

        private bool HandleSpecificTopics(string input)
        {
            // Improved name detection with more natural patterns using regex
            if (string.IsNullOrEmpty(_userMemory.Name))
            {
                var namePatterns = new[] {
                    new Regex(@"my name is (\w+(?: \w+){0,2})", RegexOptions.IgnoreCase),
                    new Regex(@"i['`]?m (\w+(?: \w+){0,2})", RegexOptions.IgnoreCase),
                    new Regex(@"i am (\w+(?: \w+){0,2})", RegexOptions.IgnoreCase),
                    new Regex(@"you can call me (\w+(?: \w+){0,2})", RegexOptions.IgnoreCase),
                    new Regex(@"call me (\w+(?: \w+){0,2})", RegexOptions.IgnoreCase),
                    new Regex(@"name['`]?s (\w+(?: \w+){0,2})", RegexOptions.IgnoreCase)
                };

                foreach (var pattern in namePatterns)
                {
                    var match = pattern.Match(input);
                    if (match.Success && match.Groups.Count > 1)
                    {
                        string name = match.Groups[1].Value.Trim();
                        if (!string.IsNullOrWhiteSpace(name))
                        {
                            _userMemory.Name = name;
                            string[] greetings = {
                                $"Nice to meet you, {name}! I'm Cyberbot, your personal cybersecurity assistant. How can I help you today?",
                                $"Great to meet you, {name}! I'm here to help with all your cybersecurity questions. What's on your mind?",
                                $"Hello {name}! I'll be happy to assist you with cybersecurity. Where shall we start?",
                                $"Welcome, {name}! I'm your cybersecurity guide. What would you like to discuss first?"
                            };
                            _display.Show(greetings[_random.Next(greetings.Length)]);
                            _userMemory.LogActivity($"User name set: {name}");
                            return true;
                        }
                    }
                }
            }

            // Check for specific topics with context awareness
            foreach (var topic in _topicResponses.Keys)
            {
                if (input.Contains(topic, StringComparison.OrdinalIgnoreCase))
                {
                    _userMemory.CurrentTopic = topic;
                    _userMemory.AddInterest(topic);

                    var responses = _topicResponses[topic];
                    string response = responses[_random.Next(responses.Count)];

                    // Check if we've discussed this topic before
                    if (_conversationHistory.Any(h => h.Contains(topic, StringComparison.OrdinalIgnoreCase)))
                    {
                        response = $"As we discussed before, {GetUserName()}, {response.ToLower()}";
                    }

                    _display.Show(response);

                    if (_followUpQuestions.ContainsKey(topic))
                    {
                        string followUp = _followUpQuestions[topic][_random.Next(_followUpQuestions[topic].Count)];
                        _display.Show(followUp);
                        _userMemory.AwaitingFollowUpResponse = true;
                    }

                    return true;
                }
            }

            // Handle personal details with more natural patterns
            var emailPattern = new Regex(@"(?:my )?email(?: address)? is ([^\s@]+@[^\s@]+\.[^\s@]+)", RegexOptions.IgnoreCase);
            var emailMatch = emailPattern.Match(input);
            if (emailMatch.Success && emailMatch.Groups.Count > 1)
            {
                string email = emailMatch.Groups[1].Value;
                _userMemory.AddPersonalDetail("email", email);
                _display.Show($"I've noted your email, {GetUserName()}. Remember to keep it secure! How can I help you with cybersecurity?");
                return true;
            }

            var phonePattern = new Regex(@"(?:my )?(?:phone|mobile|number)(?: number)? is ([+\d\s\-\(\)]{7,})", RegexOptions.IgnoreCase);
            var phoneMatch = phonePattern.Match(input);
            if (phoneMatch.Success && phoneMatch.Groups.Count > 1)
            {
                string phone = phoneMatch.Groups[1].Value;
                _userMemory.AddPersonalDetail("phone", phone);
                _display.Show($"I've noted your phone number, {GetUserName()}. Be cautious about sharing it online. What cybersecurity questions can I answer?");
                return true;
            }

            // Handle other personal details
            if (input.Contains("my username is", StringComparison.OrdinalIgnoreCase) ||
                input.Contains("my login is", StringComparison.OrdinalIgnoreCase))
            {
                string value = input.Split(new[] { "is" }, StringSplitOptions.RemoveEmptyEntries).Last().Trim();
                _userMemory.AddPersonalDetail("username", value);
                _display.Show($"I've noted your username, {GetUserName()}. Remember to never share your password with anyone! What else can I help with?");
                return true;
            }

            return false;
        }

        private void HandleUnknownInput()
        {
            string[] responses = {
                $"I'm not sure I understand, {GetUserName()}. Could you rephrase that or ask me about cybersecurity?",
                $"I specialize in cybersecurity topics, {GetUserName()}. Could you ask me about passwords, phishing, or online safety?",
                $"{GetUserName()}, I'm not sure about that. Would you like to take a cybersecurity quiz instead?",
                $"I'm a cybersecurity assistant, {GetUserName()}. Try asking me about creating strong passwords or recognizing phishing attempts.",
                $"That's an interesting question, {GetUserName()}. As a cybersecurity assistant, I can best help with topics like online safety, secure passwords, and privacy protection."
            };

            _display.Show(responses[_random.Next(responses.Length)]);
        }

        public List<QuizQuestion> GetQuizQuestions()
        {
            return _quizQuestions;
        }

        public void StartQuiz()
        {
            _userMemory.CurrentQuiz = new QuizSession();
            _userMemory.CurrentQuiz.Questions.AddRange(_quizQuestions);
            _userMemory.InQuizMode = true;
            _userMemory.LogActivity($"{GetUserName()} started cybersecurity quiz");

            _display.ShowWithColor($"Great choice, {GetUserName()}! Let's test your cybersecurity knowledge.", Color.DarkBlue);
            _display.Show("For each question, just type the number of your answer (1-4). You can say 'exit quiz' at any time.");
            _display.Show("Ready? Here's your first question:");

            ShowNextQuizQuestion();
        }

        public void ShowActivityLog()
        {
            var activities = _userMemory.GetRecentActivities();
            if (activities.Count == 0)
            {
                _display.Show($"{GetUserName()}, your activity log is empty. Let's get started with something!");
                return;
            }

            _display.Show($"{GetUserName()}, here's your recent activity:");
            foreach (var activity in activities)
            {
                _display.Show($"- {activity}");
            }
        }
    }
}