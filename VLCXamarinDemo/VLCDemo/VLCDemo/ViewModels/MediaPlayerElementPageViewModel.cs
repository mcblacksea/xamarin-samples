using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using LibVLCSharp.Shared;
using Xamarin.Essentials;

namespace VLCDemo.ViewModels
{
    /// <summary>
    /// NOTE: Code sample gotten from : https://code.videolan.org/mfkl/libvlcsharp-samples/-/blob/master/MediaElement/MediaElement/MainViewModel.cs
    /// </summary>
    public class MediaPlayerElementPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of <see cref="MainViewModel"/> class.
        /// </summary>
        public MediaPlayerElementPageViewModel()
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

            LibVLC = new LibVLC();

            var video = await GetVideoAsFileResultAsync();

            var media = new Media(LibVLC,
                new Uri(video.FullPath /*"http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4"*/));

            MediaPlayer = new MediaPlayer(media) { EnableHardwareDecoding = true };
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

        //public async Task<FileResult> ShootVideoAsync()
        //{
        //    FileResult video = null;
        //    try
        //    {
        //        var permissions = await _permissionsService.GetDeviceCameraPermissionsAsync();
        //        if (permissions == PermissionStatus.Granted)
        //        {
        //            video = await MediaPicker.CaptureVideoAsync();
        //            Console.WriteLine($"CaptureVideoAsync COMPLETED: {video.FileName}");
        //        }
        //    }
        //    catch (FeatureNotSupportedException fnsEx)
        //    {
        //        Console.WriteLine($"CaptureVideoAsync THREW: {fnsEx.Message}");
        //    }
        //    catch (PermissionException pEx)
        //    {
        //        Console.WriteLine($"CaptureVideoAsync THREW: {pEx.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"CaptureVideoAsync THREW: {ex.Message}");
        //    }
        //    return video;
        //}


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
