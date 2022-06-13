/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using Newtonsoft.Json;

namespace TestApp.Models
{
    /// <summary>
    /// This database model checks if the user is a patient or physio
    /// </summary>
    public class CheckUser
    {
        [JsonProperty("user")]
        public string User { get; set; }
    }
}
