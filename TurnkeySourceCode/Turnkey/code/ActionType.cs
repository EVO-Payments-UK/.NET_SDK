using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turnkey.code
{
    public class ActionType
    {

        public static readonly ActionType TOKENIZE = new ActionType("TOKENIZE");
        public static readonly ActionType AUTH = new ActionType("AUTH");
        public static readonly ActionType VERIFY = new ActionType("VERIFY");
        public static readonly ActionType CAPTURE = new ActionType("CAPTURE");
        public static readonly ActionType VOID = new ActionType("VOID");
        public static readonly ActionType PURCHASE = new ActionType("PURCHASE");
        public static readonly ActionType REFUND = new ActionType("REFUND");
        public static readonly ActionType GET_AVAILABLE_PAYSOLS = new ActionType("GET_AVAILABLE_PAYSOLS");
        public static readonly ActionType GET_STATUS = new ActionType("GET_STATUS");

        private readonly String code;

        public ActionType(String code)
        {
            this.code = code;
        }

        public String GetCode()
        {
            return code;
        }

        public static IEnumerable<ActionType> ActionTypes
        {
            get
            {
                yield return TOKENIZE;
                yield return AUTH;
                yield return CAPTURE;
                yield return VOID;
                yield return PURCHASE;
                yield return REFUND;
                yield return GET_AVAILABLE_PAYSOLS;
                yield return GET_STATUS;
                yield return VERIFY;
            }
        }




    }
}