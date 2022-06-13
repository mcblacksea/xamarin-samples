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
    /// This page shows a list of patients assigned to the physio with the use of a picker
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectPatientToView : ContentPage
    {
        private List<PatientList> details;
        private Dictionary<string, bool> progressPlan;
        private PlanProgressViewModel plan = new PlanProgressViewModel();
        private DisplayPatientExercisePlanViewModel model = new DisplayPatientExercisePlanViewModel();
        private string patientUid = "";
        private string patientName = "";
        private bool isViewProgress;
        public SelectPatientToView(List<PatientList> details,string titleName,bool isViewProgress)
        {
            InitializeComponent();
            this.details = details;
            labelName.Text = titleName;
            this.isViewProgress = isViewProgress;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            List<string> names = new List<string>();
            foreach(PatientList detail in details)
            {
                names.Add(detail.PatientName);
            }
            namesPicker.ItemsSource = names;
        }
        /// <summary>
        /// Allows the patient id associated with the name to be sent to the next screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void viewProgress(object sender, EventArgs e)
        {
            if(namesPicker.SelectedIndex != -1)
            {
                patientName = namesPicker.SelectedItem.ToString();
                foreach (PatientList detail in details)
                {
                    if(patientName == detail.PatientName)
                    {
                        patientUid = detail.PatientUid;
                    }
                }
                if (isViewProgress)
                {
                    progressPlan = await plan.getPatientProgress(patientUid);
                    await Navigation.PushModalAsync(new DisplayProgress(patientUid, progressPlan));
                }
                else
                {
                    Patient patient = await model.getpatientDetails(patientUid, false);
                    await Navigation.PushModalAsync(new UpdatePatientExerciseAmount(patientUid, patient));
                }
            }
            else
            {
                await DisplayAlert("Error", "Please select one of the patient names", "OK");
            }
        }
    }
}