# Summary
This document introduces the file structure of SDK project, available classes and functions for invoking and the guidelines to tell users how to utilize the SDK step by step.

# Details
## Part1 - The file structure of SDK project:
* "TurnkeySourceCode" is for SDK library source code, you can open the solution using visual studio and build it to get a *.dll file. 
* "TurnkeySDKDemoAndUnitTest" is for some demos which shows all needed parameters and the excution result, and unit test represent the detail logic of each transaction and dependency.

## Part2 - User guideline of using SDK
The library provides integration access to the Turnkey Api.You can use it to quickly integrate with the Payments system and submit transactions, check their status and more.
### Before you Begin
Before using the Payments .NET SDK you should be familiar with the contents of the API specification documents as they describes all fields and their meaning within a given payment transaction.
### Setup your Project
Payments .NET SDK is delivered as .NET Class Library project.
It depends on a third party library: Newtonsoft.Json(version:12.0.0.0) and a xml config file which contains some urls such as "SessionTokenRequestUrl","PaymentOperationActionUrl","CashierUrl","CashierMobileUrl".
requirements: .NET Framework 4.5 (or newer)
### Possible Requests
Every payment operation has its own Call Object. To successfully perform any request one needs to create the object (configure it) and then call its execute() method.
* __GetAvailablePaymentSolutionsCall__ queries the list of the possible payment solutions (ie. credit card) (based on the country/currency)
* __TokenizeCall__ tokenizes the card for future use.
* __AuthCall__ requests authorisation for a payment.
* __CaptureCall__ performs a capture operation on an authorized payment.
* __VerifyCall__ performs a verify operation on an authorized payment.
* __VoidCall__ cancels a previously authenticated payment.
* __PurchaseCall__ does an authorize and capture operations at once (and cannot be voided).
* __PurchaseTokenCall__ performs a tokenize request operation to prepare a token for loading cashier page or cahier pachase usage(including mobile side).
* __RefundCall__ refunds a previous capture operation, partially or in full.
* __StatusCheckCall__ returns the status of an already issued payment transaction, as such it doesn’t actually generate a new transaction.
All classes are descendants of the _ApiCall_ class.
For more information on payment transactions please check the API specification documents.
Some of the possible request/call chains (ie. tokenize -> auth -> capture) can be seen in the unit test to.
### Typical Flow
  I. Prepare a configuration file for the SDK:
     Get "EnvParams.xml" file from SDK package, put it under the root dictionary of your project, check all the configured values in the file and make sure they are all accuracy.
  II. Access the ApplicationConfig object like this:
      string merchantID = Properties.Settings.Default.merchantId;
                string password = Properties.Settings.Default.password;
                string merchantNotificationUrl = Properties.Settings.Default.merchantNotificationUrl;
                string allowOriginUrl = Properties.Settings.Default.allowOriginUrl;
                string merchantLandingPageUrl = Properties.Settings.Default.merchantLandingPageUrl;
                string environment = Properties.Settings.Default.TurnkeySdkConfig;

                ApplicationConfig config = new ApplicationConfig(merchantID, password, allowOriginUrl, merchantNotificationUrl,
                                                                 merchantLandingPageUrl, environment);
      The applicationConfig object defines all environment parameters listed below:
	  * TurnkeyEnvironment include two values: "UAT","Production"
	  * SessionTokenRequestUrl
	  * PaymentOperationActionUrl
	  * CashierUrl
	  * CashierMobileUrl
	  * JavaScriptUrl
	  * AllowOriginUrl
	  * MerchantNotificationUrl
	  * MerchantLandingPageUrl
	  * MerchantId
	  * Password
	  "SessionTokenRequestUrl","PaymentOperationActionUrl","CashierUrl","CashierMobileUrl","JavaScriptUrl" are readonly.
  III. Create the a Call object:
      Dictionary<String, String> params = new Dictionary<String, String>();
      inputParams.Add("country", "FR");
      inputParams.Add("currency", "EUR");
      ApiCall call = new GetAvailablePaymentSolutionsCall(config, params);
      The call parameters have to supplied via a Dictionary.
      The constructor will do a simple "pre" validation on the params Dictionary. It will only check for the required keys (without an HTTP/API call).

  IV. Execute the call:
       Dictionary<String, String> result = call.execute();

  V. Watch for Exceptions
     Occasionally the SDK will not be able to perform your request and it will throw an Exception. This could be due to some unexpected conditions like no connectivity to the API. 

     try {
	     ApiCall call = new GetAvailablePaymentSolutionsCall(config, params);
     } catch (RequiredParamException e) {
	     //Log excetion information
	     //Show message to tell user what's wrong.
     }

     try {
	     Dictionary<String, String> result = call.execute();
     } catch (ActionCallException e) {
	     //Log excetion information
	     //Show message to tell user what's wrong.
### Payments Errors
Occasionally your payment processing API will not be able to successfully complete a request and it will return an error. Please check out the API documents to find out more about errors and what causes them to occur.


## Part3 - SDK demo and unit test usage
SDK demo and unit test is an individual project, you can open it with visual studio and IISExpress.

The demo reference SDK library(.dll file) invokes the api method to complete related transactions. It is designed as web application, you can input all required parameters and submit your request to get result.

Unit test present all transaction api invoking, prepare parameters, process the result, also you can get the knowlege of relationship among the transactions.


