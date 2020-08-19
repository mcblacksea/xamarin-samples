using PaperBoy.Models;
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
	public partial class FavoritesPage : ContentPage
	{
		public FavoritesPage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            LoadFavorites();

            base.OnAppearing();
        }
        private async  void LoadFavorites()
        {
            this.BindingContext = App.viewModel;
            await App.viewModel.RefreshFavoritesAsync();

        }
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            new Common.Commands.NavigateToDetailCommand().Execute(e.Item as FavoriteInfromation);
        }

    }
}