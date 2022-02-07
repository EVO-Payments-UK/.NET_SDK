using System;
using System.Collections.Generic;
using Turnkey.code;
using Turnkey.config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Turnkey.Tests.Models
{
    [TestClass]
    public class StatusCheckTest
    {
        [TestMethod]
        [Description("Only txID")]
        public void txIdTestCall()
        {
            /*Init appliction configuration*/
            ApplicationConfig config = ObjectFactory.config;

            // TOKENIZE
            Dictionary<String, String> tokenizeParams = new Dictionary<string, string>();
            tokenizeParams.Add("number", "5424180279791732");
            tokenizeParams.Add("nameOnCard", "mastercard");
            tokenizeParams.Add("expiryYear", "2021");
            tokenizeParams.Add("expiryMonth", "04");

            TokenizeCall tokenizeCall = new TokenizeCall(config, tokenizeParams);
            Dictionary<String, String> tokenizeResult = tokenizeCall.Execute();

            Dictionary<String, String> authParams = new Dictionary<String, String>();
            authParams.Add("amount", "20.0");
            authParams.Add("channel", Channel.ECOM.GetCode());
            authParams.Add("country", CountryCode.PL.GetCode());
            authParams.Add("currency", CurrencyCode.PLN.GetCode());
            authParams.Add("paymentSolutionId", "500");
            authParams.Add("customerId", tokenizeResult["customerId"]);
            authParams.Add("specinCreditCardToken", tokenizeResult["cardToken"]);
            authParams.Add("specinCreditCardCVV", "111");
            authParams.Add("merchantNotificationUrl", "http://localhost:8080/api/TransactionResultCallback");

            AuthCall authCall = new AuthCall(config, authParams);
            Dictionary<String, String> authResult = authCall.Execute();

            Dictionary<String, String> inputParams = new Dictionary<String, String>();
            inputParams.Add("txId", authResult["txId"]);
            //inputParams.Add("merchantTxId", authResult["merchantTxId"]);

            StatusCheckCall call = new StatusCheckCall(config, inputParams);
            Dictionary<String, String> result = call.Execute();

            Assert.AreEqual(result["result"],"success");
        }

        [TestMethod]
        [Description("Only merchantTxId")]
        public void merchantTxIdTestCall()
        {
            /*Init appliction configuration*/
            ApplicationConfig config = ObjectFactory.config;

            // TOKENIZE
            Dictionary<String, String> tokenizeParams = new Dictionary<string, string>();
            tokenizeParams.Add("number", "5424180279791732");
            tokenizeParams.Add("nameOnCard", "mastercard");
            tokenizeParams.Add("expiryYear", "2021");
            tokenizeParams.Add("expiryMonth", "04");

            TokenizeCall tokenizeCall = new TokenizeCall(config, tokenizeParams);
            Dictionary<String, String> tokenizeResult = tokenizeCall.Execute();

            Dictionary<String, String> authParams = new Dictionary<String, String>();
            authParams.Add("amount", "20.0");
            authParams.Add("channel", Channel.ECOM.GetCode());
            authParams.Add("country", CountryCode.PL.GetCode());
            authParams.Add("currency", CurrencyCode.PLN.GetCode());
            authParams.Add("paymentSolutionId", "500");
            authParams.Add("customerId", tokenizeResult["customerId"]);
            authParams.Add("specinCreditCardToken", tokenizeResult["cardToken"]);
            authParams.Add("specinCreditCardCVV", "111");
            authParams.Add("merchantNotificationUrl", "http://localhost:8080/api/TransactionResultCallback");

            AuthCall authCall = new AuthCall(config, authParams);
            Dictionary<String, String> authResult = authCall.Execute();

            Dictionary<String, String> inputParams = new Dictionary<String, String>();
            //inputParams.Add("txId", authResult["txId"]);
            inputParams.Add("merchantTxId", authResult["merchantTxId"]);

            StatusCheckCall call = new StatusCheckCall(config, inputParams);
            Dictionary<String, String> result = call.Execute();

            Assert.AreEqual(result["result"], "success");
        }        
    }
}
