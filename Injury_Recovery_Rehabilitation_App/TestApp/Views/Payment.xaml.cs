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
    /// This page does validation for the xaml form and calls methods from the PaymentViewModal to get the conversion rates 
    /// to euro
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Payment : ContentPage
    {
        private PaymentViewModel pay;
        private string cardName = null;
        private string cardNum = null;
        private string month = null;
        private string year = null;
        private string currency = null;
        private string cvv = null;
        private string enteredEmail = null;
        private string message = "";
        private string physioUserId = "";
        private long monthNumber = -1;
        private long yearNumber = -1;
        private long amount = -1;
        private bool isFormDetailsCorrect = true;
        private bool comingFromLogin = false;
        private Dictionary<string, double> exchangeRates;
        private SecurityViewModel security = new SecurityViewModel();
        internal Payment(PaymentViewModel pay,bool comingFromLogin,string physioUid)
        {
            this.pay = pay;
            InitializeComponent();
            this.comingFromLogin = comingFromLogin;
            physioUserId = physioUid;
        }
        /// <summary>
        /// initialise certain lists and calling of methods as page loads
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            string[] currencys = new[] {"USD","AED","AFN*","ALL","AMD","ANG","AOA*","ARS*","AUD","AWG","AZN","BAM","BBD","BDT","BGN","BIF","BMD","BND","BOB*","BRL*","BSD","BWP","BYN","BZD","CAD","CDF","CHF","CLP*","CNY","COP*","CRC*","CVE*","CZK","DJF*","DKK","DOP","DZDEGPETB","EUR","FJD","FKP*","GBP","GEL","GIP","GMD","GNF*","GTQ*","GYD","HKD,HNL*","HRK","HTG","HUF","IDR","ILS","INR*","ISK","JMD","JPY","KES","KGS","KHR","KMF","KRW","KYD","KZT","LAK*","LBPLKR","LRD","LSL","MAD","MDL","MGA","MKD","MMK","MNT","MOP","MRO","MUR*","MVR","MWK","MXN","MYR","MZN","NAD","NGN","NIO*","NOK","NPR","NZD","PAB*","PEN*","PGK","PHP","PKR","PLN","PYG*","QAR","RON","RSD","RUB","RWF","SAR","SBD","SCR","SEK","SGD","SHP*","SLL","SOS","SRD*","STD*","SZL","THB","TJS","TOP","TRY","TTD","TWD","TZS","UAH","UGX","UYU*","UZS","VND","VUV","WST","XAF","XCD","XOF*","XPF*","YER","ZAR","ZMW"};
            string[] years = new[] {"2022","2023","2024","2025","2026","2027","2028","2029","2030"};
            string[] months = new[] { "01", "02", "03", "04", "05", "06", "07", "08", "09","10","11","12"};
            currencyPicker.ItemsSource = currencys;
            monthPicker.ItemsSource = months;
            yearPicker.ItemsSource = years;
            exchangeRates = pay.getCurrentExchangeRate();
        }
        /// <summary>
        /// This method does a conversion on euro to the selected currency and displays it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void currencyConversion(object sender, EventArgs e)
        {
            double value = 0;
            double rate = 0;
            long conversionResult = 0;
            currency = currencyPicker.SelectedItem.ToString();
           
            if (exchangeRates.ContainsKey(currency))
            {
                rate = exchangeRates[currency];
            }
            value = 20 * rate;
            conversionResult = Convert.ToInt64(value);
            member1.Text = "" + conversionResult;
            value = 40 * rate;
            conversionResult = Convert.ToInt64(value);
            member2.Text = "" + conversionResult;
            value = 50 * rate;
            conversionResult = Convert.ToInt64(value);
            member3.Text = "" + conversionResult;
        }
        /// <summary>
        /// Allows a user to enter a maximum of 16 numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckCardNumber(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 16)
            {
                string cardNum = cardNumber.Text;
                cardNum = cardNum.Remove(cardNum.Length - 1);
                cardNumber.Text = cardNum;
            }
        }
        /// <summary>
        /// Allows a user to enter a maximum of 3 numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckCvvNumber(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 3)
            {
                string cvvNum = cvvNumber.Text;
                cvvNum = cvvNum.Remove(cvvNum.Length - 1);
                cvvNumber.Text = cvvNum;
            }
        }
        /// <summary>
        /// This button when pressed checks to make sure the currency is in the right format before being 
        /// converted into a long for stripe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pay1(object sender, EventArgs e)
        {
            pay.WhichMembershipSelectedSuccessful = 1;
            string value = member1.Text;
            if(value.Length == 2)
            {
                value += "00";
                long.TryParse(value, out amount);
            }
            if (value.Length == 3)
            {
                value += "0";
                long.TryParse(value, out amount);
            }
            else
            {
                long.TryParse(value, out amount);
            }
        }
        /// <summary>
        /// This button when pressed checks to make sure the currency is in the right format before being 
        /// converted into a long for stripe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pay2(object sender, EventArgs e)
        {
            pay.WhichMembershipSelectedSuccessful = 2;
            string value = member2.Text;
            if (value.Length == 2)
            {
                value += "00";
                long.TryParse(value, out amount);
            }
            if (value.Length == 3)
            {
                value += "0";
                long.TryParse(value, out amount);
            }
            else
            {
                long.TryParse(value, out amount);
            }
        }
        /// <summary>
        /// This button when pressed checks to make sure the currency is in the right format before being 
        /// converted into a long for stripe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pay3(object sender, EventArgs e)
        {
            pay.WhichMembershipSelectedSuccessful = 3;
            string value = member3.Text;
            if (value.Length == 2)
            {
                value += "00";
                long.TryParse(value, out amount);
            }
            if (value.Length == 3)
            {
                value += "0";
                long.TryParse(value, out amount);
            }
            else
            {
                long.TryParse(value, out amount);
            }
        }
        /// <summary>
        /// This button press checks to see if all forms details are filled out before sending them to the next page
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="args"></param>
        private async void makePayment(Object Sender, EventArgs args)
        {
            cardName = name.Text;
            cardNum = cardNumber.Text;
            if (monthPicker.SelectedIndex == -1)
            {
                month = null;
            }
            else
            {
                month = monthPicker.SelectedItem.ToString();
            }
            if (yearPicker.SelectedIndex == -1)
            {
                year = null;
            }
            else
            {
                year = yearPicker.SelectedItem.ToString();
            }
            if (currencyPicker.SelectedIndex == -1)
            {
                currency = null;
            }
            else
            {
                currency = currencyPicker.SelectedItem.ToString();
            }
            cvv = cvvNumber.Text;
            enteredEmail = email.Text;
            message = CheckDetailsEnteredcorrectly(cardName, cardNum, cvv, month, year, currency, amount, enteredEmail);
            if (isFormDetailsCorrect)
            {
                if(pay.makePayment(cardNum, yearNumber, monthNumber, cvv, cardName, enteredEmail, amount, currency))
                {
                    await DisplayAlert("Confirmed", "Thank you for your payment", "OK");
                    pay.PaymentSuccessful = true;
                    if (comingFromLogin == false)
                    {
                        await Navigation.PopModalAsync(true);
                    }
                    else
                    {
                        var date = DateTime.Today.Date;
                        DateTime membershipDate;
                        if (pay.WhichMembershipSelectedSuccessful == 1)
                        {
                            membershipDate = date.AddMonths(3);
                        }
                        else if (pay.WhichMembershipSelectedSuccessful == 2)
                        {
                            membershipDate = date.AddMonths(6);
                        }
                        else
                        {
                            membershipDate = date.AddMonths(12);
                        }
                        await security.renewMembership(false, physioUserId, membershipDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                        await Navigation.PopToRootAsync();
                    } 
                }
                else
                {
                    isFormDetailsCorrect = true;
                    message = "";
                    await DisplayAlert("error", "Your payment was not successful please check your entered information", "OK");
                }
            }
            else
            {
                isFormDetailsCorrect = true;
                await DisplayAlert("Error", message, "OK");
                message = "";
            }
        }
        /// <summary>
        /// validation method for the form
        /// </summary>
        /// <param name="cardName"></param>
        /// <param name="cardNumber"></param>
        /// <param name="cvvNumber"></param>
        /// <param name="enteredMonth"></param>
        /// <param name="enteredYear"></param>
        /// <param name="enteredCurrency"></param>
        /// <param name="memberShipAmount"></param>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        private string CheckDetailsEnteredcorrectly(string cardName,string cardNumber,string cvvNumber,string enteredMonth,string enteredYear,string enteredCurrency,long memberShipAmount,string newEmail)
        {
            string message = "";
            if(cardName == null)
            {
                message += "You did not enter a card name\n";
                isFormDetailsCorrect = false;
            }
            if(cardNumber == null)
            {
                message += "You did not enter a card number\n";
                isFormDetailsCorrect = false;
            }
            if(cvvNumber == null)
            {
                message += "You did not enter a cvv number\n";
                isFormDetailsCorrect = false;
            }
            if (enteredMonth == null)
            {
                message += "You did not enter a month\n";
                isFormDetailsCorrect = false;
            }
            else
            {
                long.TryParse(enteredMonth, out monthNumber);
            }
            if (enteredYear == null)
            {
                message += "You did not enter a year\n";
                isFormDetailsCorrect = false;
            }
            else
            {
                long.TryParse(enteredYear, out yearNumber);
            }
            if (!CheckEmail(newEmail))
            {
                message += "You did not enter a valid email\n";
                isFormDetailsCorrect = false;
            }
            if (amount == -1)
            {
                message += "You did not select a membership\n";
                isFormDetailsCorrect = false;
            }
            return message;
        }
        /// <summary>
        /// Checks to see if a valid email is entered
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool CheckEmail(string email)
        {
            if (email == null)
            {
                return false;
            }
            if (email.Contains("@") && email.Contains("."))
            {
                return true;
            }
            return false;
        }
    }
}