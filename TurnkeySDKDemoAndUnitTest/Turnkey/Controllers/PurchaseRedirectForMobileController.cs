using Turnkey.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Turnkey.exception;

namespace Turnkey.Controllers
{
    /// <summary>
    /// When user want to do purchase on cashier(only for mobile), user can refer to this class. 
    /// </summary>
    public class PurchaseRedirectForMobileController : ApiController
    {
        public PurchaseRedirectForMobileController()
        {
        }

        /// <summary>
        /// This method contains two part:
        /// Part1: Get a purchase token using parameters provided by web page.
        /// Part2: Get mobile cashier url and pass it to web page
        /// Notes: Because this is a demo project, part1 is just a sample to show user how to get a purchase token, this token can be used when user do purchase.
        ///        The final purpose of this method is to get a mobile cashier url for user using to open a mobile cashier page.
        /// </summary>
        /// <returns></returns>
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

                /*Execute the call and get the response*/
                Dictionary<String, String> executeData = new PurchaseTokenCall(config, inputParams).Execute();

                /*Get merchantID and token from the response, add them to input parameters, this input parameters will be used as cashier url parameters.
                  Currently we don't need this token data, as we just want get the mobile cashier url, but in this sample we prepare the token data so as to future using */
                inputParams.Add("merchantId", config.MerchantId);
                inputParams.Add("token", executeData["token"]);

                /*Get cashier url(for mobile) from application configuration object*/
                String url = config.CashierMobileUrl;

                /*Define a dictionary object, add the url to it, thus the dictionary object can be used by "CreateResponse" method which pass the url data to web page */
                Dictionary<String, String> response = new Dictionary<string, string>();
                response.Add("url", url);

                //Return the url data to web page
                return Request.CreateResponse(HttpStatusCode.OK, response);
                
            }
            catch (RequireParamException ex) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Missing fields: " + ex.ToString());
            }
            catch (TokenAcquirationException ex) {
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
