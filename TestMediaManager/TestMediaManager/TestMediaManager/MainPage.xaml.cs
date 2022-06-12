
using MediaManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace TestMediaManager
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(true)]
   public partial class MainPage : ContentPage
   {
      public MainPage()
      {
         InitializeComponent();

         //ToDo: 
         //CrossMediaManager.Current.RepeatMode = MediaManager.Playback.RepeatMode.Off;
         //CrossMediaManager.Current.ShuffleMode = MediaManager.Queue.ShuffleMode.Off;

      

        // CrossMediaManager.Current. PlayingChanged += Current_PlayingChanged;
         CrossMediaManager.Current.BufferedChanged += Current_BufferingChanged;

         CrossMediaManager.Current.MediaItemFinished += Current_MediaItemFinished;
         CrossMediaManager.Current.MediaItemChanged += Current_MediaItemChanged;
      }

      //private void Current_PlayingChanged(object sender, MediaManager.Playback.PlayingChangedEventArgs e)
      //{
      //   //Bug: Is running on Startup without any previous operation
      //   Debug.WriteLine($"Current_PlayingChanged {e.Position}");
      //}

      private void Current_BufferingChanged(object sender, MediaManager.Playback.BufferedChangedEventArgs e)
      {
         throw new NotImplementedException();
      }

      private void Current_MediaItemFinished(object sender, MediaManager.Media.MediaItemEventArgs e)
      {
         throw new NotImplementedException();
      }

      private void Current_MediaItemChanged(object sender, MediaManager.Media.MediaItemEventArgs e)
      {
         Debug.WriteLine("Current_MediaItemChanged");
      }

   

      private async void Button_Audio_Clicked(object sender, EventArgs e)
      {
         //Audio
         //await CrossMediaManager.Current.Play("https://ia800806.us.archive.org/15/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");
         await CrossMediaManager.Current.Play("http://freesound.org/data/previews/273/273629_4068345-lq.mp3");
      }

      private async void Button_Video_Clicked(object sender, EventArgs e)
      {
         //Video
         await CrossMediaManager.Current.Play("http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4");
      }

      public IList<string> Mp3UrlList => new[]
      {
         "https://ia800806.us.archive.org/15/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3",
         "https://ia800605.us.archive.org/32/items/Mp3Playlist_555/CelineDion-IfICould.mp3",
         "https://ia800605.us.archive.org/32/items/Mp3Playlist_555/Daughtry-Homeacoustic.mp3",
         "https://storage.googleapis.com/uamp/The_Kyoto_Connection_-_Wake_Up/01_-_Intro_-_The_Way_Of_Waking_Up_feat_Alan_Watts.mp3",
         "https://aphid.fireside.fm/d/1437767933/02d84890-e58d-43eb-ab4c-26bcc8524289/d9b38b7f-5ede-4ca7-a5d6-a18d5605aba1.mp3"
      };

      private async void Button_PlayMultiple_Clicked(object sender, EventArgs e)
      {
         await CrossMediaManager.Current.Play(Mp3UrlList);
      }

      private async void Button_PlayPause_Clicked(object sender, EventArgs e)
      {
            //await CrossMediaManager.Current.PlayPause();

            switch (CrossMediaManager.Current.State)
            {
                case MediaManager.Player.MediaPlayerState.Stopped:
                case MediaManager.Player.MediaPlayerState.Paused:
                    await CrossMediaManager.Current.Play();
                    break;
                case MediaManager.Player.MediaPlayerState.Playing:
                    await CrossMediaManager.Current.Pause();
                    break;
                default:
                    await CrossMediaManager.Current.Play();
                    break;
            }
        }

      private async void Button_Stop_Clicked(object sender, EventArgs e)
      {
         await CrossMediaManager.Current.Stop();
      }

      private async void Button_StepBackward_Clicked(object sender, EventArgs e)
      {
         await CrossMediaManager.Current.StepBackward();
      }

      private async void Button_StepForward_Clicked(object sender, EventArgs e)
      {
         await CrossMediaManager.Current.StepForward();
      }

      private async void Button_PlayPrevious_Clicked(object sender, EventArgs e)
      {
         await CrossMediaManager.Current.PlayPrevious();
      }

      private async void Button_PlayNext_Clicked(object sender, EventArgs e)
      {
         await CrossMediaManager.Current.PlayNext();
      }
   }
}
