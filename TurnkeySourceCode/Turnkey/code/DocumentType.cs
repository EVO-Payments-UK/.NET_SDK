using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turnkey.code
{
    public class DocumentType
    {

        public static readonly DocumentType PASSPORT = new DocumentType("PASSPORT");
        public static readonly DocumentType NATIONAL_ID = new DocumentType("NATIONAL_ID");
        public static readonly DocumentType DRIVING_LICENSE = new DocumentType("DRIVING_LICENSE");
        public static readonly DocumentType UNIQUE_TAXPAYER_REFERENCE = new DocumentType("UNIQUE_TAXPAYER_REFERENCE");
        public static readonly DocumentType OTHER = new DocumentType("OTHER");

        private readonly String code;

        public DocumentType(String code)
        {
            this.code = code;
        }

        public String GetCode()
        {
            return code;
        }
    }
}