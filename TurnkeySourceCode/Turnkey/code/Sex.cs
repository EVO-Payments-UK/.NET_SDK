using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turnkey.code
{
    public class Sex
    {

        public static readonly Sex MALE = new Sex("MALE");
        public static readonly Sex FEMALE = new Sex("FEMALE");

        private readonly String code;

        public Sex(String code)
        {
            this.code = code;
        }

        public String GetCode()
        {
            return code;
        }

    }
}