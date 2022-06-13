/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using Newtonsoft.Json;
using System;
using System.Net;

namespace TestApp.Services
{
    class PredictModelApi
    {
        string prediction = "";
        /// <summary>
        /// This class connects to a decsion tree classifier model that is in a flask web app stored on googles cloud platform
        /// Data sent from the phone is used to classify what common injury a patient might have
        /// </summary>
        public PredictModelApi()
        {

        }
        /// <summary>
        /// This method sends an integer array off to the flask app and retruns with a prediction
        /// </summary>
        /// <param name="predictionArray"></param>
        /// <returns></returns>
        public string getPrediction(int[] predictionArray)
        {
            using (var webClient = new System.Net.WebClient())
            {
                var dataString = JsonConvert.SerializeObject(predictionArray);
                webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                prediction = webClient.UploadString(new Uri("https://app-tree-gk6j5fl6ea-nw.a.run.app/predict"), "POST", dataString);
            }
            return prediction;
        }
    }
}
