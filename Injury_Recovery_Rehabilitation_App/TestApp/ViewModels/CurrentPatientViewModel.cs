/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.services;

namespace TestApp.ViewModels
{
    /// <summary>
    /// This class returns details to do with a patient form their user id
    /// </summary>
    class CurrentPatientViewModel
    {
        private FirebaseMethods fire;
        private Patient patientDetails = null;
        public CurrentPatientViewModel()
        {
            fire = FirebaseMethods.GetInstance();
        }
        /// <summary>
        /// This method returns a patients details form their user id
        /// </summary>
        /// <param name="patientUid"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<Patient> getpatientDetails(string patientUid, bool isMocked)
        {
            patientDetails = await fire.getpatientDetails(patientUid, isMocked);
            return patientDetails;
        }
    }
}
