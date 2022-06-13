using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.services;

namespace TestApp.ViewModels
{
    /// <summary>
    /// This class returns the exercises of the exercise plan assigned to the patient 
    /// </summary>
    class DisplayPatientExercisePlanViewModel
    {
        private FirebaseMethods fire;
        private string videoLink = "";
        private Patient patientDetails = null;
        private List<Exercise> patientExerciselist = new List<Exercise>();
        private int exerciseCompletedNumber = 0;
 
        public DisplayPatientExercisePlanViewModel()
        {
            fire = FirebaseMethods.GetInstance();
        }
        /// <summary>
        /// This method returns a list of the exercises assigned to the patient
        /// </summary>
        /// <param name="exercise1"></param>
        /// <param name="exercise2"></param>
        /// <param name="exercise3"></param>
        /// <returns></returns>
        public async Task<List<Exercise>> getPatientExercises(string exercise1,string exercise2,string exercise3,bool isMocked)
        {
            List<Exercise> exerciseList = await fire.GetPatientExercises(exercise1, exercise2, exercise3, isMocked);
            return exerciseList;
        }
        /// <summary>
        /// This method returns a download url for a video that is stored in firbase storage
        /// </summary>
        /// <param name="videoName"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<string> getExerciseVideoLink(string videoName,bool isMocked)
        {
            videoLink = await fire.GetVideosFromStorage(isMocked, videoName);
            return videoLink;
        }
        /// <summary>
        /// This method returns a patients details by the use of the user id
        /// </summary>
        /// <param name="patientUid"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<Patient> getpatientDetails(string patientUid, bool isMocked)
        {
            patientDetails = await fire.getpatientDetails(patientUid, isMocked);
            return patientDetails;
        }
        /// <summary>
        /// This method calls the getpatientDetails and the getPatientExercises methods of this class
        /// and returns a list of exercises assigned to patient
        /// </summary>
        /// <param name="patientUid"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<List<Exercise>> getPatientsExercisePlan(string patientUid, bool isMocked)
        {
            patientDetails = await getpatientDetails(patientUid, isMocked);
            patientExerciselist = await getPatientExercises(patientDetails.Exer1, patientDetails.Exer2, patientDetails.Exer3, isMocked);
            return patientExerciselist;
        }
        /// <summary>
        /// This ExerciseCompletedNumber property is used to identify which exercise has been completed
        /// </summary>
        public int ExerciseCompletedNumber
        {
            get { return exerciseCompletedNumber; } 
            set { exerciseCompletedNumber = value; }
        }
        /// <summary>
        /// This method removes the time from the date object
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public string removeTimeFromDate(string dateTime)
        {
            int spaceFound = dateTime.IndexOf(" ");
            string date = dateTime.Substring(0, spaceFound);
            return date;
        }
        /// <summary>
        /// This method passes the patient user id and the current date to recordPatientProgress 
        /// to upadte the current state of the exercise plan
        /// </summary>
        /// <param name="PatientUID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<bool> saveStateOfExercisePlan(string PatientUID, string date,bool isMocked)
        {
            try
            {
                Dictionary<string, bool> patientState = await fire.getPatientProgress(PatientUID, isMocked);
                patientState[date] = true;
                await fire.recordPatientProgress(PatientUID, patientState, isMocked);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
           
        }
        /// <summary>
        /// This method checks if the user has already completed
        /// </summary>
        /// <param name="date"></param>
        /// <param name="patientUid"></param>
        /// <returns></returns>
        public async Task<bool> checkIfExercisePlanCompleteForToday(string date,string patientUid,bool IsMocked)
        {
            Dictionary<string, bool> patientState = await fire.getPatientProgress(patientUid, IsMocked);
            try
            {
                if (patientState[date] == true)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return true;
            }
            return false;
        }
    }
}
