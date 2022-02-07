using System;
using System.Collections.Generic;
using Turnkey.code;
using Turnkey.config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Turnkey.Tests.Models
{
    [TestClass]
    public class PurchaseTest
    {
        [TestMethod]
        public void PurchaseTestCall()
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

            // PURCHASE
            Dictionary<String, String> purchaseParams = new Dictionary<String, String>();
            purchaseParams.Add("amount", "20.0");
            purchaseParams.Add("channel", Channel.ECOM.GetCode());
            purchaseParams.Add("country", CountryCode.US.GetCode());
            purchaseParams.Add("currency", CurrencyCode.USD.GetCode());
            purchaseParams.Add("paymentSolutionId", "500");
            purchaseParams.Add("customerId", tokenizeResult["customerId"]);
            purchaseParams.Add("specinCreditCardToken", tokenizeResult["cardToken"]);
            purchaseParams.Add("specinCreditCardCVV", "111");
            purchaseParams.Add("merchantNotificationUrl", "http://localhost:8080/api/TransactionResultCallback");

            PurchaseCall call = new PurchaseCall(config, purchaseParams);
            Dictionary<String, String> result = call.Execute();

            Assert.AreEqual(result["result"], "success");
        }

    }
}
