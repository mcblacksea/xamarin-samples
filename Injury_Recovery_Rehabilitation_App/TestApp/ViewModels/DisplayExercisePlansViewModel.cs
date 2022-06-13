/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.models;
using TestApp.services;

namespace TestApp.ViewModels
{
    /// <summary>
    /// This class accesses the exercise functions of the FirebaseMethods class
    /// </summary>
    class DisplayExercisePlansViewModel
    {
        private FirebaseMethods fire;
        /// <summary>
        /// This constructor calls the eager singleton instance of FirebaseMethods  
        /// </summary>
        public DisplayExercisePlansViewModel() {
            fire = FirebaseMethods.GetInstance();
        }
        /// <summary>
        /// This method returns all exercise plans from the FirebaseMethods GetAllExercises method
        /// </summary>
        /// <returns></returns>
        public async Task<List<ExercisePlan>> getExerciseList(bool isMocked)
        {
            return await fire.GetAllExercises(isMocked);
        }
        /// <summary>
        /// This method passses a key to the FirebaseMethods GetExercise() to 
        /// retrive a single exercise plan from a list of exercises
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<ExercisePlan> sendExerciseKey(string key,bool isMocked)
        {
            ExercisePlan exercise = await fire.GetExercise(key, isMocked);

            return exercise;
        }
        /// <summary>
        /// This method returns a list of physio made plans
        /// </summary>
        /// <param name="physioUid"></param>
        /// <returns></returns>
        public async Task<List<ExercisePlan>> getPhysioPlans(string physioUid)
        {
            return await fire.getPhysioExercisePlans(physioUid);
        }
    }
}
