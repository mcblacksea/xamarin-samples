using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;

namespace PaperBoy.Data
{
    public class Favorite
    {
        string id;
        string title;
        string description;
        string imageUrl;
        DateTime articleDate;

        [JsonProperty(PropertyName ="id")]
        public string Id
        {
            get => id;
            set => id = value;
        }

        [JsonProperty(PropertyName ="title")]
        public string Title
        {
            get => title;
            set => title = value;
        }

        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get => description;
            set => description = value;
        }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl
        {
            get => imageUrl;
            set => imageUrl = value;
        }

        [JsonProperty(PropertyName =("articleDate"))]
        public DateTime ArticleDate
        {
            get=>articleDate;
            set => articleDate = value;
        }

        [Version]
        public string Version { get; set; }

    }

}
