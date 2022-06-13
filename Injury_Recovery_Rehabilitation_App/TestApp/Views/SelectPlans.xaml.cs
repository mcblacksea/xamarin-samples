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
    /// This page view allows the physio to select from a list of created exercise plans 
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectPlans : ContentPage
    {
        string physiouId = "";
        List<Exercise> exerciseList;
        List<ExercisePlan> physioPlans;
        ExercisePlan selectdPlan;
        ExerciseViewModel vm = new ExerciseViewModel();
        public SelectPlans(string physiouid, List<Exercise> exerciseList, List<ExercisePlan> physioPlans)
        {
            InitializeComponent();
            physiouId = physiouid;
            this.exerciseList = exerciseList;
            this.physioPlans = physioPlans;
        }
        /// <summary>
        /// This method fills the picker with exercise plan names
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            List<string> names = new List<string>();
            foreach (ExercisePlan plan in physioPlans)
            {
                names.Add(plan.exerciseName);
            }
            namesPicker.ItemsSource = names;
        }
        /// <summary>
        /// Button goes to AddPlan page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void editPlan(object sender, EventArgs args)
        {
            if (getExercisePlanFromPicker())
            {
                await Navigation.PushModalAsync(new AddPlan(physiouId, exerciseList, selectdPlan, "Edit Plan"));
            }
        }
        /// <summary>
        /// Button goes to deletePlanToDb page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void deletePlan(object sender, EventArgs args)
        {
            if (getExercisePlanFromPicker())
            {
                await vm.deletePlanToDb(physiouId,selectdPlan.exerciseName);
                await Navigation.PopModalAsync();
            }
        }
        /// <summary>
        /// This method assigns an exercise plan object from a list of exercise plans
        /// </summary>
        /// <returns></returns>
        public bool getExercisePlanFromPicker()
        {
            if (namesPicker.SelectedIndex != -1)
            {
                foreach (ExercisePlan plan in physioPlans)
                {
                    if (plan.exerciseName == namesPicker.SelectedItem.ToString())
                    {
                        selectdPlan = plan;
                    }
                }
                return true;
            }
            return false;
        }
    }
}