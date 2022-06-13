/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System;
using System.Collections.Generic;
using TestApp.models;
using TestApp.Models;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    /// <summary>
    /// This ExerciseDetail page shows a detailed description of the exercise
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseDetail : ContentPage
    {
        private DisplayExercisePlansViewModel viewModel;
        private List<PatientList> details = new List<PatientList>();
        private String exerciseKey;
        private string physioUid = "";
        private int minimum1 = 0;
        private int maximum1 = 0;
        private int minimum2 = 0;
        private int maximum2 = 0;
        private int minimum3 = 0;
        private int maximum3 = 0;
        private ExercisePlan currentExercise = null;
        private string errorMessage = "The min value cannot be larger than the max value";
        private bool isCorrect = true;
        private bool isExer1Correct = false;
        private bool isExer2Correct = false;
        private bool isExer3Correct = false;
        /// <summary>
        ///  This constructor takes the exercise key from DisplayExercises Detail button press
        ///  and creates an instance of DisplayExercisesViewModel to access the FirebaseMethods class methods
        /// </summary>
        /// <param name="key"></param>
        /// <param name="physioUid"></param>
        public ExerciseDetail(String key, string physioUid, List<PatientList> details)
        {
            exerciseKey = key;
            viewModel = new DisplayExercisePlansViewModel();
            this.physioUid = physioUid;
            this.details = details;
            InitializeComponent();
        }
        /// <summary>
        /// This OnAppearing method is used to populate the stack view with the returned exercise details by using the
        /// exercise key
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            currentExercise = await viewModel.sendExerciseKey(exerciseKey, false);
            BindingContext = currentExercise;
        }
        /// <summary>
        /// This createPatient button passes exercise details new screen called register patient
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="args"></param>
        private async void createPatient(Object Sender, EventArgs args)
        {
            if (allMinMaxValuesFilledIn())
            {
                checkMinMaxValuesAreCorrect();
                if (isCorrect && isExer1Correct && isExer2Correct && isExer3Correct)
                {
                    await Navigation.PushModalAsync(new RegisterPatient(currentExercise, physioUid, minimum1, minimum2, minimum3, maximum1, maximum2, maximum3));
                }
                else
                {
                    await DisplayAlert("Error", errorMessage, "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please fill in all min max values for each exercise\n", "OK");
            }
        }
        /// <summary>
        /// This button is used to pass exercise details to new screen called add to existing patient 
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="args"></param>
        private async void selectPatient(Object Sender, EventArgs args)
        {

            if (allMinMaxValuesFilledIn())
            {
                checkMinMaxValuesAreCorrect();
                if (isCorrect && isExer1Correct && isExer2Correct && isExer3Correct)
                {
                    if(details.Count != 0)
                    {
                        await Navigation.PushModalAsync(new AddToExistingPatient(currentExercise, physioUid, minimum1, minimum2, minimum3, maximum1, maximum2, maximum3, details));
                    }
                    else
                    {
                        await DisplayAlert("Error","You have no current patients", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", errorMessage, "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please fill in all min max values for each exercise\n", "OK");
            }
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
        /// This method checks if the min value is less than the max value for the exercise
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exerciseName"></param>
        /// <returns></returns>
        private bool checkIfMinLessThanMax(int min,int max,string exerciseName)
        {
            if(min > max)
            {
                return false;
            }
            return true;
            
        }
    }
}