using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turnkey.code
{
    public class Channel
    {
        public static readonly Channel ECOM = new Channel("ECOM");
        public static readonly Channel MOTO = new Channel("MOTO");

        private readonly String code;

        public Channel(String code)
        {
            this.code = code;
        }

        public String GetCode()
        {
            return code;
        }

    }
}