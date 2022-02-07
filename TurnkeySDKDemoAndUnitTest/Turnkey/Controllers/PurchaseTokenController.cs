using Turnkey.config;
using Turnkey.exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Turnkey.Controllers
{
    /// <summary>
    /// Complete an purchase token request, three steps:
    /// 1. Get request paramters from httpContent object
    /// 2. Pass the request parameters to the call method to execute the call
    /// 3. Get the response data and return to web page
    /// </summary>
    public class PurchaseTokenController : ApiController
    {
        public async Task<object> Post()

        {
            try
            {
                /* Get request paramters from HttpContent object, parse it to dictionary format*/
                HttpContent requestContent = Request.Content;
                string res = requestContent.ReadAsStringAsync().Result;
                Dictionary<String, String> inputParams = Tools.requestToDictionary(res);

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
                Dictionary<String, String> executeData = new PurchaseTokenCall(config, inputParams).Execute();

                /*In the response data, we can add more data, such as cashierUrl, mobileCashierUrl*/
                inputParams["merchantId"] = config.MerchantId;
                inputParams["token"] = executeData["token"];
                inputParams["cashierUrl"] = config.CashierUrl;
                inputParams["mobileCashierUrl"] = config.CashierMobileUrl;

                //return the response data to web page
                return Request.CreateResponse(HttpStatusCode.OK, inputParams);

            }
            catch (RequireParamException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Missing fields: " + ex.ToString());
            }
            catch (TokenAcquirationException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Could not acquire token: " + ex.ToString());
            }
            catch (PostToApiException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Outgoing POST failed: " + ex.ToString());
            }
            catch (GeneralException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "General SDK error: " + ex.ToString());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error: " + ex.ToString());
            }


        }
    }
}
