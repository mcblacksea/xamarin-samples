using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Gcm.Client;
using ImageCircle.Forms.Plugin.Droid;

namespace PaperBoy.Droid
{
    [Activity(Label = "PaperBoy", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static MainActivity instance = null;

        public static MainActivity CurrentActivity => instance;
        protected override void OnCreate(Bundle bundle)
        {
            instance = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            ImageCircleRenderer.Init();

            RegisterForNotifications();

            LoadApplication(new App());
            var x = typeof(Xamarin.Forms.Themes.DarkThemeResources);
            x = typeof(Xamarin.Forms.Themes.LightThemeResources);
            x = typeof(Xamarin.Forms.Themes.Android.UnderlineEffect);
        }

        private void RegisterForNotifications()
        {
            try
            {
                GcmClient.CheckDevice(this);
                GcmClient.CheckManifest(this);
                GcmClient.Register(this,PushHandlerBroadcastReceiver.SENDER_IDS);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

