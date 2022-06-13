/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using Newtonsoft.Json;

namespace TestApp.Models
{
    /// <summary>
    /// Used to generate a new exercise plan
    /// </summary>
    public class Plan
    {
        [JsonProperty("name")]
        public string exerciseName { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("exercise1")]
        public string Exercise1 { get; set; }
        [JsonProperty("exercise2")]
        public string Exercise2 { get; set; }
        [JsonProperty("exercise3")]
        public string Exercise3 { get; set; }
        [JsonProperty("exerciseImage")]
        public string ImageString { get; set; }
    }
}
