/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System;
using TestApp.Models;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    /// <summary>
    /// This class is used to update the number of exercises that a patient has to do
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatePatientExerciseAmount : ContentPage
    {
        private string patientUid = "";
        private RegisterPatientViewModel update;
        private Patient patient;
        private bool isExer1Correct = false;
        private bool isExer2Correct = false;
        private bool isExer3Correct = false;
        private int minimum1 = 0;
        private int maximum1 = 0;
        private int minimum2 = 0;
        private int maximum2 = 0;
        private int minimum3 = 0;
        private int maximum3 = 0;
        public UpdatePatientExerciseAmount(string patientUid, Patient patient)
        {
            InitializeComponent();
            update = new RegisterPatientViewModel();
            this.patient = patient;
            this.patientUid = patientUid;
            exercise1.Text = patient.Exer1;
            exercise2.Text = patient.Exer2;
            exercise3.Text = patient.Exer3;
            min1.Text = patient.minExercise1.ToString();
            min2.Text = patient.minExercise2.ToString();
            min3.Text = patient.minExercise3.ToString();
            max1.Text = patient.maxExercise1.ToString();
            max2.Text = patient.maxExercise2.ToString();
            max3.Text = patient.maxExercise3.ToString();
        }
        /// <summary>
        /// This button send updated exercise numbers to RegisterPatientViewModel so they can update the patient record
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="args"></param>
        private async void updateExercisePlan(Object Sender, EventArgs args)
        {
            if (allMinMaxValuesFilledIn())
            {
                checkMinMaxValuesAreCorrect();
                if (isExer1Correct && isExer2Correct && isExer3Correct)
                {
                    await update.updatePatientRecord(patient.PatientName, patient.Gender, patient.Email, patient.InjuryType, patient.InjuryOccurred, patient.Age, patient.InjurySeverity, minimum1, minimum2, minimum3, maximum1, maximum2, maximum3, patient.Exer1, patient.Exer2, patient.Exer3, patientUid);
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error","Make sure the right number is less than the left number", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please fill in all min max values for each exercise\n", "OK");
            }
        }
        /// <summary>
        /// This method checks to see if all min max values are assigned to all exercises in the plan
        /// </summary>
        /// <returns></returns>
        private bool allMinMaxValuesFilledIn()
        {
            bool allNumbersAdded = true;
            allNumbersAdded = int.TryParse(min1.Text, out minimum1);
            allNumbersAdded = int.TryParse(max1.Text, out maximum1);
            allNumbersAdded = int.TryParse(min2.Text, out minimum2);
            allNumbersAdded = int.TryParse(max2.Text, out maximum2);
            allNumbersAdded = int.TryParse(min3.Text, out minimum3);
            allNumbersAdded = int.TryParse(max3.Text, out maximum3);
            return allNumbersAdded;
        }
        /// <summary>
        /// This function calls the checkIfMinLessThanMax method with the three differnt exercises
        /// </summary>
        private void checkMinMaxValuesAreCorrect()
        {
            isExer1Correct = checkIfMinLessThanMax(minimum1, maximum1, exercise1.Text);
            isExer2Correct = checkIfMinLessThanMax(minimum2, maximum2, exercise2.Text);
            isExer3Correct = checkIfMinLessThanMax(minimum3, maximum3, exercise3.Text);
        }
        /// <summary>
        /// This method checks if the min value is less than the max value for the exercise
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exerciseName"></param>
        /// <returns></returns>
        private bool checkIfMinLessThanMax(int min, int max, string exerciseName)
        {
            if (min > max)
            {
                return false;
            }
            return true;
        }
    }
}