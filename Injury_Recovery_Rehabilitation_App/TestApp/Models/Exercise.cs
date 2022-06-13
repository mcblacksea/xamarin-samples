/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using Newtonsoft.Json;

namespace TestApp.Models
{
    /// <summary>
    /// This describes the data for the exercise Json Structure
    /// </summary>
    public class Exercise
    {
        [JsonProperty("name")]
        public string ExerciseName { get; set; }
        [JsonProperty("copyRight")]
        public string exerciseVideoCopyright { get; set; }
        [JsonProperty("videoLink")]
        public string VideoLink { get; set; }
    }
}
