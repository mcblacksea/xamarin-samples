using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITestExample
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.InstalledApp("com.companyname.xamarinuitestapp").StartApp();
                // return ConfigureApp.Android.EnableLocalScreenshots().ApkFile(@"W:\proj\dev-stack\xamarin\xamarin-samples\XamarinUITestApp\XamarinUITestApp\XamarinUITestApp.Android\bin\Debug\com.companyname.xamarinuitestapp.apk").StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}