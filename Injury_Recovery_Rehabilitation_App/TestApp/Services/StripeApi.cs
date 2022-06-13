/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using Stripe;
using System;

namespace TestApp.Services
{
    /// <summary>
    /// This class takes in payment details with a form and allows a payment to take place using the stripe api
    /// </summary>
    class StripeApi
    {
        public StripeApi()
        {

        }
        /// <summary>
        /// This function creates a customer account and charges the new customer
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="yearExpires"></param>
        /// <param name="monthExpires"></param>
        /// <param name="cvcNumber"></param>
        /// <param name="cardName"></param>
        /// <param name="email"></param>
        /// <param name="amountPaid"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public bool makePayment(string cardNumber, long yearExpires, long monthExpires, string cvcNumber, string cardName, string email, long amountPaid, string currency)//https://www.youtube.com/watch?v=_b8kNxoGW3k used this video to learn how to use stripe
        {
            try
            {
                StripeConfiguration.SetApiKey("sk_test_51KLV7MADf8rgziOA4uajs5wRdQDS2vmsTlOlgOTfAqpEZ0Pgt0iGxevzp2IOQRlo89Jzc3Aia5aEhb7QzYiEWePy00VnyvgIra");
                Stripe.TokenCardOptions stripcard = new Stripe.TokenCardOptions();
                stripcard.Number = cardNumber;
                stripcard.ExpYear = yearExpires;
                stripcard.ExpMonth = monthExpires;
                stripcard.Cvc = cvcNumber;
                //Step 1 : Assign Card to Token Object and create Token
                Stripe.TokenCreateOptions token = new Stripe.TokenCreateOptions();
                token.Card = stripcard;
                Stripe.TokenService serviceToken = new Stripe.TokenService();
                Stripe.Token newToken = serviceToken.Create(token);
                // Step 2 : Assign Token to the Source
                var options = new SourceCreateOptions
                {
                    Type = SourceType.Card,
                    Currency = currency,
                    Token = newToken.Id
                };
                var service = new SourceService();
                Source source = service.Create(options);
                //Step 3 : Now generate the customer who is doing the payment
                Stripe.CustomerCreateOptions myCustomer = new Stripe.CustomerCreateOptions()
                {
                    Name = cardName,
                    Email = email,
                    Description = "Customer for " + email,
                };
                var customerService = new Stripe.CustomerService();
                Stripe.Customer stripeCustomer = customerService.Create(myCustomer);
                //Step 4 : Now Create Charge Options for the customer. 
                var chargeoptions = new Stripe.ChargeCreateOptions
                {
                    Amount = amountPaid,
                    Currency = currency,
                    ReceiptEmail = email,
                    Customer = stripeCustomer.Id,
                    Source = source.Id

                };
                //Step 5 : Perform the payment by  Charging the customer with the payment. 
                var service1 = new Stripe.ChargeService();
                Stripe.Charge charge = service1.Create(chargeoptions); // This will do the Payment
                return true;
            }
            catch (StripeException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
