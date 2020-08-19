using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PaperBoy.Models;
using PaperBoy.Models.News;
using PaperBoy.Models.Trending;

namespace PaperBoy.Helpers
{
    public static class NewsHelper
    {
        public async static Task<List<NewsInformation>>GetTrendingAsync()
        {
            var results = new List<NewsInformation>();

            var searchUrl = $"https://api.cognitive.microsoft.com/bing/v7.0/news/trendingtopics?cc=US";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.NewsSearchApiKey);

            var uri = new Uri(searchUrl);
            var result = await client.GetStringAsync(uri);
            var newsResult = JsonConvert.DeserializeObject<TrendingNewsResult>(result);

            results = (from item in newsResult.value
                       select new NewsInformation()
                       {
                           Title = item.name,
                           Description=item.query.text,
                           CreatedDate=DateTime.Now,
                           ImageUrl=item.image.url,
                       }).ToList();

            return results.Where(w => !string.IsNullOrWhiteSpace(w.ImageUrl)).Take(10).ToList();
           
        }
        public async static Task<List<NewsInformation>> GetByCategoryAsync(NewsCategoryType newsCategory)
        {
            var results = new List<NewsInformation>();

            var searchUrl = $"https://api.cognitive.microsoft.com/bing/v7.0/news/?mkt=en-US&Category={newsCategory}";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.NewsSearchApiKey);

            var uri = new Uri(searchUrl);
            var result = await client.GetStringAsync(uri);
            var newsResult = JsonConvert.DeserializeObject<NewsResult>(result);

            results = (from item in newsResult.value
                       select new NewsInformation()
                       {
                           Title = item.name,
                           Description=item.description,
                           ImageUrl=item.image?.thumbnail?.contentUrl,
                           CreatedDate=item.datePublished
                       }).ToList();
            return results.Where(w => !string.IsNullOrWhiteSpace(w.ImageUrl)).Take(10).ToList();
        }

        public async static Task<List<NewsInformation>> GetSearchAsync(String searchQuery)
        {
            var results=new List<NewsInformation>();
            var searchUrl= $"https://api.cognitive.microsoft.com/bing/v7.0/news/search?q={searchQuery}&mkt=en-US";

            var client=new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.NewsSearchApiKey);

            var url=new Uri(searchUrl);
            var result =await client.GetStringAsync(url);

            var searchResult = JsonConvert.DeserializeObject<NewsResult>(result);
            results = (from item in searchResult.value
                select new NewsInformation()
                {
                    Title = item.name,
                    Description = item.description,
                    ImageUrl = item.image?.thumbnail?.contentUrl,
                    CreatedDate = item.datePublished
                }).ToList();
            return results.Where(w => !String.IsNullOrWhiteSpace(w.ImageUrl)).Take(10).ToList();
        }
    }
}
