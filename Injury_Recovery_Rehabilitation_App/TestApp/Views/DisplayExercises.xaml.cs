/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System;
using System.Collections.Generic;
using System.Linq;
using TestApp.models;
using TestApp.Models;
using TestApp.ViewModels;
using TestApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.views
{
    /// <summary>
    /// This page is used to display a list of exercises in a picker
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayExercises : ContentPage
    {
        private DisplayExercisePlansViewModel viewModel;
        private List<PatientList> details = new List<PatientList>();
        private string physioUid = "";
        /// <summary>
        /// This constructor creates an instance of DisplayExercisesViewModel to call the FirebaseMethods methods
        /// </summary>
        public DisplayExercises(string physioUid,List<PatientList> details)
        {
            viewModel = new DisplayExercisePlansViewModel();
            this.physioUid = physioUid;
            this.details = details;
            InitializeComponent();
            
        }
        /// <summary>
        /// This OnAppearing method is used to populate a list view with a list of exercises by calling 
        /// the FirebaseMethods methods through DisplayExercisesViewModel
        /// Linq groupby is used to group exercises togather
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            MyListView.ItemsSource = null;
            List<ExercisePlan> physioPlanList = await viewModel.getPhysioPlans(physioUid);
            List<ExercisePlan> exercises = await viewModel.getExerciseList(false);
            if (physioPlanList != null)
            {
                exercises.AddRange(physioPlanList);
            }
            var sortedByCategorys = exercises.GroupBy(val => val.Category);
            MyListView.ItemsSource = sortedByCategorys;
        }
        /// <summary>
        /// This Details button press method returns with an exercise key and opens a new page called ExerciseDetail
        /// through DisplayExercisesViewModel
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="args"></param>
        private void Details(Object Sender, EventArgs args)
        {
            Button button = (Button)Sender;
            string exerciseKey = button.CommandParameter.ToString().Replace("\"", "");
            Navigation.PushModalAsync(new ExerciseDetail(exerciseKey, physioUid, details));
        }
    }
}
