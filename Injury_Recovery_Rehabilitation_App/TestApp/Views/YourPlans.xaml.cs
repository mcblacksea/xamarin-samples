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
    /// This page is a menu for deleting updateing and creating a new exercise plan
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YourPlans : ContentPage
    {
        string physiouId = "";
        List<Exercise> exerciseList;
        List<ExercisePlan> physioPlans;
        private DisplayExercisePlansViewModel display = new DisplayExercisePlansViewModel();
        public YourPlans(string physiouid,List<Exercise> exerciseList)
        {
            InitializeComponent();
            physiouId = physiouid;
            this.exerciseList = exerciseList;
        }
        protected async override void OnAppearing()
        {
            physioPlans = await display.getPhysioPlans(physiouId);
            base.OnAppearing();
        }
        /// <summary>
        /// This button goes to add a new plan page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void createPlan(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new AddPlan(physiouId, exerciseList,null,"Add Plan"));
        }
        /// <summary>
        /// This button goes to edit delete page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void editDeletePlan(object sender, EventArgs args)
        {
            if (physioPlans.Count == 0)
            {
                await DisplayAlert("Error","You have no plans to edit or delete", "OK");
            }
            else
            {
                await Navigation.PushModalAsync(new SelectPlans(physiouId, exerciseList, physioPlans));
            }
        }
    }
}