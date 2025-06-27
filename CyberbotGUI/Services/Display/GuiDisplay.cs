using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberbotGUI.Services.Display
{
    public class GuiDisplay
    {
        private readonly MainForm _mainForm;

        public GuiDisplay(MainForm mainForm)
        {
            _mainForm = mainForm;
        }

        public void Show(string message)
        {
            _mainForm.AddMessageToChat("Cyberbot", message, false);
        }

        public void ShowTypingEffect(string text, int delay = 25)
        {
            // For GUI, we'll just show the message immediately
            Show(text);
        }

        public void ShowWithColor(string message, Color color)
        {
            _mainForm.ShowWithColor(message, color);
        }
    }
}