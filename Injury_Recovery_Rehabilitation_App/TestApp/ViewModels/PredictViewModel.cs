/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System.Collections.Generic;
using TestApp.Services;

namespace TestApp.ViewModels
{
    /// <summary>
    /// This class takes in the form data and uses dictionarys to build an array that can be sent to the flask api
    /// </summary>
    class PredictViewModel
    {
        PredictModelApi predict;
        Dictionary<string, int> skillDict = new Dictionary<string, int>();
        Dictionary<string, int> injuredDict = new Dictionary<string, int>();
        Dictionary<string, int> ageDict = new Dictionary<string, int>();
        Dictionary<string, int> genderDict = new Dictionary<string, int>();
        Dictionary<string, int> activityDict = new Dictionary<string, int>();
        int[] predictionArray = new int[5];
        string result = "";
        public PredictViewModel()
        {
            predict = new PredictModelApi();
            setDicts();
        }
        /// <summary>
        /// Sends values to model api and returns with the predicted injury
        /// </summary>
        /// <param name="skillLevel"></param>
        /// <param name="injuredBefore"></param>
        /// <param name="Age"></param>
        /// <param name="gender"></param>
        /// <param name="activity"></param>
        /// <returns></returns>
        public string predictWithValues(string skillLevel,string injuredBefore,string Age,string gender,string activity)
        {
            predictionArray[0] = skillDict[skillLevel];
            predictionArray[1] = injuredDict[injuredBefore];
            predictionArray[2] = ageDict[Age];
            predictionArray[3] = genderDict[gender];
            predictionArray[4] = activityDict[activity];
            result  = predict.getPrediction(predictionArray);
            return result;
        }
        /// <summary>
        /// Matchs the values choosen to the models numeric values
        /// </summary>
        public void setDicts()
        {
            skillDict.Add("amatur", 0);
            skillDict.Add("Casual", 1);
            skillDict.Add("professional", 2);
            injuredDict.Add("Yes", 1);
            injuredDict.Add("No", 0);
            ageDict.Add("0-18", 0);
            ageDict.Add("18-36", 1);
            ageDict.Add("36-50", 2);
            ageDict.Add("50-65", 3);
            ageDict.Add("65-80", 4);
            genderDict.Add("Male", 1);
            genderDict.Add("Female", 0);
            activityDict.Add("Rugby", 7);
            activityDict.Add("Soccer", 9);
            activityDict.Add("Gym", 3);
            activityDict.Add("Jogging", 6);
            activityDict.Add("Hockey", 4);
            activityDict.Add("Weightlifting", 11);
            activityDict.Add("Dancing", 1);
            activityDict.Add("Tennis", 10);
            activityDict.Add("Golf", 2);
            activityDict.Add("Horse Riding", 5);
            activityDict.Add("Skateboarding", 8);
            activityDict.Add("Cycling", 0);
        }
    }
}
