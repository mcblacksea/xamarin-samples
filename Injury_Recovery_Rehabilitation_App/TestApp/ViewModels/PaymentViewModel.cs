/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System.Collections.Generic;
using TestApp.Services;

namespace TestApp.ViewModels
{
    /// <summary>
    /// This class sends the form data to the stripe api and retives the current exchange rates to euro
    /// </summary>
    class PaymentViewModel
    {
        private bool isPaid = false;
        private int membership = 0;
        private CallToExcangeRateApi rateApi = new CallToExcangeRateApi();
        private StripeApi stripeApi = new StripeApi();
        public PaymentViewModel()
        {

        }
        /// <summary>
        /// This method sends payment details to the stripe api method and returns a true value if successful       
        /// <param name="cardNumber"></param>
        /// <param name="yearExpires"></param>
        /// <param name="monthExpires"></param>
        /// <param name="cvcNumber"></param>
        /// <param name="cardName"></param>
        /// <param name="email"></param>
        /// <param name="amountPaid"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public bool makePayment(string cardNumber,long yearExpires,long monthExpires,string cvcNumber,string cardName,string email,long amountPaid,string currency)
        {
            return stripeApi.makePayment(cardNumber, yearExpires, monthExpires, cvcNumber, cardName, email, amountPaid, currency);
        }
        /// <summary>
        /// This method calls exchange rate api to return other currency exchnage rates to euro in a dictionary structure
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, double> getCurrentExchangeRate()//https://app.exchangerate-api.com/
        {
            return rateApi.getCurrentExchangeRate();
        }
        /// <summary>
        /// Used to check if payment was successful
        /// </summary>
        public bool PaymentSuccessful
        {
            get { return isPaid; }
            set { isPaid = value; }
        }
        /// <summary>
        /// Used to get the selected membership so I can enter how long the user membership is for
        /// </summary>
        public int WhichMembershipSelectedSuccessful
        {
            get { return membership; }
            set { membership = value; }
        }
    }
}
