using System;
using System.ComponentModel;

using WebRTC.DemoApp.Helper;

using Xamarin.Forms;

namespace WebRTC.DemoApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            RoomIdEntry.Text = "3004b38d-cf8a-49d3-9793-bfe8b2209d0d";
        }

        async void StartCall_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CallPage(RoomIdEntry.Text, true));
        }

        async void JoinCall_Clicked(object sender, EventArgs e)
        {
            var roomId = RoomIdEntry.Text;
            if (!string.IsNullOrEmpty(roomId) || !string.IsNullOrWhiteSpace(roomId))
            {
                await Navigation.PushAsync(new CallPage(roomId, false));
            }
        }
    }
}
