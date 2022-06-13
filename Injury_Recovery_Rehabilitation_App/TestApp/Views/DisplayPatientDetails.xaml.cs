/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using TestApp.models;
using TestApp.Models;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    /// <summary>
    /// This page display a patients details in a form
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayPatientDetails : ContentPage
    {
        private IFirebaseAuthenticator auth = DependencyService.Get<IFirebaseAuthenticator>();
        private RegisterPatientViewModel model;
        private Patient patient;
        private ExercisePlan exercisePlan;
        private int min1 = 0;
        private int min2 = 0;
        private int min3 = 0;
        private int max1 = 0;
        private int max2 = 0;
        private int max3 = 0;
        private string patientUid = "";
        private string injuryType = "";
        private string injuryOccurred = "";
        private bool infoCorrect = true;
        private string errorMessage = "";
        private DateTime today;
        private string startDate;
        private string endDate;
        private DateTime sDate;
        private DateTime nDate;
        private int result1;
        private int result2;
        string physioUid = "";
        private string gender;
        private string patientName;
        private int patientAge;
        private string patientEmail;
        private int severityNumber;
        private string newInjuryType;
        private string newinjuryOccurred;
        private string encryptKey = "";

        public DisplayPatientDetails(Patient patient, ExercisePlan exercisePlan, string patientUid, int min1, int min2, int min3, int max1, int max2, int max3,string physioUid,string encryptionKey)
        {
            InitializeComponent();
            this.patient = patient;
            this.exercisePlan = exercisePlan;
            this.min1 = min1;
            this.min2 = min2;
            this.min3 = min3;
            this.max1 = max1;
            this.max2 = max2;
            this.max3 = max3;
            this.patientUid = patientUid;
            injury.IsEnabled = false;
            injury.IsVisible = false;
            InjuryLabel.IsVisible = false;
            occured.IsEnabled = false;
            occured.IsVisible = false;
            OccuredLabel.IsVisible = false;
            this.physioUid = physioUid;
            model = new RegisterPatientViewModel(auth);
            encryptKey = encryptionKey;
        }
        /// <summary>
        /// As the screen is loading some variables are set into the patient form  
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            name.Text = patient.PatientName;
            age.Text = patient.Age.ToString();
            email.Text = patient.Email;
            if (patient.Gender == "Male")
            {
                genderPicker.SelectedIndex = 0;
            }
            else
            {
                genderPicker.SelectedIndex = 1;
            }
        }
        /// <summary>
        /// This picker method is used to hide and unhide a entry field depending on the form choice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InjuryPicked(object sender, EventArgs e)
        {
            injuryType = Injurypicker.SelectedItem.ToString();
            if (injuryType == "Enter Your Own")
            {
                InjuryLabel.IsVisible = true;
                injury.IsVisible = true;
                injury.IsEnabled = true;
            }
            else
            {
                injury.Text = null;
                injury.IsEnabled = false;
                injury.IsVisible = false;
                InjuryLabel.IsVisible = false;
            }
        }
        /// <summary>
        /// This picker event hides the Optional Additional Injury entry if something is picked on the picker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InjuryOccurred(object sender, EventArgs e)
        {
            injuryOccurred = Occurredpicker.SelectedItem.ToString();
            if (injuryOccurred == "Enter Your Own")
            {
                OccuredLabel.IsVisible = true;
                occured.IsVisible = true;
                occured.IsEnabled = true;
            }
            else
            {
                occured.Text = null;
                occured.IsEnabled = false;
                occured.IsVisible = false;
                OccuredLabel.IsVisible = false;
            }
        }
        /// <summary>
        /// This method sends the form data to a view model to be entered into the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void startNewPlan(object sender, EventArgs e)
        {
            checkFormvalidation();
            if (infoCorrect == false)
            {
                await DisplayAlert("Error", errorMessage, "OK");
                infoCorrect = true;
            }
            else
            {
                Dictionary<string, bool> planDates = new Dictionary<string, bool>();
                for (var dt = sDate; dt <= nDate; dt = dt.AddDays(1))
                {
                    string date = date = dt.ToString("dd/MM/yyyy");
                    date = date.Replace("/", "-");
                    planDates.Add(date, false);
                }
                await model.addNewPlanToExistingPatient(patientName, gender, patientEmail, injuryType, injuryOccurred, patientAge, severityNumber, planDates, exercisePlan, physioUid, newInjuryType, newinjuryOccurred, min1, min2, min3, max1, max2, max3, patientUid, encryptKey);
                await Navigation.PushModalAsync(new PhysioMenuScreen(physioUid));
            }
        }
        /// <summary>
        /// This method checks the form validation
        /// </summary>
        /// <returns></returns>
        private bool checkFormvalidation()
        {

            // check if gender was picked
            if(genderPicker.SelectedIndex != -1)
            {
                gender = genderPicker.SelectedItem.ToString();
            }
            
            if (gender == "")
            {
                errorMessage += "Please select a gender\n";
                infoCorrect = false;
            }
            //check if name was entered
            patientName = name.Text;
            if (patientName == null)
            {
                errorMessage += "Please enter patient name\n";
                infoCorrect = false;
            }
            //check if age was entered
            int.TryParse(age.Text, out patientAge);
            if (patientAge == 0 || patientAge < 0)
            {
                errorMessage += "Please enter patient age\n";
                infoCorrect = false;
            }
            //check if email was entered and is valid
            patientEmail = email.Text;
            if (patientEmail == null)
            {
                errorMessage += "Please enter a valid patient email\n";
                infoCorrect = false;
            }
            else
            {
                if (patientEmail.Contains("@") && patientEmail.Contains("."))
                {

                }
                else
                {
                    errorMessage += "Please enter a valid email\n";
                    infoCorrect = false;
                }
            }
            //check severity number was picked
            if (SeverityPicker.SelectedIndex != -1)
            {
                int.TryParse(SeverityPicker.SelectedItem.ToString(), out severityNumber);
            }
            else
            {
                errorMessage += "Please pick a severity number\n";
            }
            //check if an injury type was picked or a new injury type was entered
            newInjuryType = injury.Text;
            if (newInjuryType == null)
            {
                try
                {
                    injuryType = Injurypicker.SelectedItem.ToString();
                    if (injuryType == "Enter Your Own")
                    {
                        errorMessage += "Please pick an injury Type or enter a new injury type info\n";
                        infoCorrect = false;
                    }
                }
                catch (NullReferenceException error)
                {
                    Console.WriteLine(error.StackTrace);
                    errorMessage += "Please pick an injury Type or enter your own\n";
                    infoCorrect = false;
                }
            }
            //check if a new injury occurred by selecting in the picker or was entered
            newinjuryOccurred = occured.Text;
            if (newinjuryOccurred == null)
            {
                try
                {
                    injuryOccurred = Occurredpicker.SelectedItem.ToString();
                    if (injuryOccurred == "Enter Your Own")
                    {
                        errorMessage += "Please pick how Injury occurred or enter your own\n";
                        infoCorrect = false;
                    }

                }
                catch (NullReferenceException error)
                {
                    Console.WriteLine(error.StackTrace);
                    errorMessage += "Please pick how Injury occurred or enter additional injury info\n";
                    infoCorrect = false;
                }
            }

            today = DateTime.Today;
            startDate = startDatePicker.Date.ToString("dd/MM/yyyy");
            endDate = endDatePicker.Date.ToString("dd/MM/yyyy");
            CultureInfo objcul = new CultureInfo("en-GB");
            sDate = DateTime.ParseExact(startDate, "dd/MM/yyyy", objcul);
            nDate = DateTime.ParseExact(endDate, "dd/MM/yyyy", objcul);
            result1 = DateTime.Compare(sDate, today);
            result2 = DateTime.Compare(nDate, today);
            //check if the start date of the plan is less than the end of the plan
            if (result1 < 0 || result2 < 0)
            {
                errorMessage += "Please pick a date from today onwards\n";
                infoCorrect = false;
            }
            return infoCorrect;
        }
    }
}