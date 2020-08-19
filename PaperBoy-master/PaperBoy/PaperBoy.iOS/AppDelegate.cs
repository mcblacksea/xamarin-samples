using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using PaperBoy.Data;
using UIKit;

namespace PaperBoy.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            UIColor accentColor = UIColor.FromRGB(0, 89, 178);

            UISlider.Appearance.TintColor = accentColor;
            UISlider.Appearance.ThumbTintColor = accentColor;

            UITabBar.Appearance.TintColor = accentColor;
            UITabBar.Appearance.SelectedImageTintColor = accentColor;

            UINavigationBar.Appearance.BarTintColor = accentColor;
            UINavigationBar.Appearance.TintColor = accentColor;
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White });

            ImageCircleRenderer.Init();

            RegisterForPushNotifications();
            LoadApplication(new App());
            var x = typeof(Xamarin.Forms.Themes.DarkThemeResources);
            x = typeof(Xamarin.Forms.Themes.LightThemeResources);
            x = typeof(Xamarin.Forms.Themes.iOS.UnderlineEffect);
            return base.FinishedLaunching(app, options);
        }

        private void RegisterForPushNotifications()
        {
            var settings = UIUserNotificationSettings.GetSettingsForTypes(
                UIUserNotificationType.Alert
                | UIUserNotificationType.Badge
                | UIUserNotificationType.Sound,
                new NSSet());

            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            UIApplication.SharedApplication.RegisterForRemoteNotifications();
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            const string templateBodyAPNS= "{\"aps\":{\"alert\":\"$(messageParam)\"}}";

            JObject templates =new JObject();
            templates["genericMessage"] = new JObject()
            {
                {"body", templateBodyAPNS}
            };

            Push push = FavoriteManager.DefaultManager.CurrentClient.GetPush();

#pragma warning disable CS1701 // Assuming assembly reference matches identity
            push.RegisterAsync(deviceToken, templates);
#pragma warning restore CS1701 // Assuming assembly reference matches identity
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            Helpers.ToastHelper.ProcessNotification(userInfo);
        }
    }
}
