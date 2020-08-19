using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaperBoy.Helpers;
using PaperBoy.Models;
using PaperBoy.Common.Commands;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PaperBoy.Models.News;

namespace PaperBoy.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TechnologyPage : ContentPage
	{
		public TechnologyPage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            this.BindingContext = App.viewModel;
            base.OnAppearing();
        }
        public void OnItemTapped(object sender,ItemTappedEventArgs e)
        {
            new NavigateToDetailCommand().Execute(e.Item as NewsInformation);
        }
    }
}