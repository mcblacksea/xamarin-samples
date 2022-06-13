/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using Newtonsoft.Json;

namespace TestApp.Models
{
    /// <summary>
    /// This describes the data for the IosCredentials Json Structure
    /// </summary>
    public class IosCredentials
    {
        [JsonProperty("uid")]
        public string userId { get; set; }
        [JsonProperty("salt")]
        public string salt { get; set; }
        [JsonProperty("md5")]
        public string saltHashed { get; set; }
    }
}
