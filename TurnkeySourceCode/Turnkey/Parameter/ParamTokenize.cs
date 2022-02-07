using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turnkey.Parameter
{
    [Serializable]
    public class ParamTokenizeSessionToken:ParamBase
    {
        public readonly string merchantId = "merchantId";
        public readonly string password = "password";
        public readonly string action = "action";
        public readonly string timestamp = "timestamp";
        public readonly string allowOriginUrl = "allowOriginUrl";
        public readonly string customerId = "customerId";
        public readonly string cardDescription = "cardDescription";
    }

    [Serializable]
    public class ParamTokenizeAction: ParamBase
    {
        public readonly string number = "number";
        public readonly string nameOnCard = "nameOnCard";
        public readonly string expiryMonth = "expiryMonth";
        public readonly string expiryYear = "expiryYear";
        public readonly string cardDescription = "cardDescription";
        public readonly string startMonth = "startMonth";
        public readonly string startYear = "startYear";
        public readonly string issueNumber = "issueNumber";
    }
}