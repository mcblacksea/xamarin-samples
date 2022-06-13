/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    /// <summary>
    /// This page gives a form that can be used to try and predict the 
    /// type of injury a patient might get when performing an activity
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PredictInjuryType : ContentPage
    {
        string sLevel = null;
        string gender = null;
        string stringAge = null;
        string isInjuryBefore = null;
        string selectedActivity = null;
        PredictViewModel predict;
        private RadioButton genderButton, injuryButton;

        public PredictInjuryType()
        {
            InitializeComponent();
            predict = new PredictViewModel();
        }
        /// <summary>
        /// Picker for skill level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skillLevel(object sender, EventArgs e)
        {
            if (skill.SelectedIndex != -1)
            {
                sLevel = skill.SelectedItem.ToString();
            }
        }
        /// <summary>
        /// Radio button for gender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getGender(object sender, CheckedChangedEventArgs e)
        {
            genderButton = sender as RadioButton;
            gender = genderButton.Content.ToString();
        }
        /// <summary>
        /// Picker for age
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAge(object sender, EventArgs e)
        {
            if (age.SelectedIndex != -1)
            {
                stringAge = age.SelectedItem.ToString();
            }
        }
        /// <summary>
        /// Radio button previous injury
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getInjuryBefore(object sender, CheckedChangedEventArgs e)
        {
            injuryButton = sender as RadioButton;
            isInjuryBefore = injuryButton.Content.ToString();
        }
        /// <summary>
        /// Picker for patient activity eg Rugby
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectActivity(object sender, EventArgs e)
        {
            if (activity.SelectedIndex != -1)
            {
                selectedActivity = activity.SelectedItem.ToString();
            }
        }
        /// <summary>
        /// Send values to view model for processing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void sendValues(object sender, EventArgs e)
        {
            if(sLevel != null && gender != null && stringAge != null && isInjuryBefore != null && selectedActivity != null)
            {
                string result = predict.predictWithValues(sLevel, isInjuryBefore, stringAge, gender, selectedActivity);
                await DisplayAlert("The model predicted", result, "OK");
            }
            else
            {
                await DisplayAlert("Error","You did not select all the values", "OK");
            }
        }
    }
}