using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using NativeMedia;
using MediaGallery;
using System.Diagnostics;

namespace MediaGallery
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var cts = new CancellationTokenSource();
            IMediaFile[] files = null;

            try
            {
                var request = new MediaPickRequest(1, MediaFileType.Image, MediaFileType.Video)
                {
                    PresentationSourceBounds = System.Drawing.Rectangle.Empty,
                    UseCreateChooser = true,
                    Title = "Select"
                };

                cts.CancelAfter(TimeSpan.FromMinutes(5));

                Debug.WriteLine("BEFORE PickAsync");
                var results = await NativeMedia.MediaGallery.PickAsync(request, cts.Token);
                Debug.WriteLine("AFTER PickAsync");
                files = results?.Files?.ToArray();
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("CANCELED PickAsync");
                // handling a cancellation request
            }
            catch (Exception)
            {
                Debug.WriteLine("EXCEPTION PickAsync");
                // handling other exceptions
            }
            finally
            {
                cts.Dispose();
            }


            if (files == null)
                return;

            Debug.WriteLine("DONE picking file");

            testImg.Source = new StreamImageSource() { Stream = async _ => await files[0].OpenReadAsync() };

        }
    }
}
