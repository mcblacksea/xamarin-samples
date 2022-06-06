using LibVLCSharp.Shared;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace VlcXamSample
{
    /// <summary>
    /// Represents the main viewmodel.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
        }

        private LibVLC _libVLC;

        /// <summary>
        /// Gets the <see cref="LibVLCSharp.Shared.LibVLC"/> instance.
        /// </summary>
        public LibVLC LibVLC
        {
            get => _libVLC;
            private set => Set(nameof(LibVLC), ref _libVLC, value);
        }

        private MediaPlayer _mediaPlayer;
        /// <summary>
        /// Gets the <see cref="LibVLCSharp.Shared.MediaPlayer"/> instance.
        /// </summary>
        public MediaPlayer MediaPlayer
        {
            get => _mediaPlayer;
            private set => Set(nameof(MediaPlayer), ref _mediaPlayer, value);
        }

        /// <summary>
        /// Initialize LibVLC and playback when page appears
        /// </summary>
        public async void OnAppearing()
        {
            Core.Initialize();

            LibVLC = new LibVLC(enableDebugLogs: true);

            var video = await GetVideoAsFileResultAsync();

            var media = new Media(LibVLC, new Uri(video.FullPath/*"http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"*/));

            MediaPlayer = new MediaPlayer(media) { EnableHardwareDecoding = true };
            media.Dispose();
            MediaPlayer.Play();
        }

        public async Task<FileResult> GetVideoAsFileResultAsync()
        {
            try
            {
                return await this.GetVideoFromGalleryAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Debug:{ex.Message}");
            }
            return null;
        }

        public async Task<FileResult> GetVideoFromGalleryAsync()
        {
            var result = await MediaPicker.PickVideoAsync(new MediaPickerOptions
            {
                Title = "Please pick a video"
            });
            return result;
        }


        internal void OnDisappearing()
        {
            MediaPlayer.Dispose();
            LibVLC.Dispose();
        }

        private void Set<T>(string propertyName, ref T field, T value)
        {
            if (field == null && value != null || field != null && !field.Equals(value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

}
