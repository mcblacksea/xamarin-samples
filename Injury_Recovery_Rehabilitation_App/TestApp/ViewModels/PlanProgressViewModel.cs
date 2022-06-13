/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.services;

namespace TestApp.ViewModels
{
    /// <summary>
    /// This class returns the patient plan progress and patient names and user ids
    /// </summary>
    class PlanProgressViewModel
    {
        private FirebaseMethods fire;
        public PlanProgressViewModel()
        {
            fire = FirebaseMethods.GetInstance();
        }
        /// <summary>
        /// This method returns a patients progress plan
        /// </summary>
        /// <param name="patientUid"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, bool>> getPatientProgress(string patientUid)
        {
            return await fire.getPatientProgress(patientUid, false);
        }
        /// <summary>
        /// This meyhod returns a list of patient user ids and encrypted patient names
        /// </summary>
        /// <param name="physioId"></param>
        /// <returns></returns>
        public async Task <List<PatientList>> getPatientNameAndPatientUserId(string physioId,bool isMocked)
        {
            return await fire.GetNamesAndPatientUids(physioId, isMocked); 
        }
    }
}
