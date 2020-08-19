using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaperBoy.Data;
using PaperBoy.ViewModels;
using Xamarin.Forms;
using PaperBoy.Helpers;

namespace PaperBoy
{
    //android Package name : com.paperboy.android
    public partial class App : Application
	{
        public static MainViewModel  viewModel{ get; set; }
        public static INavigation MainNavigation { get; set; }
        //private static FavoritesDatabase database;
        //public static FavoritesDatabase Database
        //{
        //    get
        //    {
        //        if(database==null)
        //        {
        //            database=new FavoritesDatabase(StorageHelper.GetLocalFilePath("Favorite.db"));
        //        }
        //        return database;
        //    }
        //}
        public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new PaperBoy.MainPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
