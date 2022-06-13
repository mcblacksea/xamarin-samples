/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using Newtonsoft.Json;

namespace TestApp.Models
{
    /// <summary>
    /// This class assigns an encryption key to a patient userid for encrypting sensitive data
    /// </summary>
    public class Encryption
    {
        [JsonProperty("patientUid")]
        public string PatientUid { get; set; }
        [JsonProperty("encryptionKey")]
        public string EncryptionKey { get; set; }
    }
}
