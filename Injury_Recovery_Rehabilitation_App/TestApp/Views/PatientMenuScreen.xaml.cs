/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System;
using System.Collections.Generic;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    /// <summary>
    /// This page displays the menu for the patient
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientMenuScreen : ContentPage
    {
        DisplayPatientExercisePlanViewModel model = new DisplayPatientExercisePlanViewModel();
        PlanProgressViewModel plan = new PlanProgressViewModel();
        string patientId = "";
        DateTime today;
        Dictionary<string, bool> progressPlan;
        public PatientMenuScreen(string uid)
        {
            InitializeComponent();
            patientId = uid;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            imgageDisplay.Source = "danLogo.png";
        }
        /// <summary>
        /// This button goes to the ShowPatientExercisePlan page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void goToExercisePlan(object sender, EventArgs args)
        {
            today = DateTime.Today;
            string date = today.ToString("dd/MM/yyyy");
            date = date.Replace("/", "-");
            bool isCompleteForToday = await model.checkIfExercisePlanCompleteForToday(date, patientId,false);
            if (isCompleteForToday)
            {
                await DisplayAlert("Error", "You have already completed your exercise for today or your plan is finished", "OK");
            }
            else
            {
                await Navigation.PushModalAsync(new ShowPatientExercisePlan(patientId));
            }
        }
        /// <summary>
        /// This button goes to the DisplayProgress page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void goToPatientProgress(object sender, EventArgs args)
        {
            progressPlan = await plan.getPatientProgress(patientId);
            await Navigation.PushModalAsync(new DisplayProgress(patientId,progressPlan));
        }
        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}