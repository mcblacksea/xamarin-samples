using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaperBoy.Common.Commands;
using PaperBoy.Models;
using PaperBoy.Models.News;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaperBoy.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
		public SearchPage ()
		{
		    InitializeComponent();
        }
        protected override void OnAppearing()
        {
            LoadDefaultSearchResults();
            base.OnAppearing();
        }

	    private async void LoadDefaultSearchResults()
	    {
	        this.BindingContext = App.viewModel;

	        if (string.IsNullOrWhiteSpace(App.viewModel.SearchQuery))
	            App.viewModel.SearchQuery = "Microsoft";

	        await App.viewModel.RefreshSearchResultAsync();

	    }

	    public void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            new NavigateToDetailCommand().Execute(e.Item as NewsInformation);
        }

    }
}