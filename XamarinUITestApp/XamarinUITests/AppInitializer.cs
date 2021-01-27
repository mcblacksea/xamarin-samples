using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace XamarinUITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.InstalledApp("com.companyname.xamarinuitestapp").StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}