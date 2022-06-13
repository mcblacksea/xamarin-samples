/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestApp.Models
{
    /// <summary>
    /// This describes the data for the progress Json Structure
    /// </summary>
    public class Progress
    {
        [JsonProperty("complete")]
        public Dictionary<string, bool> planDates { get; set; }
    }
}
