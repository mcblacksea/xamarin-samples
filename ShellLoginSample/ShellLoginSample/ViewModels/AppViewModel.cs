using ShellLoginSample.Views;
using Xamarin.Forms;

namespace ShellLoginSample.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        private bool isAdmin;

        public bool IsAdmin { get => isAdmin; set => SetProperty(ref isAdmin, value); }
        public AppViewModel()
        {
            MessagingCenter.Subscribe<LoginPage>(this, "admin", (sender) =>
            {
                IsAdmin = true;
            });

            MessagingCenter.Subscribe<LoginPage>(this, "user", (sender) =>
            {
                IsAdmin = false;
            });

        }
    }
}
