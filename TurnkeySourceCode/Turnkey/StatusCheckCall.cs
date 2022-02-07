//-----------------------------------------------------------------------
// <copyright file="StatusCheckCall.cs" company="nodus">
//     nodus.  All rights reserved.
// </copyright>
// <author>martin</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Turnkey.code;
using Turnkey.config;
using Turnkey.exception;
using Turnkey.Parameter;

namespace Turnkey
{
    /// <summary>
    /// Status check action
    /// </summary>
    public class StatusCheckCall : ApiCall
    {
        public StatusCheckCall(ApplicationConfig config, Dictionary<String, String> inputParams) : base(config, inputParams)
        {

        }

        protected override void PreValidateParams(Dictionary<String, String> inputParams)
        {
            if(!inputParams.ContainsKey("txId") && !inputParams.ContainsKey("merchantTxId"))
                throw new RequireParamException("Filed txId or merchantTxId must be provided at least one.");
        }

        protected override Dictionary<String, String> GetTokenParams(Dictionary<String, String> inputParams)
        {
            Dictionary<String, String> tokenParam = TokenParameters;
            tokenParam.Add("action", ActionType.GET_STATUS.GetCode());

            GenerateRestParameters(inputParams, tokenParam, new ParamGetStatusSessionToken());

            return tokenParam;
        }

        protected override Dictionary<String, String> GetActionParams(Dictionary<String, String> inputParams, String token)
        {
            Dictionary<String, String> actionParams = new Dictionary<String, String>();
            actionParams.Add("merchantId", config.MerchantId);
            actionParams.Add("token", token);

            GenerateRestParameters(inputParams, actionParams, new ParamGetStatusAction());

            return actionParams;
        }
    }
}