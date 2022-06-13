/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System;
using System.Collections.Generic;
using TestApp.Models;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    /// <summary>
    /// This class is a menu to allow the patient to select which exercise they want to do first
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowPatientExercisePlan : ContentPage
    {
        private static DisplayPatientExercisePlanViewModel display = new DisplayPatientExercisePlanViewModel();
        private string downLoadLink1 = "";
        private string downLoadLink2 = "";
        private string downLoadLink3 = "";
        private bool exercise1Complete = false;
        private bool exercise2Complete = false;
        private bool exercise3Complete = false;
        private string videoFile = "";
        private string patientUid = "";
        private string exerciseName = "";
        private string videoCopyRight = "";
        private Patient details = null;
        private DateTime dateTime = DateTime.Today;
        private string date = "";
        private List<Exercise> patientExerciselist = new List<Exercise>();
        
        public ShowPatientExercisePlan(string uid)
        {
            patientUid = uid;
            InitializeComponent();
        }
        /// <summary>
        /// This method is used to populate data releated to the exercises from the database
        /// as the page is loading and check if an exercise has been completed
        /// </summary>
        protected async override void OnAppearing()
        {
            details = await display.getpatientDetails(patientUid, false);
            base.OnAppearing();
            date = dateTime.ToString();
            date = display.removeTimeFromDate(date);
            currentDate.Text = "Todays Date " + date;
            imgageDisplay.Source = "danLogo.png";
            if (patientExerciselist.Count == 0)
            {
                patientExerciselist = await display.getPatientsExercisePlan(patientUid, false);
            }
            exer1.Text = patientExerciselist[0].ExerciseName;
            exer2.Text = patientExerciselist[1].ExerciseName;
            exer3.Text = patientExerciselist[2].ExerciseName;
            
            if (display.ExerciseCompletedNumber == 1)
            {
                progress.Progress += 0.33;
                exer1.IsEnabled = false;
                exercise1Complete = true;
            }
            else if (display.ExerciseCompletedNumber == 2)
            {
                progress.Progress += 0.33;
                exer2.IsEnabled = false;
                exercise2Complete = true;
            }
            else if (display.ExerciseCompletedNumber == 3)
            {
                progress.Progress += 0.34;
                exer3.IsEnabled = false;
                exercise3Complete = true;
            }
        }
        /// <summary>
        /// This button gets the necessary exercise data ready for the first exercise so it can be passed to the next page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void exercise1(object sender, EventArgs e)
        {
            videoFile = "";
            videoFile = patientExerciselist[0].VideoLink;
            downLoadLink1 = await display.getExerciseVideoLink(videoFile, false);
            exerciseName = patientExerciselist[0].ExerciseName;
            videoCopyRight = "Video Copyright " + patientExerciselist[0].exerciseVideoCopyright;
            await Navigation.PushModalAsync(new ShowExerciseContent(downLoadLink1, exerciseName, videoCopyRight, patientUid,display,1,details.minExercise1,details.maxExercise1));
        }
        /// <summary>
        /// This button gets the necessary exercise data ready for the second exercise so it can be passed to the next page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void exercise2(object sender, EventArgs e)
        {
            videoFile = "";
            videoFile = patientExerciselist[1].VideoLink;
            downLoadLink2 = await display.getExerciseVideoLink(videoFile, false);
            exerciseName = patientExerciselist[1].ExerciseName;
            videoCopyRight = "Video Copyright " + patientExerciselist[1].exerciseVideoCopyright;
            await Navigation.PushModalAsync(new ShowExerciseContent(downLoadLink2, exerciseName, videoCopyRight, patientUid,display,2, details.minExercise2,details.maxExercise2));
        }
        /// <summary>
        /// This button gets the necessary exercise data ready for the third exercise so it can be passed to the next page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void exercise3(object sender, EventArgs e)
        {
            videoFile = "";
            videoFile = patientExerciselist[2].VideoLink;
            downLoadLink3 = await display.getExerciseVideoLink(videoFile, false);
            exerciseName = patientExerciselist[2].ExerciseName;
            videoCopyRight = "Video Copyright " + patientExerciselist[2].exerciseVideoCopyright;
            await Navigation.PushModalAsync(new ShowExerciseContent(downLoadLink3, exerciseName, videoCopyRight, patientUid, display,3,details.minExercise3,details.maxExercise3));
        }
        /// <summary>
        /// This button saves the state of the current exercise plan when all exercises are completed for th
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void saveResult(object sender, EventArgs e)
        {
            if(exercise1Complete == true && exercise2Complete == true && exercise3Complete == true)
            {
                string date = dateTime.ToString("dd/MM/yyyy");
                date = date.Replace("/", "-");
                bool isComplete =  await display.saveStateOfExercisePlan(patientUid, date,false);
                await DisplayAlert("Message", "Exercise Plan Saved", "OK");
                await Application.Current.MainPage.Navigation.PopModalAsync(true);
            }
            else
            {
                await DisplayAlert("Message", "You have not completed all exercises", "OK");
            }
        }
    }
}