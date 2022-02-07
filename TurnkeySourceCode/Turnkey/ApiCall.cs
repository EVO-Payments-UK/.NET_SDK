//-----------------------------------------------------------------------
// <copyright file="ApiCall.cs" company="nodus">
//     nodus.  All rights reserved.
// </copyright>
// <author>martin</author>
//-----------------------------------------------------------------------

namespace Turnkey
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Turnkey.config;
    using Turnkey.exception;
    using Turnkey.Parameter;
    
    
    public abstract class ApiCall
    {
       
	    protected ApplicationConfig config;
        private static readonly HttpClient client = new HttpClient();
        protected Dictionary<String, String> inputParams;
        protected string validationMessage = string.Empty;
        private static Dictionary<String, String> tokenParams = new Dictionary<string, string>();

        //Define global token used parameters
        public Dictionary<String, String> TokenParameters
        {
            get
            {
                tokenParams.Clear();
                tokenParams.Add("merchantId", config.MerchantId);
                tokenParams.Add("password", config.Password);
                tokenParams.Add("timestamp", (Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds)).ToString());
                tokenParams.Add("allowOriginUrl", config.AllowOriginUrl);
                return tokenParams;
            }
        }

        /// <summary>
        /// Init base call object, validate the required parameters.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="inputParams"></param>
         public ApiCall(ApplicationConfig config, Dictionary<String, String> inputParams) {
             
             try
             {
                 this.config = config;
                 FormatInputParams(inputParams);
                 this.inputParams = inputParams;
                 PreValidateParams(inputParams); 
             }
             catch(RequireParamException ex)
             {
                Tools.WriteToLogFile(ex.ToString());
                validationMessage = ex.Message;
             }
             catch(Exception ex) {
                Tools.WriteToLogFile(ex.ToString());
                throw new RequireParamException("An internal error occurred");
             }
         }

        /// <summary>
        /// If the input parameter contains url from html or other resource, we should decode it first
        /// </summary>
        /// <param name="inputParams"></param>
        private void FormatInputParams(Dictionary<String, String> inputParams)
        {
            try
            {
                List<string> allNeedChangedKeys = new List<string>();
                foreach (var item in inputParams)
                {
                    if (item.Value.ToLower().StartsWith("http"))
                        allNeedChangedKeys.Add(item.Key);
                }

                foreach (string item in allNeedChangedKeys)
                {
                    inputParams[item] = HttpUtility.UrlDecode(inputParams[item], Encoding.GetEncoding(936));
                }
            }
            catch(Exception ex)
            {
                Tools.WriteToLogFile(ex.ToString());
                throw new GeneralException("An internal error occurred");
            }
        }
        public void InputParamAfterValidation(List<String> requiredParams)
        {
            if (requiredParams.Count > 0)
            {
                string message = string.Empty;
                foreach (string param in requiredParams)
                {
                    message += param + ",";
                }
                message = message.Substring(0, message.Length - 1);
                //throw new RequireParamException("Pre-validation failed: " + message + " required.");
                throw new RequireParamException("A request parameter was missing or invalid");
            }
        }

        /// <summary>
        /// Post requested paramters to web server that is specified by url paramter.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramMap"></param>
        /// <returns></returns>
        public static async Task<String> PostToApi(String url, Dictionary<String, String> paramMap) {
            String apiResponseString = "";
            try
            {
                var wb = new WebClient();
                NameValueCollection nameValueCollection = new NameValueCollection();
                foreach (var kvp in paramMap)
                {
                    nameValueCollection.Add(kvp.Key.ToString(), kvp.Value.ToString());
                }
                
                var response = wb.UploadValues(url, "POST", nameValueCollection);
                apiResponseString = Encoding.UTF8.GetString(response);

            }
            catch (Exception ex) {
                Tools.WriteToLogFile(ex.ToString());
                var exception =  new PostToApiException("A communication error occurred");
                throw exception;
            }


            return apiResponseString;
        }

        /// <summary>
        /// Except global paramters, get other paramters the token/action needs
        /// </summary>
        /// <param name="inputParams"></param>
        /// <param name="outputParams"></param>
        /// <param name="parameter"></param>
        protected void GenerateRestParameters(Dictionary<String, String> inputParams, Dictionary<String, String> outputParams,ParamBase parameter)
        {
            Dictionary<String, String> allParams = Tools.GetDictionaryFromObject(parameter);
            foreach (string value in allParams.Values)
            {
                if (inputParams.ContainsKey(value)&& !outputParams.ContainsKey(value))
                    outputParams.Add(value, inputParams[value]);
            }
        }
        protected abstract Dictionary<String, String> GetTokenParams(Dictionary<String, String> inputParams);

        protected abstract Dictionary<String, String> GetActionParams(Dictionary<String, String> inputParams, String token);

        protected abstract void PreValidateParams(Dictionary<String, String> inputParams);

        /// <summary>
        /// This function is to execute the operation action, having two steps:
        /// 1. Get session token
        /// 2. If the session token is got successfully, then do the operation action.
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, String> Execute()
        {
            try
            {  
                if(!string.IsNullOrEmpty(validationMessage))
                {
                    Dictionary<string, string> result = new Dictionary<string, string>();
                    result.Add("result","failure");
                    result.Add("message", validationMessage);
                    return result;
                }

                //Get session token
                Task<String> tokenResponse = PostToApi(config.SessionTokenRequestUrl, GetTokenParams(inputParams));

                Dictionary<String, String> values = Tools.JsonToDictionary(tokenResponse.Result);

                //If seesion token received successfully, then do the operation action using the token, else return the failure response directly.
                if (values["result"] != "failure")
                {
                    String token = values["token"];

                    Dictionary<String, String> actionParams = GetActionParams(inputParams, token);
                    if (actionParams == null)
                    {
                        return values;
                    }
                    Task<String> actionResponse = PostToApi(config.PaymentOperationActionUrl, actionParams);

                    values = Tools.JsonToDictionary(actionResponse.Result);

                    return values;
                }
                else
                {
                    return values;
                }
            }
            catch (Exception ex)
            {
                Tools.WriteToLogFile(ex.ToString());
                var exception = new ActionCallException("A token error occurred");
                throw exception;
            }
        }
    }
}