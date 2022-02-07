using System;
using System.Collections.Generic;
using Turnkey.code;
using Turnkey.config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Turnkey.Tests.Models
{
    [TestClass]
    public class VerifyTest
    {
        [TestMethod]
        public void VerifyTestCall()
        {
            /*Init appliction configuration*/
            ApplicationConfig config = ObjectFactory.config;

            // TOKENIZE
            Dictionary<String, String> inputParams = new Dictionary<string, string>();
            inputParams.Add("number", "5424180279791732");
            inputParams.Add("nameOnCard", "mastercard");
            inputParams.Add("expiryYear", "2021");
            inputParams.Add("expiryMonth", "04");

            TokenizeCall tokenizeCall = new TokenizeCall(config, inputParams);
            Dictionary<String, String> tokenizeResult = tokenizeCall.Execute();

            Dictionary<String, String> authParams = new Dictionary<String, String>();
            authParams.Add("amount", "0");
            authParams.Add("channel", Channel.ECOM.GetCode());
            authParams.Add("country", CountryCode.PL.GetCode());
            authParams.Add("currency", CurrencyCode.PLN.GetCode());
            authParams.Add("paymentSolutionId", "500");
            authParams.Add("customerId", tokenizeResult["customerId"]);
            authParams.Add("specinCreditCardToken", tokenizeResult["cardToken"]);
            authParams.Add("specinCreditCardCVV", "111");
            authParams.Add("merchantNotificationUrl", "http://localhost:8080/api/TransactionResultCallback");

            //Do verify
            VerifyCall call = new VerifyCall(config, authParams);
            Dictionary<String, String> result = call.Execute();

            Assert.AreEqual(result["result"], "success");

        }
    }
}
