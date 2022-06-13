/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    /// <summary>
    /// This page displays a patients progress with calender
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayProgress : ContentPage
    {
        private Dictionary<string, bool> progressPlan;
        private string stringDate = "";
        public DisplayProgress(string uid, Dictionary<string, bool> progressPlan)
        {
            InitializeComponent();
            this.progressPlan = progressPlan;
        }
        /// <summary>
        /// As the page loads I pass a dictionary so it the populate  calendar method
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            populateCalendar(progressPlan);
        }
        /// <summary>
        /// This method populates a calander with a dictionary of dates
        /// </summary>
        /// <param name="progressPlan"></param>
        private void populateCalendar(Dictionary<string, bool> progressPlan)
        {
            CultureInfo objcul = new CultureInfo("en-GB");
            calendar.SpecialDates = new List<XamForms.Controls.SpecialDate>();
            int i = 0;
            while (i < progressPlan.Count)
            {
                stringDate = progressPlan.Keys.ElementAt(i);
                stringDate = stringDate.Replace("-", "/");
                
                DateTime currentDate = DateTime.ParseExact(stringDate, "dd/MM/yyyy", objcul);
                if (currentDate.Date == DateTime.Today)
                {
                    calendar.SpecialDates.Add(new XamForms.Controls.SpecialDate(currentDate) { BackgroundColor = Color.SteelBlue, TextColor = Color.Black, BorderColor = Color.White, BorderWidth = 0, Selectable = false });
                }
                else
                {
                    if (progressPlan.Values.ElementAt(i) == true)
                    {

                        calendar.SpecialDates.Add(new XamForms.Controls.SpecialDate(currentDate) { BackgroundColor = Color.Green, TextColor = Color.Black, BorderColor = Color.White, BorderWidth = 0, Selectable = false });
                    }
                    else
                    {

                        calendar.SpecialDates.Add(new XamForms.Controls.SpecialDate(currentDate) { BackgroundColor = Color.Red, TextColor = Color.Black, BorderColor = Color.White, BorderWidth = 0, Selectable = false });
                    }
                }
                i++;
            }
        }
        /// <summary>
        /// This bu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void menu(object sender, EventArgs e)
        {
             await Navigation.PopModalAsync();
        }
    }
}