using Turnkey.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Turnkey.Controllers
{
    /// <summary>
    /// Complete an status check request, three steps:
    /// 1. Get request paramters from httpContent object
    /// 2. Pass the request parameters to action call method to execute the action
    /// 3. Get the response data and return to web page
    /// </summary>
    public class StatusCheckController : ApiController
    {
        public async Task<object> Post()

        {
            /* Get request paramters from HttpContent object, parse it to dictionary format*/
            HttpContent requestContent = Request.Content;
            string res = requestContent.ReadAsStringAsync().Result;
            Dictionary<String, String> requestData = Tools.requestToDictionary(res);

            /*Init appliction configuration, get a config object*/
            string merchantID = Properties.Settings.Default.merchantId;
            string password = Properties.Settings.Default.password;
            string merchantNotificationUrl = Properties.Settings.Default.merchantNotificationUrl;
            string allowOriginUrl = Properties.Settings.Default.allowOriginUrl;
            string merchantLandingPageUrl = Properties.Settings.Default.merchantLandingPageUrl;
            string environment = Properties.Settings.Default.TurnkeySdkConfig;

            ApplicationConfig config = new ApplicationConfig(merchantID, password, allowOriginUrl, merchantNotificationUrl,
                                                             merchantLandingPageUrl, environment);

            /*Execute the action call and get the response*/
            StatusCheckCall statusCheckCall = new StatusCheckCall(config, requestData);
            Dictionary<string, string> response = statusCheckCall.Execute();

            //return the response data to web page
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
