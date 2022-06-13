/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;

namespace TestApp.Views
{
    /// <summary>
    /// This class will allow a physiotherapist to sign up to the application
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPhsysio : ContentPage
    {
        private Regex regexForLettersOnly = new Regex("^[a-zA-Z]*$");
        private bool isInfoCorrect = true;
        private string message = "";
        private PaymentViewModel pay = new PaymentViewModel();
        private RegisterPhysioViewModel vm;
        public RegisterPhsysio(IFirebaseAuthenticator Auth)
        {
            vm = new RegisterPhysioViewModel(Auth);
            InitializeComponent();
        }
        /// <summary>
        /// This method checks to see if only alphabetical charactera are entered for a name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkName(object sender, TextChangedEventArgs e)
        {
            if (regexForLettersOnly.IsMatch(e.NewTextValue))
            {
                name.Text = e.NewTextValue;
            }
        }
        /// <summary>
        /// This method checks to see if the two entered passwords match
        /// </summary>
        /// <param name="conPass"></param>
        /// <returns></returns>
        private bool checkToConfirmPass(string conPass)
        {
            if (pass.Text == null)
            {
                return false;
            }
            if (pass.Text.Equals(conPass))
            {
                return true;
            }
                return false;
        }
        /// <summary>
        /// This method checks to see if a valid email was entered
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool checkIfValidEmail(string email)
        {
            if(email == null)
            {
                return false;
            }
            if(email.Contains("@") && email.Contains("."))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// This button goes to the Payment page so they can select a membership and pay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void payment(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Payment(pay,false,""));
        }
        /// <summary>
        /// Return to login screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void login(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        /// <summary>
        /// This button allows a user to signup to the app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void registerPhysio(object sender, EventArgs e)
        {
            checkDetailsEnteredcorrectly();

            if (isInfoCorrect && pay.PaymentSuccessful)
            {
                DateTime dateAndTime = DateTime.Now;
                var date = dateAndTime.Date;
                DateTime membershipDate;
                if (pay.WhichMembershipSelectedSuccessful == 1)
                {
                     membershipDate = date.AddMonths(3);
                }
                else if(pay.WhichMembershipSelectedSuccessful == 2)
                {
                     membershipDate = date.AddMonths(6);
                }
                else
                {
                     membershipDate = date.AddMonths(12);
                }
                await vm.setUpPhysioAccount(email.Text, name.Text, pNumber.Text, membershipDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), conPass.Text,false);
                await DisplayAlert("Successs", "Sign up successful ", "Ok");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("error", message, "Ok");
                message = "";
                isInfoCorrect = true;
            }
        }
        /// <summary>
        /// This method checks to see if the form was filled out correctly
        /// </summary>
        /// <returns></returns>
        private string checkDetailsEnteredcorrectly()
        {
            if(name.Text == null)
            {
                isInfoCorrect = false;
                message += "You did not enter a name\n";
            }
            if (pNumber == null)
            {
                isInfoCorrect = false;
                message += "You did not enter a physio id number\n";
            }
            if (!checkIfValidEmail(email.Text))
            {
                isInfoCorrect = false;
                message += "You did not enter a valid email\n";
            }
            if (!checkToConfirmPass(conPass.Text))
            {
                isInfoCorrect = false;
                message += "The two passwords don't match\n";
            }
            if (pay.PaymentSuccessful == false)
            {
                isInfoCorrect = false;
                message += "You need to pay for a membership before you can register\n";
            }
            return message;
        }
    }
}