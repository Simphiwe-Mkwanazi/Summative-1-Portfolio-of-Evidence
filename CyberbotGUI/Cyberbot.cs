using CyberbotGUI.Models;
using CyberbotGUI.Services.Display;
using CyberbotGUI.Services.Media;
using CyberbotGUI.Services.Memory;
using CyberbotGUI.Services.ResponseHandlers;
using System;

namespace CyberbotGUI
{
    public class Cyberbot
    {
        private readonly GuiDisplay _display;
        private readonly MediaPlayer _mediaPlayer;
        private readonly CyberbotResponseHandler _responseHandler;
        private readonly UserMemory _userMemory;

        public Cyberbot(GuiDisplay display, MediaPlayer mediaPlayer, CyberbotResponseHandler responseHandler, UserMemory userMemory)
        {
            _display = display ?? throw new ArgumentNullException(nameof(display));
            _mediaPlayer = mediaPlayer ?? throw new ArgumentNullException(nameof(mediaPlayer));
            _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
            _userMemory = userMemory ?? throw new ArgumentNullException(nameof(userMemory));
        }

        public void Initialize()
        {
            try
            {
                _mediaPlayer.Play("welcome.wav");
            }
            catch (Exception ex)
            {
                _display.Show($"Audio error: {ex.Message}");
            }

            _display.ShowTypingEffect("Welcome to Cyberbot - Your Cybersecurity Assistant");
        }

        public void ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return;

            // Add to conversation history
            _userMemory.AddConversation(input, "");

            // Process through response handler
            _responseHandler.HandleResponse(input);
        }

        public void ShowHelp()
        {
            _responseHandler.ShowHelp();
        }

        public void StartQuiz()
        {
            _responseHandler.StartQuiz();
        }

        public void ShowActivityLog()
        {
            _responseHandler.ShowActivityLog();
        }

        public void AddConversation(string input, string response)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                _userMemory.AddConversation(input, response);
            }
        }

        public void ClearConversationHistory()
        {
            _userMemory.ConversationHistory.Clear();
        }

        public void SetUserName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                _userMemory.Name = name;
                _display.Show($"Nice to meet you, {name}! How can I help you today?");
            }
        }

        public void AddPersonalDetail(string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
            {
                _userMemory.AddPersonalDetail(key, value);
            }
        }

        public string GetPersonalDetail(string key)
        {
            return _userMemory.GetPersonalDetail(key);
        }

        public void AddInterest(string interest)
        {
            if (!string.IsNullOrWhiteSpace(interest))
            {
                _userMemory.AddInterest(interest);
            }
        }

        public void ClearSensitiveData()
        {
            _userMemory.ClearSensitiveData();
        }
    }
}