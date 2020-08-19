using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBoy.Models.News
{
    public class NewsInformation
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class Sort
    {
        public string name { get; set; }
        public string id { get; set; }
        public bool isSelected { get; set; }
        public string url { get; set; }
    }

    public class Thumbnail
    {
        public string contentUrl { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Image
    {
        public Thumbnail thumbnail { get; set; }
    }

    public class About
    {
        public string readLink { get; set; }
        public string name { get; set; }
    }

    public class Provider
    {
        public string _type { get; set; }
        public string name { get; set; }
    }

    public class Mention
    {
        public string name { get; set; }
    }

    public class Value
    {
        public string name { get; set; }
        public string url { get; set; }
        public Image image { get; set; }
        public string description { get; set; }
        public IList<About> about { get; set; }
        public IList<Provider> provider { get; set; }
        public DateTime datePublished { get; set; }
        public string category { get; set; }
        public IList<Mention> mentions { get; set; }
    }

    public class NewsResult
    {
        public string _type { get; set; }
        public string readLink { get; set; }
        public int totalEstimatedMatches { get; set; }
        public IList<Sort> sort { get; set; }
        public IList<Value> value { get; set; }
    }


}
