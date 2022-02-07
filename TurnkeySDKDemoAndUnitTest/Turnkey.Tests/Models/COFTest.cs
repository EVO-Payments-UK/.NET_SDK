using Turnkey.config;
using System;
using System.Collections.Generic;
using Turnkey.code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Turnkey.Tests.Models
{
    /// <summary>
    /// This class contains three test cases: CIT_AuthTestCall,RecurringInitiate_Purchase_TestCall,MIT_AuthTestCall
    /// CIT_AuthTestCall completes a CIT with authorization
    /// MIT_AuthTestCall completes a MIT with authorization
    /// RecurringInitiate_Purchase_TestCall completes a recurring initialization with purchase, adding additional parameters which is required for recurring payment such as mmrpContractNumber,mmrpBillPayment...
    /// </summary>
    [TestClass]
    public class COFTest
    {
        /// <summary>
        /// To initiate a card on file transaction by completing an authorization transaction with card on file required parameters
        /// This is the first in a series of COF transactions
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private Dictionary<String, String> COFFirstTran_Auth(ApplicationConfig config)
        {
            Dictionary<String, String> inputParams = new Dictionary<string, string>();
            inputParams.Add("number", "5424180279791732");
            inputParams.Add("nameOnCard", "mastercard");
            inputParams.Add("expiryYear", "2021");
            inputParams.Add("expiryMonth", "04");

            inputParams.Add("cardOnFileType", "First");

            TokenizeCall tokenizeCall = new TokenizeCall(config, inputParams);
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

            AuthCall call = new AuthCall(config, authParams);
            Dictionary<String, String> result = call.Execute();

            return result;
        }

        /// <summary>
        /// Do a CIT transaction which uses previous stored credentials to complete an authorization
        /// The merchantTrxID is returned from the initiated authorization transaction field "merchantTxId"
        /// Not just authorization support CIT, but Verify and purchase also support CIT
        /// </summary>
        /// <param name="config"></param>
        /// <param name="merchantTraID"></param>
        /// <returns></returns>
        private Dictionary<String, String> COFSubTrans_CIT_Auth(ApplicationConfig config, string merchantTrxID)
        {
            Dictionary<String, String> inputParams = new Dictionary<string, string>();
            inputParams.Add("number", "5424180279791732");
            inputParams.Add("nameOnCard", "mastercard");
            inputParams.Add("expiryYear", "2021");
            inputParams.Add("expiryMonth", "04");

            inputParams.Add("cardOnFileType", "Repeat");
            inputParams.Add("cardOnFileInitiator", "Cardholder");
            inputParams.Add("cardOnFileInitialTransactionId", merchantTrxID);

            TokenizeCall tokenizeCall = new TokenizeCall(config, inputParams);
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

            AuthCall call = new AuthCall(config, authParams);
            Dictionary<String, String> result = call.Execute();

            return result;
        }

        /// <summary>
        /// Complete a CIT with Authorization.
        /// Not just authorization support CIT, but Verify and purchase also support CIT
        /// </summary>
        [TestMethod]
        public void CIT_AuthTestCall()
        {
            /*Init appliction configuration*/
            ApplicationConfig config = ObjectFactory.config;

            Dictionary<String, String> firstCOFAuthResult = COFFirstTran_Auth(config);

            Dictionary<String, String> subCITAuthResult = COFSubTrans_CIT_Auth(config, firstCOFAuthResult["merchantTxId"]);

            Assert.AreEqual(subCITAuthResult["result"], "redirection");

        }

        /// <summary>
        /// To initiate a recurring transaction by completing an purchase transaction with card on file required parameters
        /// For Recurring Payments this must be “PURCHASE” or “VERIFY”
        /// Fields mmrpCustomerPresent, mmrpContractNumber ,mmrpExistingDebt,mmrpCurrentInstallmentNumber,mmrpOriginalMerchantTransactionId are required when mmrpBillPayment is set to "Recurring"
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private Dictionary<String, String> COFInitiateRecurringTran_Purchase(ApplicationConfig config, string merchantTrxID)
        {
            Dictionary<String, String> inputParams = new Dictionary<string, string>();
            inputParams.Add("number", "5424180279791732");
            inputParams.Add("nameOnCard", "mastercard");
            inputParams.Add("expiryYear", "2021");
            inputParams.Add("expiryMonth", "04");

            //Card on file parameter
            inputParams.Add("cardOnFileType", "Repeat");
            inputParams.Add("cardOnFileInitiator", "Merchant");
            inputParams.Add("cardOnFileInitialTransactionId", merchantTrxID);

            //Recurring parameter
            inputParams.Add("mmrpBillPayment", "Recurring");
            inputParams.Add("mmrpCustomerPresent", "BillPayment");
            inputParams.Add("mmrpContractNumber", "12345678");
            inputParams.Add("mmrpExistingDebt", "NotExistingDebt");
            inputParams.Add("mmrpCurrentInstallmentNumber", "1");
            inputParams.Add("mmrpOriginalMerchantTransactionId", merchantTrxID);

            TokenizeCall tokenizeCall = new TokenizeCall(config, inputParams);
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

            PurchaseCall call = new PurchaseCall(config, authParams);
            Dictionary<String, String> result = call.Execute();

            return result;
        }

        /// <summary>
        /// Complete a recurring payment initial with purchase.
        /// For Recurring Payments this must be “PURCHASE” or “VERIFY”
        /// </summary>
        [TestMethod]
        public void RecurringInitiate_Purchase_TestCall()
        {
            /*Init appliction configuration*/
            ApplicationConfig config = ObjectFactory.config;

            Dictionary<String, String> firstCOFAuthResult = COFFirstTran_Auth(config);

            Dictionary<String, String> recurringResult = COFInitiateRecurringTran_Purchase(config, firstCOFAuthResult["merchantTxId"]);

            Assert.AreEqual(recurringResult["result"], "redirection");

        }


        /// <summary>
        /// Do a MIT transaction which uses previous stored credentials to complete an authorization
        /// The merchantTrxID is returned from the initiated authorization transaction field "merchantTxId"
        /// Not just authorization support MIT, but Verify and purchase also support MIT
        /// </summary>
        /// <param name="config"></param>
        /// <param name="merchantTraID"></param>
        /// <returns></returns>
        private Dictionary<String, String> COFSubTrans_MIT_Auth(ApplicationConfig config, string merchantTrxID)
        {
            Dictionary<String, String> inputParams = new Dictionary<string, string>();
            inputParams.Add("number", "5424180279791732");
            inputParams.Add("nameOnCard", "mastercard");
            inputParams.Add("expiryYear", "2021");
            inputParams.Add("expiryMonth", "04");

            inputParams.Add("cardOnFileType", "Repeat");
            inputParams.Add("cardOnFileInitiator", "Merchant");
            inputParams.Add("cardOnFileInitialTransactionId", merchantTrxID);

            TokenizeCall tokenizeCall = new TokenizeCall(config, inputParams);
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

            AuthCall call = new AuthCall(config, authParams);
            Dictionary<String, String> result = call.Execute();

            return result;
        }


        /// <summary>
        /// Complete a MIT with Authorization.
        /// Not just authorization support MIT, but Verify and purchase also support MIT
        /// </summary>
        [TestMethod]
        public void MIT_AuthTestCall()
        {
            /*Init appliction configuration*/
            ApplicationConfig config = ObjectFactory.config;

            Dictionary<String, String> firstCOFAuthResult = COFFirstTran_Auth(config);

            Dictionary<String, String> subCITAuthResult = COFSubTrans_MIT_Auth(config, firstCOFAuthResult["merchantTxId"]);

            Assert.AreEqual(subCITAuthResult["result"], "redirection");

        }
    }
}
