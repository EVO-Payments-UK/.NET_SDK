using System;
using System.Collections.Generic;
using Turnkey.config;
using Turnkey.code;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Turnkey.Tests.Models
{
    [TestClass]
    public class GetAvailablePaymentSolutionsTest
    {
        [TestMethod]
        public void GetAvailablePaymentSolutionsTestCall()
        {
            /*Init appliction configuration*/
            ApplicationConfig config = ObjectFactory.config;

            Dictionary<String, String> inputParams = new Dictionary<string, string>();
            inputParams.Add("country", CountryCode.FR.GetCode());
            inputParams.Add("currency", CurrencyCode.EUR.GetCode());

            //Do  GetAvailablePaymentSolutions
            GetAvailablePaymentSolutionsCall call = new GetAvailablePaymentSolutionsCall(config, inputParams);
            Dictionary<String, String> result = call.Execute();


            Assert.AreEqual(result["result"],"success");
        }
    }
}
