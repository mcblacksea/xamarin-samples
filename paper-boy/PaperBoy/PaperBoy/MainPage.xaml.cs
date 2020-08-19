using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using PaperBoy.ViewModels;

namespace PaperBoy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : TabbedPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            if(App.viewModel==null)
            {
                App.viewModel = new MainViewModel();
                App.viewModel.RefreshNewsAsync();
            }
            
            App.MainNavigation = Navigation;

            CrossConnectivity.Current.ConnectivityChanged += OnConnectivityChannged;
            base.OnAppearing();
        }

        private void OnConnectivityChannged(object sender, ConnectivityChangedEventArgs e)
        {
            //TODO: Method Impilimentation
        }
       
       
    }
}
