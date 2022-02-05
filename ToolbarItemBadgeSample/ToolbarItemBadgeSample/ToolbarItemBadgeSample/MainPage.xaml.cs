using System.Linq;
using ToolbarItemBadgeSample.Services;
using Xamarin.Forms;

namespace ToolbarItemBadgeSample
{
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnAppearing();
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if(ToolbarItems.Count > 0)
            {
                DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{e.NewValue}", Color.Red, Color.White);
            }


        }
    }
}
