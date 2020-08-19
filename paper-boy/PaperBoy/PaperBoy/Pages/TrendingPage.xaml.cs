using PaperBoy.Common.Commands;
using PaperBoy.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaperBoy.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TrendingPage : ContentPage
	{
		public TrendingPage ()
		{
			InitializeComponent ();
            
        }
        protected override void OnAppearing()
        {
            this.BindingContext = App.viewModel;
            base.OnAppearing();
        }
        public void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            new NavigateToDetailCommand().Execute(e.Item as NewsInformation);
        }
    }
}