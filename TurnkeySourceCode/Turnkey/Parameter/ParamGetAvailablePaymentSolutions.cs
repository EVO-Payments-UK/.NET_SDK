using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turnkey.Parameter
{
    [Serializable]
    public class ParamGetAvailablePaymentSolutionsSessionToken : ParamBase
    {
        public readonly string merchantId = "merchantId";
        public readonly string password = "password";
        public readonly string action = "action";
        public readonly string timestamp = "timestamp";
        public readonly string allowOriginUrl = "allowOriginUrl";
        public readonly string country = "country";
        public readonly string currency = "currency";

        public readonly string brandId = "brandId";
    }

    [Serializable]
    public class ParamGetAvailablePaymentSolutionsAction : ParamBase
    {
    }
}