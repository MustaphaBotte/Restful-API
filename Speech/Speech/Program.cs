using SpeechLib;
namespace Speech
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SpVoice voice = new SpVoice();
            voice.Rate=1;
            voice.Volume = 80;
            voice.Rate = 2;
            voice.Priority = SpeechVoicePriority.SVPAlert;

            string text = "hello every one";
            voice.Speak(text);

        }
    }
}
