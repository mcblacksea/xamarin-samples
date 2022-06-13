/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using Plugin.FirebasePushNotification;
using System;
using System.Globalization;
using TestApp.ViewModels;
using TestApp.views;
using TestApp.Views;
using Xamarin.Forms;
namespace TestApp
{
    /// <summary>
    /// This class is the first screen of the application allows a user to enter login details
    /// or go to the physio register page
    /// </summary>
    public partial class MainPage : ContentPage
    {
        private IFirebaseAuthenticator auth;
        private SecurityViewModel secuirty;
        private bool emailCorrect = true;
        private CultureInfo objcul = new CultureInfo("en-GB");
        private DateTime currentMembership;
        private DateTime currentDate = DateTime.Today.Date;
        bool togglePassword = true;
        /// <summary>
        /// This is the login page for both apps
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
            {
                CrossFirebasePushNotification.Current.OnNotificationReceived += Current_OnNotificationReceived;
            }
            secuirty = new SecurityViewModel();
            auth = DependencyService.Get<IFirebaseAuthenticator>();//https://github.com/xamarin/GooglePlayServicesComponents/issues/391
            //Navigation.PushModalAsync(new DisplayExercises("CL7a2BcjGKgKdkBFhWYGndw5Xz63"));
            //Navigation.PushModalAsync(new ShowPatientExercisePlan("tNiEslj2QdWvOz0HU4xJvOev5K32"));
            //Navigation.PushModalAsync(new DisplayProgress("tNiEslj2QdWvOz0HU4xJvOev5K32"));
            //Navigation.PushModalAsync(new PhysioMenuScreen("wReLTtb9SdZXpYOTbFTQDSacas93"));
            //Navigation.PushModalAsync(new PatientMenuScreen("Xmy0ofjszSe9DohrrH1uezsfbmz1"));
            /////////////////////////////////////////////////////////////////////
        }
        private void Current_OnNotificationReceived(object source, FirebasePushNotificationDataEventArgs e)
        {
            DisplayAlert("Notification", $"Data: {e.Data["myData"]}", "OK");
        }
        /// <summary>
        /// This button sends the entered email and password to ether the ios or android depending which class calls the 
        /// IFirebaseAuthenticator interface
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LoginClicked(object sender, EventArgs e)
        {
            //string Email = email.Text;
            //string Pass = pass.Text;
            //if(Email == null)
            //{
            //    emailCorrect = false;
            //}
            //else if (Email.Contains("@") && Email.Contains("."))
            //{
                
            //}
            //else
            //{
            //    await DisplayAlert("Error", "Please enter a valid email\n", "OK");
            //    emailCorrect = false;
            //}
            //if(emailCorrect != false && Pass != null)
            //{
            //    string userId = await auth.LoginWithEmailPassword(Email, Pass);

            //    if (userId == "true")//used if using an ios phone
            //    {
            //        userId = await secuirty.checkIfLoginIsVerified(Email, Pass,false);
            //    }
            //    if (userId != "")
            //    {
            //        string userType = await secuirty.checkUserType(userId,false);
            //        if (userType == "patient")
            //        {
            //            await DisplayAlert("Login Successful", "", "OK");
            //            await Navigation.PushAsync(new PatientMenuScreen(userId));
            //        }
            //        else if(userType == "physio")
            //        {
            //            currentMembership = await secuirty.getPhysioMembership(false, userId);
            //            int result = DateTime.Compare(currentDate, currentMembership);
            //            if (result <= 0)
            //            {
                            await DisplayAlert("Login Successful", "", "OK");
                            // Attempt to play 'fake' video
                            //await Navigation.PushModalAsync(new ShowExerciseContent("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", "1", "11", "1", null, 1, 1, 1));
                            await Navigation.PushAsync(new PhysioMenuScreen("1"));
            //            }
            //            else
            //            {
            //                PaymentViewModel pay = new PaymentViewModel();
            //                await DisplayAlert("error","Your membership has expired please renew your membership", "OK");
            //                await Navigation.PushAsync(new Payment(pay,true, userId));
            //            }
            //        }
            //        else
            //        {
            //            await DisplayAlert("error", "An error has occurred please try entering the email and password again", "OK");
            //        }
            //    }
            //    else
            //    {
            //        await DisplayAlert("Login Failed", "E-mail or password are incorrect. Try again!", "OK");
            //    }
            //}
            //emailCorrect = true;
        }
        /// <summary >
        /// This method will bring the physiotherapist registration page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPhsysio(auth));
        }
        /// <summary>
        /// Used to display password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showPass(object sender, EventArgs e)
        {

            togglePassword = !togglePassword;
            pass.IsPassword = togglePassword;
        }
    }
}
