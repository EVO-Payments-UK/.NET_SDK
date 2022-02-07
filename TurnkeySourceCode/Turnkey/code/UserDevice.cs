using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turnkey.code
{
    public class UserDevice
    {
        public static readonly UserDevice MOBILE = new UserDevice("MOBILE");
        public static readonly UserDevice DESKTOP = new UserDevice("DESKTOP");
        public static readonly UserDevice UNKNOWN = new UserDevice("UNKNOWN");

        private readonly String code;

        public UserDevice(String code)
        {
            this.code = code;
        }

        public String GetCode()
        {
            return code;
        }
    }
}