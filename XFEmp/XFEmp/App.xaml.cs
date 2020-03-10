using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFEmp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new DevPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
