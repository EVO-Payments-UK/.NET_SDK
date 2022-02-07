using GlobalTurnkey.Models.code;
using GlobalTurnkey.Models.config;
using GlobalTurnkey.Models.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlobalTurnkey.Models
{
    public class LoadPaymentFormCall : ApiCall
    {
        public LoadPaymentFormCall(ApplicationConfig config, Dictionary<String, String> inputParams) : base(config, inputParams)
        {

        }

        protected override void preValidateParams(Dictionary<String, String> inputParams)
        {
            List<String> requiredParams = new List<String>();
            requiredParams.Add("amount");
            requiredParams.Add("channel");
            requiredParams.Add("merchantNotificationUrl");
            requiredParams.Add("action");
            requiredParams.Add("country");
            requiredParams.Add("currency");
            requiredParams.Add("paymentSolutionId");
            foreach (KeyValuePair<String, String> entry in inputParams)
            {
                if (entry.Value != null && entry.Value.Trim().Length > 0)
                {
                    requiredParams.Remove(entry.Key);
                }
            }

            InputParamAfterValidation(requiredParams);
        }

        protected override Dictionary<String, String> getTokenParams(Dictionary<String, String> inputParams)
        {
            Dictionary<String, String> tokenParam = TokenParameters;
            GenerateRestParameters(inputParams, tokenParam, new ParamAuthHostedPaymentToken());
            return tokenParam;
        }

        protected override Dictionary<String, String> getActionParams(Dictionary<String, String> inputParams, String token)
        {
            Dictionary<String, String> actionParams = new Dictionary<String, String>();
            actionParams.Add("merchantId", config.merchantId);
            actionParams.Add("token", token);
            GenerateRestParameters(inputParams, actionParams, new ParamAuthHostedPaymentAction());

            return actionParams;
        }

    }
}