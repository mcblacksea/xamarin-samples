using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Views;
using Android.Widget;
using PaperBoy.Droid.DependencyServices;
using PaperBoy.Interfaces;
using Xamarin.Forms;

[assembly:Dependency(typeof(TextSpeecher))]
namespace PaperBoy.Droid.DependencyServices
{
    public class TextSpeecher : Java.Lang.Object,ITextSpeecher,TextToSpeech.IOnInitListener
    {
        TextToSpeech speaker;
        string toSpeak;
        public TextSpeecher(){ }

        public void Speak(string text)
        {
            var ctx = Forms.Context;
            toSpeak = text;
            if(speaker==null)
            {
                speaker = new TextToSpeech(ctx, this);
            }
            else
            {
                if(Build.VERSION.Release.StartsWith("4"))
                {
                    speaker.Speak(toSpeak, QueueMode.Flush, null);
                }
                else
                {
                    speaker.Speak(toSpeak, QueueMode.Flush, null, null);
                }
            }
        }
        public void OnInit([GeneratedEnum] OperationResult status)
        {
            if(status.Equals(OperationResult.Success))
            {
                if(Build.VERSION.Release.StartsWith("4"))
                {
                    speaker.Speak(toSpeak, QueueMode.Flush, null);
                }
                else
                {
                    speaker.Speak(toSpeak, QueueMode.Flush, null, null);
                }
            }
        }
    }
}