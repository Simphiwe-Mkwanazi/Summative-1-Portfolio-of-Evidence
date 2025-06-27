using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace CyberbotGUI.Services.Media
{
    // Base class for all media players
    public abstract class MediaPlayer
    {
        public abstract void Play(string path);
    }

    // Specialized media player for sound files
    public class SoundPlayerAdapter : MediaPlayer
    {
        public override void Play(string path)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(path))
                {
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"I'm sorry! Unable to play the audio greeting: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}