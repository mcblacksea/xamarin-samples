/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using Newtonsoft.Json;

namespace TestApp.Models
{
    /// <summary>
    /// This model is used to save a patient name with the userid
    /// </summary>
    public class PatientList
    {
        [JsonProperty("patientUid")]
        public string PatientUid { get; set; }
        [JsonProperty("patientName")]
        public string PatientName { get; set; }
    }
}
