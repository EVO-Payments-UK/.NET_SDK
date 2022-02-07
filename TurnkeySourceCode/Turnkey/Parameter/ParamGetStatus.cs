using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turnkey.Parameter
{
    [Serializable]
    public class ParamGetStatusSessionToken : ParamBase
    {
        public readonly string merchantId = "merchantId";
        public readonly string password = "password";
        public readonly string action = "action";
        public readonly string timestamp = "timestamp";
        public readonly string allowOriginUrl = "allowOriginUrl";
    }

    [Serializable]
    public class ParamGetStatusAction : ParamBase
    {
        public readonly string txId = "txId";
        public readonly string merchantTxId = "merchantTxId";
    }
}