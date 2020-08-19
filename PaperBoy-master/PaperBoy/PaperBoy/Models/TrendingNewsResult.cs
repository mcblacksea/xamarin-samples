using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBoy.Models.Trending
{
    public class TrendingNewsResult
    {
        public string _type { get; set; }
        public Value[] value { get; set; }
    }
    public class Value
    {
        public string name { get; set; }
        public Image image { get; set; }
        public string webSearchUrl { get; set; }
        public bool isBreakingNews { get; set; }
        public Query query { get; set; }
    }
    public class Image
    {
        public string url { get; set; }
        public Provider[] provider { get; set; }
    }

    public class Provider
    {
        public string _type { get; set; }
        public string name { get; set; }
    }

    public class Query
    {
        public string text { get; set; }
    }
}
