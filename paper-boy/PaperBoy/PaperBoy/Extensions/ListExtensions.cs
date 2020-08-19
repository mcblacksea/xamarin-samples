using PaperBoy.Data;
using PaperBoy.Models;
using PaperBoy.Models.News;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaperBoy.Extensions
{
    public static class ListExtensions
    {
        public static NewsInformation AsArticle(this FavoriteInfromation favorite)
        {
            return new NewsInformation()
            {
                CreatedDate=favorite.ArticleDate,
                Description=favorite.Description,
                ImageUrl=favorite.ImageUrl,
                Title=favorite.Title
            };
        }

        public async static Task<FavoriteInfromation>AsFavorite(this NewsInformation article,string categoryTitle)
        {
            return new FavoriteInfromation()
            {
                ArticleDate = article.CreatedDate,
                Description = article.Description,
                ImageUrl = article.ImageUrl,
                Title = article.Title,
                CategoryTitle = categoryTitle,

            };
        }
        public static FavoriteInfromation AsFavorite(this Favorite favorite,String categoryTitle)
        {
            return new FavoriteInfromation()
            {
                ArticleDate = favorite.ArticleDate,
                Description = favorite.Description,
                ImageUrl = favorite.ImageUrl,
                Title = favorite.Title,
                CategoryTitle = categoryTitle,

            };
        }
    }
}
