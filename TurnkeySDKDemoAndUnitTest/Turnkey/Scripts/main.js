//Tools:

var links = [
    "getAvailablePaymentSolutions",
    "tokenize",
    "purchase",
    "auth",
    "verify",
    "capture",
    "void",
    "refund",
    "statuscheck",
    "redirect",
    "redirect2",
    "jsapi",
    "mobileredirect"
];

var dataToGet = {};

function redirectToSite(url) {
    window.open(url, '_blank');
}


function clear() {
    for (var i = 0; i < links.length; i++) {
        document.getElementById(links[i]).style.display = "none";
    }
}

function showContent(divId) {
    clear();
    document.getElementById(divId).style.display = "block";
}




//Post:

function sendRecivePost(dataToSend, urlToSend, successFunction) {
    $('#loading').modal('show');
    $.ajax({
        url: urlToSend,
        type: "post",
        data: dataToSend,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (rd) {
            console.log(JSON.stringify(rd));
            if (successFunction == "Tokenize") {
                document.getElementById("customerIdToPurchase").value = rd.customerId;
                document.getElementById("specinCreditCardTokenToPurchase").value = rd.cardToken;
                document.getElementById("customerIdToAuth").value = rd.customerId;
                document.getElementById("specinCreditCardTokenToAuth").value = rd.cardToken;
                document.getElementById("customerIdToPurchase").style.color = "#00c91e";
                document.getElementById("specinCreditCardTokenToPurchase").style.color = "#00c91e";
                document.getElementById("customerIdToAuth").style.color = "#00c91e";
                document.getElementById("specinCreditCardTokenToAuth").style.color = "#00c91e";
                document.getElementById("customerIdToVERIFY").value = rd.customerId;
                document.getElementById("specinCreditCardTokenToVERIFY").value = rd.cardToken;
                document.getElementById("customerIdToVERIFY").style.color = "#00c91e";
                document.getElementById("specinCreditCardTokenToVERIFY").style.color = "#00c91e";
            }
            if (successFunction == "Purchase") {
                document.getElementById("txIdToStatusCheck").value = rd.txId;
                document.getElementById("merchantTxIdToStatusCheck").value = rd.merchantTxId;
                document.getElementById("txIdToStatusCheck").style.color = "#00c91e";
                document.getElementById("merchantTxIdToStatusCheck").style.color = "#00c91e";

                document.getElementById("originalMerchantTxIdToRefound").value = rd.merchantTxId;
                document.getElementById("originalMerchantTxIdToRefound").style.color = "#00c91e";
            }
            if (successFunction == "Auth") {
                document.getElementById("originalMerchantTxIdToCapture").value = rd.merchantTxId;
                document.getElementById("originalMerchantTxIdToCapture").style.color = "#00c91e";
                document.getElementById("originalMerchantTxIdToVoid").value = rd.merchantTxId;
                document.getElementById("originalMerchantTxIdToVoid").style.color = "#00c91e";

                document.getElementById("merchantTxIdToStatusCheck").value = rd.merchantTxId;
                document.getElementById("merchantTxIdToStatusCheck").style.color = "#00c91e";
                document.getElementById("txIdToStatusCheck").value = rd.txId;
                document.getElementById("txIdToStatusCheck").style.color = "#00c91e";
            }
            if (successFunction == "Capture") {
                document.getElementById("originalMerchantTxIdToVoid").value = rd.originalMerchantTxId;
                document.getElementById("originalMerchantTxIdToVoid").style.color = "#00c91e";
                document.getElementById("originalMerchantTxIdToRefound").value = rd.originalMerchantTxId;
                document.getElementById("originalMerchantTxIdToRefound").style.color = "#00c91e";
            }
            if (successFunction == "Void" || successFunction == "Capture") {
                document.getElementById("originalMerchantTxIdToRefound").value = rd.originalMerchantTxId;
                document.getElementById("originalMerchantTxIdToRefound").style.color = "#00c91e";
            }
            if (successFunction == "PurchaseRedirect") {
                document.getElementById("modal-footer").innerHTML = '<button type="button" class="btn btn-primary" onclick="redirectToSite(\'' + rd.url.replace("\"", "").trim() + '\')">Redirect</button> <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>';
            }
            else {
                document.getElementById("modal-footer").innerHTML = '<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>';
            }
            if (successFunction == "PurchaseToken") {
                alert(JSON.stringify(rd));
                $.ajax({
                    crossOrigin: true,
                    url: "https://cashier-turnkeyuat.test.myriadpayments.com/ui/cashier",
                    type: "get",
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    data: rd,
                    success: function (rd) { alert(JSON.stringify(rd)); },
                    error: function (rd) { alert("error: " + JSON.stringify(rd)) }
                });
            }
            $('#loading').modal('hide');
            var placeOfResult = document.getElementById("placeOfResult");
            placeOfResult.innerHTML = "<pre>" + JSON.stringify(rd) + "</pre>";
            $('#responseDataModal').modal('show');
        },
        error: function (rd) { alert(JSON.stringify(rd)); }
    });
}

function invokeGetAvailablePaymentSolutions() {
    
    var country = document.getElementById("country").value.trim();
    var currency = document.getElementById("currency").value.trim();
    var data = { "country": country, "currency": currency };
    sendRecivePost(data, "api/GetAvailablePaymentSolutions", "getAvailablePaymentSolutions");
}

function invokeTokenize() {
    var number = document.getElementById("numberToTokenize").value.trim();
    var nameOnCard = document.getElementById("nameOnCardToTokenize").value.trim();
    var expiryMonth = document.getElementById("expiryMonthToTokenize").value.trim();
    var expiryYear = document.getElementById("expiryYearToTokenize").value.trim();
    var data = { "number": number, "nameOnCard": nameOnCard, "expiryMonth": expiryMonth, "expiryYear": expiryYear };
    sendRecivePost(data, "api/Tokenize", "Tokenize");

}

function invokePurchase() {
    var amount = document.getElementById("amountToPurchase").value.trim();
    var channel = document.getElementById("channelToPurchase").value.trim();
    var country = document.getElementById("countryToPurchase").value.trim();
    var currency = document.getElementById("currencyToPurchase").value.trim();
    var paymentSolutionId = document.getElementById("paymentSolutionIdToPurchase").value.trim();
    var customerId = document.getElementById("customerIdToPurchase").value.trim();
    var specinCreditCardToken = document.getElementById("specinCreditCardTokenToPurchase").value.trim();
    var specinCreditCardCVV = document.getElementById("specinCreditCardCVVToPurchase").value.trim();
    var merchantNotificationUrl = document.getElementById("merchantNotificationUrl").value.trim();
    var data = { "amount": amount, "channel": channel, "country": country, "currency": currency, "paymentSolutionId": paymentSolutionId, "customerId": customerId, "specinCreditCardToken": specinCreditCardToken, "specinCreditCardCVV": specinCreditCardCVV, "merchantNotificationUrl": merchantNotificationUrl };
    sendRecivePost(data, "api/Purchase", "Purchase");
}

function invokeAuth() {
    var amount = document.getElementById("amountToAuth").value.trim();
    var channel = document.getElementById("channelToAuth").value.trim();
    var country = document.getElementById("countryToAuth").value.trim();
    var currency = document.getElementById("currencyToAuth").value.trim();
    var paymentSolutionId = document.getElementById("paymentSolutionIdToAuth").value.trim();
    var customerId = document.getElementById("customerIdToAuth").value.trim();
    var specinCreditCardToken = document.getElementById("specinCreditCardTokenToAuth").value.trim();
    var specinCreditCardCVV = document.getElementById("specinCreditCardCVVToAuth").value.trim();
    var merchantNotificationUrl = document.getElementById("merchantNotificationUrl").value.trim();
    var data = { "amount": amount, "channel": channel, "country": country, "currency": currency, "paymentSolutionId": paymentSolutionId, "customerId": customerId, "specinCreditCardToken": specinCreditCardToken, "specinCreditCardCVV": specinCreditCardCVV, "merchantNotificationUrl": merchantNotificationUrl };
    sendRecivePost(data, "api/Auth", "Auth");

}

function invokeVerify() {
    var amount = document.getElementById("amountToVERIFY").value.trim();
    var channel = document.getElementById("channelToVERIFY").value.trim();
    var country = document.getElementById("countryToVERIFY").value.trim();
    var currency = document.getElementById("currencyToVERIFY").value.trim();
    var paymentSolutionId = document.getElementById("paymentSolutionIdToVERIFY").value.trim();
    var customerId = document.getElementById("customerIdToVERIFY").value.trim();
    var specinCreditCardToken = document.getElementById("specinCreditCardTokenToVERIFY").value.trim();
    var specinCreditCardCVV = document.getElementById("specinCreditCardCVVToVERIFY").value.trim();
    var merchantNotificationUrl = document.getElementById("merchantNotificationUrl").value.trim();
    var data = { "amount": amount, "channel": channel, "country": country, "currency": currency, "paymentSolutionId": paymentSolutionId, "customerId": customerId, "specinCreditCardToken": specinCreditCardToken, "specinCreditCardCVV": specinCreditCardCVV, "merchantNotificationUrl": merchantNotificationUrl };
    sendRecivePost(data, "api/Verify", "");

}

function invokeCapture() {
    var originalMerchantTxId = document.getElementById("originalMerchantTxIdToCapture").value.trim();
    var amount = document.getElementById("amountToCapture").value.trim();
    var data = { "amount": amount, "originalMerchantTxId": originalMerchantTxId};
    sendRecivePost(data, "api/Capture", "Capture");
}

function invokeVoid() {
    var originalMerchantTxId = document.getElementById("originalMerchantTxIdToVoid").value.trim();
    var data = { "originalMerchantTxId": originalMerchantTxId};
    sendRecivePost(data, "api/Void", "Void");
}

function invokeRefund() {
    var amount = document.getElementById("amountToRefound").value.trim();
    var originalMerchantTxId = document.getElementById("originalMerchantTxIdToRefound").value.trim();
    var data = { "originalMerchantTxId": originalMerchantTxId,"amount": amount};
    sendRecivePost(data, "api/Refund", "Refund");

}

function invokeStatusCheck() {
    var txId = document.getElementById("txIdToStatusCheck").value.trim();
    var merchantTxId = document.getElementById("merchantTxIdToStatusCheck").value.trim();
    var data = { "txId": txId, "merchantTxId": merchantTxId };
    
    sendRecivePost(data, "api/StatusCheck", "StatusCheck");

}



function invokeRedirect() {
    var merchantNotificationUrl = document.getElementById("merchantNotificationUrlToRedirect").value.trim();
    var country = document.getElementById("countryToRedirect").value.trim();
    var currency = document.getElementById("currencyToRedirect").value.trim();
    var amount = document.getElementById("amountToRedirect").value.trim();
    var channel = document.getElementById("channelToRedirect").value.trim();
    var paymentSolutionId = document.getElementById("paymentSolutionIdToRedirect").value.trim();
    var integrationMode = document.getElementById("integrationModeToRedirect").value.trim();
    var data = { "merchantNotificationUrl": merchantNotificationUrl,"amount": amount, "channel": channel, "country": country, "currency": currency, "paymentSolutionId": paymentSolutionId, "integrationMode": integrationMode};
    sendRecivePost(data, "api/PurchaseRedirect", "PurchaseRedirect");

}

function invokeRedirectForMobile() {
    var merchantNotificationUrl = document.getElementById("merchantNotificationUrlToRedirect").value.trim();
    var country = document.getElementById("countryToRedirect").value.trim();
    var currency = document.getElementById("currencyToRedirect").value.trim();
    var amount = document.getElementById("amountToRedirect").value.trim();
    var channel = document.getElementById("channelToRedirect").value.trim();
    var paymentSolutionId = document.getElementById("paymentSolutionIdToRedirect").value.trim();
    var integrationMode = document.getElementById("integrationModeToRedirect").value.trim();
    var data = { "merchantNotificationUrl": merchantNotificationUrl, "amount": amount, "channel": channel, "country": country, "currency": currency, "paymentSolutionId": paymentSolutionId, "integrationMode": integrationMode };
    sendRecivePost(data, "api/PurchaseRedirectForMobile", "PurchaseRedirect");

}

function invokeRedirect2() {
    var merchantNotificationUrl = document.getElementById("merchantNotificationUrlToRedirect2").value.trim();
    var country = document.getElementById("countryToRedirect2").value.trim();
    var currency = document.getElementById("currencyToRedirect2").value.trim();
    var amount = document.getElementById("amountToRedirect2").value.trim();
    var channel = document.getElementById("channelToRedirect2").value.trim();
    var paymentSolutionId = document.getElementById("paymentSolutionIdToRedirect2").value.trim();
    var integrationMode = document.getElementById("integrationModeToRedirect2").value.trim();
    var data = { "merchantNotificationUrl": merchantNotificationUrl, "amount": amount, "channel": channel, "country": country, "currency": currency, "paymentSolutionId": paymentSolutionId, "integrationMode": integrationMode};
    this.dataToGet = data;
    sendRecivePost(data, "api/PurchaseToken", "PurchaseToken");
}


function pay() {

    // get token
    // note: jQuery is not required for Turnkey JS (only used for the token request etc.)!

    var merchantId = "";
    var token = "";
    var merchantNotificationUrl = document.getElementById("merchantNotificationUrlToJsaApi").value.trim();
    var paymentSolutionId = document.getElementById("paymentSolutionIdToJsaApi").value.trim();
    var channel = document.getElementById("channelToJsaApi").value.trim();
    var amount = document.getElementById("amountToJsaApi").value.trim();
    var currency = document.getElementById("currencyToJsaApi").value.trim();
    var country = document.getElementById("countryToJsaApi").value.trim();
    var customerId = document.getElementById("customerIdToJsaApi").value.trim();
    var data = { "merchantNotificationUrl": merchantNotificationUrl, "amount": amount, "channel": channel, "country": country, "currency": currency, "paymentSolutionId": paymentSolutionId, "customerId": customerId };

    $.ajax({
        url: "api/PurchaseToken",
        type: "post",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: data,
        success: function (rd) {
            $('#pay').modal('show');
            var cashier = com.myriadpayments.api.cashier();
            cashier.init({
                baseUrl: rd.cashierUrl
            });
            cashier.show({
                containerId: "ipgCashierDiv",
                merchantId: rd.merchantId,
                token: rd.token,
                successCallback: JSON.stringify(rd),
                failureCallback: JSON.stringify(rd),
                cancelCallback: JSON.stringify(rd)
            });
            
        },
        error: function (rd) { alert("error: " + JSON.stringify(rd)); }
    });

};