/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 * looked at https://app.exchangerate-api.com/
 */
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TestApp.Models;

namespace TestApp.Services
{
    /// <summary>
    /// This class returns the currency conversion rates to euro from exchangerateapi
    /// </summary>
    class CallToExcangeRateApi
    {
        private ExchangeRateModel Test;
        private Dictionary<string, double> exchangeRates;
        public CallToExcangeRateApi()
        {

        }
        /// <summary>
        /// This method calls a exchange rate api so the membership prices can reflect what the user should be paying in their currency
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, double> getCurrentExchangeRate()
        {
            String URLString = "https://v6.exchangerate-api.com/v6/a050309c53399e5e5f400366/latest/EUR";
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(URLString);
                Test = JsonConvert.DeserializeObject<ExchangeRateModel>(json);
            }
            ChangeValueToMap();
            return exchangeRates;
        }
        /// <summary>
        /// A map of the exchange rate prices tied to a currency key
        /// </summary>
        private void ChangeValueToMap()
        {
            exchangeRates = new Dictionary<string, double>();
            exchangeRates["AED"] = Test.conversion_rates.AED;
            exchangeRates["ARS"] = Test.conversion_rates.ARS;
            exchangeRates["AUD"] = Test.conversion_rates.AUD;
            exchangeRates["BGN"] = Test.conversion_rates.BGN;
            exchangeRates["BRL"] = Test.conversion_rates.BRL;
            exchangeRates["BSD"] = Test.conversion_rates.BSD;
            exchangeRates["CAD"] = Test.conversion_rates.CAD;
            exchangeRates["CHF"] = Test.conversion_rates.CHF;
            exchangeRates["CLP"] = Test.conversion_rates.CLP;
            exchangeRates["CNY"] = Test.conversion_rates.CNY;
            exchangeRates["COP"] = Test.conversion_rates.COP;
            exchangeRates["CZK"] = Test.conversion_rates.CZK;
            exchangeRates["DKK"] = Test.conversion_rates.DKK;
            exchangeRates["DOP"] = Test.conversion_rates.DOP;
            exchangeRates["EGP"] = Test.conversion_rates.EGP;
            exchangeRates["EUR"] = Test.conversion_rates.EUR;
            exchangeRates["FJD"] = Test.conversion_rates.FJD;
            exchangeRates["GBP"] = Test.conversion_rates.GBP;
            exchangeRates["GTQ"] = Test.conversion_rates.GTQ;
            exchangeRates["HKD"] = Test.conversion_rates.HKD;
            exchangeRates["HRK"] = Test.conversion_rates.HRK;
            exchangeRates["HUF"] = Test.conversion_rates.HUF;
            exchangeRates["IDR"] = Test.conversion_rates.IDR;
            exchangeRates["ILS"] = Test.conversion_rates.ILS;
            exchangeRates["INR"] = Test.conversion_rates.INR;
            exchangeRates["ISK"] = Test.conversion_rates.ISK;
            exchangeRates["JPY"] = Test.conversion_rates.JPY;
            exchangeRates["KRW"] = Test.conversion_rates.KRW;
            exchangeRates["KZT"] = Test.conversion_rates.KZT;
            exchangeRates["MXN"] = Test.conversion_rates.MXN;
            exchangeRates["MYR"] = Test.conversion_rates.MYR;
            exchangeRates["NOK"] = Test.conversion_rates.NOK;
            exchangeRates["NZD"] = Test.conversion_rates.NZD;
            exchangeRates["PAB"] = Test.conversion_rates.PAB;
            exchangeRates["PEN"] = Test.conversion_rates.PEN;
            exchangeRates["PHP"] = Test.conversion_rates.PHP;
            exchangeRates["PKR"] = Test.conversion_rates.PKR;
            exchangeRates["PLN"] = Test.conversion_rates.PLN;
            exchangeRates["PYG"] = Test.conversion_rates.PYG;
            exchangeRates["RON"] = Test.conversion_rates.RON;
            exchangeRates["RUB"] = Test.conversion_rates.RUB;
            exchangeRates["SAR"] = Test.conversion_rates.SAR;
            exchangeRates["SEK"] = Test.conversion_rates.SEK;
            exchangeRates["SGB"] = Test.conversion_rates.SGD;
            exchangeRates["THB"] = Test.conversion_rates.THB;
            exchangeRates["TRY"] = Test.conversion_rates.TRY;
            exchangeRates["TWD"] = Test.conversion_rates.TWD;
            exchangeRates["UAH"] = Test.conversion_rates.UAH;
            exchangeRates["USD"] = Test.conversion_rates.USD;
            exchangeRates["UYU"] = Test.conversion_rates.UYU;
            exchangeRates["ZAR"] = Test.conversion_rates.ZAR;
        }
    }
}
