using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Turnkey.Parameter;
using System.Xml;
using Turnkey.exception;

namespace Turnkey.config
{
    /// <summary>
    /// This class defines some paramters user need to pass to gateway to do merchant's action and to get token.
    /// Also defines information such as session token url, action url cashier url which merchant can use to raise a request.
    /// </summary>
    public class ApplicationConfig
    {
        //Session token url 
        public string SessionTokenRequestUrl { get;}

        //Operation action url
        public string PaymentOperationActionUrl { get;}

        //Cashier url
        public string CashierUrl { get;}

        //Mobile cashier url
        public string CashierMobileUrl { get;}

        //Javascript url
        public string JavaScriptUrl { get;}
        public string AllowOriginUrl { get; set; }
        public string MerchantNotificationUrl { get; set; }
        public string MerchantLandingPageUrl { get; set; }
        public string MerchantId { get; set; }
        public string Password { get; set; }

        //Indication current environment is UAT or Production
        public string TurnkeyEnvironment { get; set; }
        public ApplicationConfig(string merchantId,string password,string allowOriginUrl,string merchantNotificationUrl,string merchantLandingPageUrl="",string environment="UAT")
        { 
            MerchantId = merchantId;
            Password = password;
            AllowOriginUrl = allowOriginUrl;
            MerchantNotificationUrl = merchantNotificationUrl;
            MerchantLandingPageUrl = merchantLandingPageUrl;
            TurnkeyEnvironment = environment;

            try
            {
                /*Deep to root directory to get application configuration file*/
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                if(!basePath.EndsWith(@"\"))
                    basePath = basePath+@"\";
                string paramsPath = basePath + "EnvParams.xml";
                XmlDocument xml = new XmlDocument();
                xml.Load(paramsPath);

                /*Get readonly values from application configuration file for user using*/
                XmlNode paramNode = xml.DocumentElement.SelectSingleNode(string.Format("Environment[@value='{0}']",environment));
                SessionTokenRequestUrl = paramNode.SelectSingleNode("SessionTokenRequestUrl") == null ? "" : paramNode.SelectSingleNode("SessionTokenRequestUrl").InnerText;
                PaymentOperationActionUrl = paramNode.SelectSingleNode("PaymentOperationActionUrl") == null ? "" : paramNode.SelectSingleNode("PaymentOperationActionUrl").InnerText;
                CashierUrl = paramNode.SelectSingleNode("CashierUrl") == null ? "" : paramNode.SelectSingleNode("CashierUrl").InnerText;
                CashierMobileUrl = paramNode.SelectSingleNode("CashierMobileUrl") == null ? "" : paramNode.SelectSingleNode("CashierMobileUrl").InnerText;
                JavaScriptUrl = paramNode.SelectSingleNode("JavaScriptUrl") == null ? "" : paramNode.SelectSingleNode("JavaScriptUrl").InnerText;
            }
            catch(Exception ex)
            {
                Tools.WriteToLogFile(ex.ToString());
                throw new GeneralException("An internal error occurred");
            }
        }
    }
}