using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaperBoy.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PaperBoy.Helpers;

namespace PaperBoy.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingPage : ContentPage
	{
        public SettingPage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            InitializaeSetting();
            base.OnAppearing();
        }

        private void InitializaeSetting()
        {
            this.BindingContext = App.viewModel;
            articleCounterSlider.Value = 10;
            categoryPicker.SelectedIndex = 1;

            var label = GeneralHelper.GetLabel();
            var extendedLabel = GeneralHelper.GetLabel("Running PaperBoy on", true);

            App.viewModel.PlatformLabel = label;
            App.viewModel.ExtendedPlatformLabel = extendedLabel;

            var orientation = GeneralHelper.GetOrientation();
            App.viewModel.DeviceOrientation = orientation;

        }
        private void OnSaveClicked(object sender,EventArgs e)
        {
            App.Current.Resources["ListTextColor"] = Color.Blue;
        }
    }
}