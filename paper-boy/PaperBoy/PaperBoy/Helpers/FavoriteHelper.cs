//using PaperBoy.Data;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace PaperBoy.Helpers
//{
//    public static class FavoriteHelper
//    {
//        public async static Task<bool> EnsureCategoriesAsync()
//        {
//            List<NewsCategory> categories = new List<NewsCategory>();

//            categories.Add(new NewsCategory() { Title = "Technology", InternalName = "Technology" });
//            categories.Add(new NewsCategory() { Title = "Business", InternalName = "Business" });
//            categories.Add(new NewsCategory() { Title = "Entertainment", InternalName = "Entertainment" });
//            categories.Add(new NewsCategory() { Title = "Health", InternalName = "Health" });
//            categories.Add(new NewsCategory() { Title = "Politics", InternalName = "Politics" });
//            categories.Add(new NewsCategory() { Title = "Science and Technology", InternalName = "ScienceAndTechnology" });
//            categories.Add(new NewsCategory() { Title = "Sports", InternalName = "Sports" });
//            categories.Add(new NewsCategory() { Title = "World", InternalName = "World" });

//            await App.Database.SaveCategoriesAsync(categories);

//            return true;
//        }
//    }
//}
