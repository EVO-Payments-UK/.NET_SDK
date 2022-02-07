using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turnkey.config;

namespace Turnkey.Tests
{
    public class ObjectFactory
    {
        public static ApplicationConfig config
        {
            get
            {
                if (m_config == null)
                {
                    string merchantID = "167885";
                    string password = "56789";
                    string merchantNotificationUrl = "http://localhost:8080/api/TransactionResultCallback";
                    string allowOriginUrl = "http://localhost:8080";
                    string merchantLandingPageUrl = "http://localhost:8080/";
                    string environment = "UAT";

                    m_config = new ApplicationConfig(merchantID, password, allowOriginUrl, merchantNotificationUrl,
                                                             merchantLandingPageUrl, environment);
                    return m_config;
                }
                else
                    return m_config;
            }
        }

        private static ApplicationConfig m_config;
    }
}
