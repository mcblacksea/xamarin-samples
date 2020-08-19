using PaperBoy.Common;
using PaperBoy.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PaperBoy.Models
{
    public class FavoriteInfromation:ObservableBase
    {
        private int _id;
        public int Id
        {
            get => _id;
            set { SetProperty(ref this._id,value); }
        }

        private string _title;
        public string Title
        {
            get =>_title; 
            set { this.SetProperty(ref this._title, value); }
        }

        private string _categoryTitle;
        public string CategoryTitle
        {
            get =>_categoryTitle; 
            set { this.SetProperty(ref this._categoryTitle, value); }
        }

        private string _description;
        public string Description
        {
            get => _description; 
            set { this.SetProperty(ref this._description, value); }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl; 
            set { this.SetProperty(ref this._imageUrl, value); }
        }

        private DateTime _articleDate;
        public DateTime ArticleDate
        {
            get => _articleDate; 
            set { this.SetProperty(ref this._articleDate, value); }
        }

    }
    public class FavoritesCollection : ObservableCollection<FavoriteInfromation>
    {
        public async Task AddAsync(FavoriteInfromation articel)
        {
            var favorite = new Favorite()
            {
                ArticleDate=articel.ArticleDate,
                Description=articel.Description,
                ImageUrl=articel.ImageUrl,
                Title=articel.Title,
            };
            
            await FavoriteManager.DefaultManager.SaveFavoriteAsync(favorite);

            this.Add(articel);
        }
    }
}
