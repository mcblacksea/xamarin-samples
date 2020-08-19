using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaperBoy.Models.News;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaperBoy.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
        public NewsInformation CurrentArticle { get; set; }
        public ItemDetailPage ()
		{
			InitializeComponent ();
		}
        public ItemDetailPage(NewsInformation article)
        {
            InitializeComponent();
            CurrentArticle = article;
        }
        protected override void OnAppearing()
        {
            BindingContext = CurrentArticle;
            base.OnAppearing();
        }
    }
}