using Xamarin.Forms;
using XF.PCL.Pages;

namespace XF
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new GridPage();
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
