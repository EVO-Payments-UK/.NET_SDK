using System;
using System.Collections.Generic;
using Turnkey.config;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace Turnkey.Tests.Models
{
    [TestClass]
    public class TokenizeTest
    {
        [TestMethod]
        public void noExTestCall()
        {
            Dictionary<String, String> inputParams = new Dictionary<string, string>();
            inputParams.Add("number", "5424180279791732");
            inputParams.Add("nameOnCard", "mastercard");
            inputParams.Add("expiryYear", "2021");
            inputParams.Add("expiryMonth", "04");
            inputParams.Add("customerId", "123456789");
            inputParams.Add("cardDescription", "test");


            /*Init appliction configuration*/
            ApplicationConfig config = ObjectFactory.config;

            //Do tokenize
            TokenizeCall call = new TokenizeCall(config, inputParams);
            Dictionary<String, String> result = call.Execute();

            Assert.AreEqual(result["result"],"success");
        }

        [TestMethod]
        [Description("Expected: expiryMonth is required.")]
        public void ExExpTestCall()
        {
            Dictionary<String, String> inputParams = new Dictionary<string, string>();
            inputParams.Add("number", "5424180279791732");
            inputParams.Add("nameOnCard", "mastercard");
            inputParams.Add("expiryYear", "2010");

            /*Init appliction configuration*/
            ApplicationConfig config = ObjectFactory.config;

            TokenizeCall call = new TokenizeCall(config, inputParams);
            Dictionary<String, String> result = call.Execute();

            Assert.AreEqual(result["result"],"failure");
        }
    }
}
