using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PaperBoy.Droid
{
    [Activity(Label = "Paperboy",Theme ="@style/Theme.Splash",MainLauncher =true,NoHistory =true)]
    public class SplashActivity : Activity
    {
        public override void OnCreate(Bundle savedInstanceState,PersistableBundle persistableState)
        {
            base.OnCreate(savedInstanceState,persistableState);

            // Create your application here
        }
        protected override void OnResume()
        {
            base.OnResume();

            Task startupWork = new Task(async () => await Task.Delay(1000));
            startupWork.ContinueWith(t =>
            {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            }, TaskScheduler.FromCurrentSynchronizationContext());

            startupWork.Start();
        }
    }
}