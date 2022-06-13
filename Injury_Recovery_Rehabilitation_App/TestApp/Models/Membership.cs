/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using Newtonsoft.Json;

namespace TestApp.Models
{
    /// <summary>
    /// This model is used to return the membership assigned to the physio
    /// </summary>
    public class Membership
    {
        [JsonProperty("membershipExpiryDate")]
        public string MembershipDate { get; set; }
    }
}
