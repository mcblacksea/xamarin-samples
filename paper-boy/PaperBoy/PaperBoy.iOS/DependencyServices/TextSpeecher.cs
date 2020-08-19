using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AVFoundation;
using Foundation;
using PaperBoy.Interfaces;
using PaperBoy.iOS.DependencyServices;
using UIKit;
using Xamarin.Forms;

[assembly:Dependency(typeof(TextSpeecher))]
namespace PaperBoy.iOS.DependencyServices
{
    public class TextSpeecher : ITextSpeecher
    {
        public TextSpeecher()
        {
                
        }
        public void Speak(string text)
        {
            var speechSynthersizer = new AVSpeechSynthesizer();
            var speechUtterance = new AVSpeechUtterance(text)
            {
                Rate = AVSpeechUtterance.MaximumSpeechRate / 4,
                Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
                Volume = 0.5f,
                PitchMultiplier = 1.0F
            };
            speechSynthersizer.SpeakUtterance(speechUtterance);
        }
    }
}