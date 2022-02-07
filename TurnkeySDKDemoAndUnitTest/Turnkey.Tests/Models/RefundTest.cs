using System;
using System.Collections.Generic;
using System.Threading;
using Turnkey.code;
using Turnkey.config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Turnkey.Tests.Models
{
    [TestClass]
    public class RefundTest
    {
        [TestMethod]
        [Description("This test case may need a long time due to waiting for the transaction status becoming captured")]
        public void AuthToRefundTestCall()
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

            //AUTH
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

            Assert.AreEqual(authResult["result"], "success");

            // CAPTURE
            if (authResult["result"] == "success" && authResult["status"]== "NOT_SET_FOR_CAPTURE")
            {
                Dictionary<String, String> captureParams = new Dictionary<String, String>();
                captureParams.Add("originalMerchantTxId", authResult["merchantTxId"]);
                captureParams.Add("amount", "20.0");

                CaptureCall call = new CaptureCall(config, captureParams);
                Dictionary<String, String> result = call.Execute();


                if (result["result"] == "success" && (result["status"] == "SET_FOR_CAPTURE"||result["status"]== "CAPTURED"))
                {
                    string status = string.Empty;
                    while(status != "CAPTURED")
                    {
                        Dictionary<String, String> statusParam = new Dictionary<String, String>();
                        statusParam.Add("txId", authResult["txId"]);

                        StatusCheckCall statusCall = new StatusCheckCall(config, statusParam);
                        Dictionary<String, String> statusResult = statusCall.Execute();
                        status = statusResult["status"];
                    }
                    Dictionary<String, String> refundParams = new Dictionary<String, String>();
                    refundParams.Add("originalMerchantTxId", result["originalMerchantTxId"]);
                    refundParams.Add("amount", "20.0");

                    RefundCall cCall = new RefundCall(config, refundParams);
                    Dictionary<String, String> cptrueResult = cCall.Execute();

                    Assert.AreEqual(cptrueResult["result"],"success");
                    Assert.AreEqual(cptrueResult["status"], "SET_FOR_REFUND");
                }
            }
        }

        [TestMethod]
        public void PurchaseToRefundTestCall()
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

            //Purchase
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

            PurchaseCall purchaseCall = new PurchaseCall(config, authParams);
            Dictionary<String, String> purchaseResult = purchaseCall.Execute();

            Assert.AreEqual(purchaseResult["result"], "success");

            if(purchaseResult["result"] == "success" && (purchaseResult["status"] == "SET_FOR_CAPTURE" || purchaseResult["status"] == "CAPTURED"))
            {
                string status = string.Empty;
                while (status != "CAPTURED")
                {
                    Dictionary<String, String> statusParam = new Dictionary<String, String>();
                    statusParam.Add("txId", purchaseResult["txId"]);

                    StatusCheckCall statusCall = new StatusCheckCall(config, statusParam);
                    Dictionary<String, String> statusResult = statusCall.Execute();
                    status = statusResult["status"];
                }

                Dictionary<String, String> refundParams = new Dictionary<String, String>();
                refundParams.Add("originalMerchantTxId", purchaseResult["merchantTxId"]);
                refundParams.Add("amount", "20.0");

                RefundCall cCall = new RefundCall(config, refundParams);
                Dictionary<String, String> cptrueResult = cCall.Execute();

                Assert.AreEqual(cptrueResult["result"], "success");
                Assert.AreEqual(cptrueResult["status"], "SET_FOR_REFUND");
            }
        }
    }
}
