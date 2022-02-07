using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turnkey.Parameter
{
    [Serializable]
    public class ParamAuthHostedPaymentToken : ParamBase
    {
        /* reqired fields */
        public readonly string merchantId = "merchantId"; 
        public readonly string password = "password";
        public readonly string action = "action";
        public readonly string timestamp = "timestamp";
        public readonly string allowOriginUrl = "allowOriginUrl";
        public readonly string channel = "channel";
        public readonly string currency = "currency";
        public readonly string country = "country";
        public readonly string merchantNotificationUrl = "merchantNotificationUrl";
        public readonly string amount = "amount";

        /* condition fields */
        public readonly string payerDocumentNumber = "payerDocumentNumber";
        public readonly string customerAddressPostalCode = "customerAddressPostalCode";
        public readonly string customerDocumentState = "customerDocumentState";
        public readonly string customerDocumentNumber = "customerDocumentNumber";
        public readonly string customerId = "customerId"; //
        public readonly string cardDescription = "cardDescription";

        /* optional fields */
        public readonly string selectedInstallmentsPlanId = "selectedInstallmentsPlanId";
        public readonly string freeText = "freeText";
        public readonly string limitMax = "limitMax";
        public readonly string limitMin = "limitMin";
        public readonly string storeCard = "storeCard";
        public readonly string bankMid = "bankMid";
        public readonly string specinProcessWithoutCvv2 = "specinProcessWithoutCvv2";
        public readonly string specinCreditCardToken = "specinCreditCardToken";
        public readonly string processUnknownSecurePayment = "processUnknownSecurePayment";
        public readonly string forceSecurePayment = "forceSecurePayment";
        public readonly string payerCustomerId = "payerCustomerId";
        public readonly string payerDocumentType = "payerDocumentType";
        public readonly string payerPhone = "payerPhone";
        public readonly string payerDateOfBirth = "payerDateOfBirth";
        public readonly string payerEmail = "payerEmail";
        public readonly string payerLastName = "payerLastName";
        public readonly string payerFirstName = "payerFirstName";
        public readonly string customerBillingAddressPhone = "customerBillingAddressPhone";
        public readonly string customerBillingAddressState = "customerBillingAddressState";
        public readonly string customerBillingAddressCountry = "customerBillingAddressCountry";
        public readonly string customerBillingAddressPostalCode = "customerBillingAddressPostalCode";
        public readonly string customerBillingAddressDistrict = "customerBillingAddressDistrict";
        public readonly string customerBillingAddressCity = "customerBillingAddressCity";
        public readonly string customerBillingAddressStreet = "customerBillingAddressStreet";
        public readonly string customerBillingAddressFlat = "customerBillingAddressFlat";
        public readonly string customerBillingAddressHouseNumber = "customerBillingAddressHouseNumber";
        public readonly string customerBillingAddressHouseName = "customerBillingAddressHouseName";
        public readonly string customerShippingAddressPhone = "customerShippingAddressPhone";
        public readonly string customerShippingAddressState = "customerShippingAddressState";
        public readonly string customerShippingAddressCountry = "customerShippingAddressCountry";
        public readonly string customerShippingAddressPostalCode = "customerShippingAddressPostalCode";
        public readonly string customerShippingAddressDistrict = "customerShippingAddressDistrict";
        public readonly string customerShippingAddressCity = "customerShippingAddressCity";
        public readonly string customerShippingAddressStreet = "customerShippingAddressStreet";
        public readonly string customerShippingAddressFlat = "customerShippingAddressFlat";
        public readonly string customerShippingAddressHouseNumber = "customerShippingAddressHouseNumber";
        public readonly string customerShippingAddressHouseName = "customerShippingAddressHouseName";
        public readonly string customerAddressPhone = "customerAddressPhone";
        public readonly string customerAddressState = "customerAddressState";
        public readonly string customerAddressCountry = "customerAddressCountry";
        public readonly string customerAddressDistrict = "customerAddressDistrict";
        public readonly string customerAddressCity = "customerAddressCity";
        public readonly string customerAddressStreet = "customerAddressStreet";
        public readonly string customerAddressFlat = "customerAddressFlat";
        public readonly string customerAddressHouseNumber = "customerAddressHouseNumber";
        public readonly string customerAddressHouseName = "customerAddressHouseName";
        public readonly string customerIPAddress = "customerIPAddress";
        public readonly string customerPhone = "customerPhone";
        public readonly string customerEmail = "customerEmail";
        public readonly string customerRegistrationDate = "customerRegistrationDate";
        public readonly string customerDateOfBirth = "customerDateOfBirth";
        public readonly string customerSex = "customerSex";//
        public readonly string customerLastName = "customerLastName";//
        public readonly string merchantReference = "merchantReference"; //
        public readonly string customerFirstName = "customerFirstName";//
        public readonly string customerDocumentType = "customerDocumentType";
        public readonly string merchantLandingPageUrl = "merchantLandingPageUrl";
        public readonly string language = "language";
        public readonly string paymentSolutionId = "paymentSolutionId";
        public readonly string discountAmount = "discountAmount";
        public readonly string chargeAmount = "chargeAmount";
        public readonly string shippingAmount = "shippingAmount";
        public readonly string taxAmount = "taxAmount";
        public readonly string userAgent = "userAgent";  //
        public readonly string userDevice = "userDevice";  //
        public readonly string brandId = "brandId";  //
        public readonly string operatorId = "operatorId";  //
        public readonly string merchantTxId = "merchantTxId";   //
        public readonly string firstTimeTransaction = "firstTimeTransaction"; //
        public readonly string quickSale = "quickSale";  //
        public readonly string s_text1 = "s_text1";
        public readonly string s_text2 = "s_text2";
        public readonly string s_text3 = "s_text3";
        public readonly string s_text4 = "s_text4";
        public readonly string s_text5 = "s_text5";
        public readonly string d_date1 = "d_date1";
        public readonly string d_date2 = "d_date2";
        public readonly string d_date3 = "d_date3";
        public readonly string d_date4 = "d_date4";
        public readonly string d_date5 = "d_date5";
        public readonly string b_bool1 = "b_bool1";
        public readonly string b_bool2 = "b_bool2";
        public readonly string b_bool3 = "b_bool3";
        public readonly string b_bool4 = "b_bool4";
        public readonly string b_bool5 = "b_bool5";
        public readonly string n_num1 = "n_num1";
        public readonly string n_num2 = "n_num2";
        public readonly string n_num3 = "n_num3";
        public readonly string n_num4 = "n_num4";
        public readonly string n_num5 = "n_num5";
    }

    [Serializable]
    public class ParamAuthHostedPaymentAction : ParamBase
    {
        /* condition fields */
        public readonly string containerId = "containerId";
        public readonly string successCallback = "successCallback";
        public readonly string failureCallback = "failureCallback";
        public readonly string cancelCallback = "cancelCallback";
        public readonly string integrationMode = "integrationMode";

        /* optional fields */
        public readonly string bannerUrl = "bannerUrl";
    }
}