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
    /// This class allows for deleting updating and creating of an exercise plan and return a list of exercise plan names
    /// </summary>
    class ExerciseViewModel
    {
        private FirebaseMethods fire;
        public ExerciseViewModel()
        {
            fire = FirebaseMethods.GetInstance();
        }
        /// <summary>
        /// This method returns an object list from the database
        /// </summary>
        /// <returns></returns>
        public async Task<List<Exercise>> getExercises()
        {
            return await fire.getAListOfExerciseNames();
        }
        /// <summary>
        /// This method addes a new exercise plan to a list of physio plans
        /// </summary>
        /// <param name="physioUserid"></param>
        /// <param name="exerciseName"></param>
        /// <param name="exer1"></param>
        /// <param name="exer2"></param>
        /// <param name="exer3"></param>
        /// <param name="image"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<bool> addNewPlanToDb(string physioUserid,string exerciseName,string exer1,string exer2,string exer3,string image,string category)
        {
            bool tF = await fire.addNewPlan(physioUserid, category, exer1, exer2, exer3, image, exerciseName);
            return tF;
        }
        /// <summary>
        /// This method updates an existing plan with a new one
        /// </summary>
        /// <param name="physioUserid"></param>
        /// <param name="newExerciseName"></param>
        /// <param name="exer1"></param>
        /// <param name="exer2"></param>
        /// <param name="exer3"></param>
        /// <param name="image"></param>
        /// <param name="category"></param>
        /// <param name="orginalExerciseName"></param>
        /// <returns></returns>
        public async Task<bool> updatePlanToDb(string physioUserid, string newExerciseName, string exer1, string exer2, string exer3, string image, string category,string orginalExerciseName)
        {
            bool tf = await fire.updatePhysioPlan(physioUserid, newExerciseName, exer1, exer2, exer3, image, category, orginalExerciseName);
            return tf;
        }
        /// <summary>
        /// This method removes a created exercise plan from the physio
        /// </summary>
        /// <param name="physioUserid"></param>
        /// <param name="exerciseName"></param>
        /// <returns></returns>
        public async Task<bool> deletePlanToDb(string physioUserid, string exerciseName)
        {
            bool tf = await fire.deletePhysioPlan(physioUserid, exerciseName);
            return tf;
        }
    }
}
