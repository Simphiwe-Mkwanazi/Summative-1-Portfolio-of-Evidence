github link:
https://github.com/Simphiwe-Mkwanazi/Summative-1-Portfolio-of-Evidence/edit/main/README.md 
# Summative-1-Portfolio-of-Evidence
POE_PART_3
Cyberbot - Cybersecurity Assistant
Overview
Cyberbot is a Windows Forms application designed to serve as an interactive cybersecurity assistant. It provides users with cybersecurity advice, manages security-related tasks, and offers an interactive quiz to test and improve cybersecurity knowledge.

Features
Interactive Chat Interface: Users can ask cybersecurity questions and receive personalized responses.

Task Management: Create, track, and complete security-related tasks with reminders.

Cybersecurity Quiz: Test your knowledge with an interactive quiz featuring multiple-choice questions.

Personalization: Remembers user details, interests, and conversation history.

Activity Logging: Tracks user interactions and security activities.

Technical Details
Project Structure
Cyberbot.cs: Main bot class that coordinates all components.

CyberbotResponseHandler.cs: Handles user input and generates appropriate responses.

UserMemory.cs: Manages user data, conversation history, and personal details.

GuiDisplay.cs: Handles all UI display operations.

MediaPlayer.cs: Base class for media playback (with SoundPlayerAdapter implementation).

Models:

CyberTask.cs: Represents a security-related task.

QuizQuestion.cs: Represents a quiz question with options.

QuizSession.cs: Manages an active quiz session.

Forms:

MainForm.cs: Primary application interface with chat, tasks, and quiz tabs.

QuizForm.cs: Dedicated quiz interface.

TaskForm.cs: Form for creating/editing tasks.

Dependencies
.NET 9.0 Windows Forms

System.Media for sound playback

Installation
Ensure you have the .NET 9.0 SDK installed.

Clone or download the repository.

Open the solution in Visual Studio.

Build and run the project.

Usage
Initial Setup:

The bot will ask for your name when first launched.

Chat Interface:

Type your cybersecurity questions in the input box and press Enter.

Supported topics include passwords, phishing, privacy, malware, VPNs, 2FA, and more.

Task Management:

Create security-related tasks with titles, descriptions, and due dates.

Mark tasks as complete or delete them when no longer needed.

Cybersecurity Quiz:

Start the quiz from the Quiz tab.

Answer multiple-choice questions to test your knowledge.

Receive explanations for each answer.

Commands:

"help": Show available commands and topics.

"exit" or "quit": Close the application.

"list tasks": View your current tasks.

"start quiz": Begin the cybersecurity quiz.

Customization
To add more quiz questions or response topics, modify:

InitializeQuizQuestions() in CyberbotResponseHandler.cs

InitializeTopicResponses() in CyberbotResponseHandler.cs

Security Considerations
User data is stored in memory only (cleared when application closes).

Input sanitization is performed to prevent basic injection attacks.

Sensitive data can be cleared using the ClearSensitiveData() method.

License
This project is open-source. Feel free to use and modify it according to your needs.

Screenshots
(Include screenshots of the main interface, chat examples, task management, and quiz if available)

Future Enhancements
Persistent storage for user data

More quiz questions and categories

Additional cybersecurity tools and utilities

Dark/light mode toggle

Multi-language support
