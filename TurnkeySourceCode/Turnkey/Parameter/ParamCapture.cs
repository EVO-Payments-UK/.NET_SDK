using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turnkey.Parameter
{
    [Serializable]
    public class ParamCaptureSessionToken : ParamBase
    {
        public readonly string merchantId = "merchantId";
        public readonly string password = "password";
        public readonly string action = "action";
        public readonly string timestamp = "timestamp";
        public readonly string allowOriginUrl = "allowOriginUrl";
        public readonly string originalMerchantTxId = "originalMerchantTxId";
        public readonly string amount = "amount";

        public readonly string originalTxId = "originalTxId";
        public readonly string agentId = "agentId";
    }

    [Serializable]
    public class ParamCaptureAction : ParamBase
    {
    }
}