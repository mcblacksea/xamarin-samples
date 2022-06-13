/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System.Collections.Generic;

namespace TestApp.Models
{
    /// <summary>
    /// This model has extra fields used for the mock database
    /// </summary>
    public class MockPhysio
    {
        //extra fields for mock database
        public string PhysioName { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public List<string> patientList = new List<string>();
        public string physioUid = "";
        public string PatientUid { get; set; }
        public string Membership { get; set; }
    }
}
