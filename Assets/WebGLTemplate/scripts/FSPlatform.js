var fsclient;

function getUrlParameter(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)");
    var results = regex.exec(location.search);
    return results === null
        ? ""
        : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function includeScript(url, onSuccess, onError) {
    var script = document.createElement("script");
    script.onload = onSuccess;
    script.onerror = onError;
    script.src = url;
    document.head.appendChild(script);
}

function FSPlatform() {
    this.viewerId = getUrlParameter("viewerId");
    this.authKey = getUrlParameter("authKey");
    this.initialApiResult = this.extractInitialApiResult();
    this.purchaseResolve = undefined;
    this.purchaseReject = undefined;

    //fsapi.addCallback("onOrderSuccess", this.onOrderSuccess.bind(this));
    //fsapi.addCallback("onOrderFail", this.onOrderFail.bind(this));
    //fsapi.addCallback("onOrderCancel", this.onOrderCancel.bind(this));
}

/**
 * method=users.get&fields=photo_200,sex&form=JSON&v=5.103
 */
FSPlatform.prototype.extractInitialApiResult = function () {
    var apiResult = getUrlParameter("api_result");

    if (!apiResult) {
        console.log("Unable to get initial api result");
        return;
    }

    var data = JSON.parse(apiResult);
    if (data.response) {
        return data.response;
    } else {
        console.log("Unable to parse initial api result");
    }
};

FSPlatform.prototype.initialize = function (onSuccess, onFailure) {
    var fsapiUrl = getUrlParameter("fsapi");
    var appid = getUrlParameter("apiId");
    var clientKey = getUrlParameter("client_key");

    includeScript(
        fsapiUrl,
        function () {
            fsclient = new fsapi(appid, clientKey);
            fsclient.init(function () {
                console.log("Unable to initialize fsapi");
            });
            onSuccess();
        },

        function () {
            onFailure("Unable to load fsapi script");
        }
    );
};

FSPlatform.prototype.getAuthFields = function () {
    return {
        apiType: 7,
        apiUid: this.viewerId,
        authSig: this.authKey,
        sessionKey: "-",
    };
};

FSPlatform.prototype.getInstallReferrer = function () {
    return "";
};

FSPlatform.prototype.getDefaultLocale = function () {
    return "ru";
};

FSPlatform.prototype.buyProduct = function (
    productId,
    _productTitle,
    _productDescription,
    _productPrice,
    _productImage,
    onSuccess,
    onFailure
) {
    this.purchaseResolve = onSuccess;
    this.purchaseReject = onFailure;

    fsclient.event("buyItem", function() { console.log("BuyEventCallBack")},
    {
        name: _productTitle,
        itemId: productId,
        priceFmCents: _productPrice,
        amount: 1,
        callBack: function() { console.log("BuyItemCallBack")}
    });
};

FSPlatform.prototype.loadUserProfile = function (onSuccess, onFailure) {
    if (this.initialApiResult) {
        var userData = this.initialApiResult[this.viewerId];
        var avatar =
            userData["photo_medium"] &&
            userData["photo_medium"].indexOf("fotocdn.net") >= 0
                ? ""
                : userData["photo_medium"];

        onSuccess({
            firstName: userData["first_name"],
            lastName: userData["last_name"],
            avatar: avatar,
            gender: userData["sex"] === 1 ? 1 : 0,
        });
    } else {
        onFailure("Initial API result not found");
    }
};

FSPlatform.prototype.onOrderSuccess = function (orderId) {
    this.purchaseResolve && this.purchaseResolve(orderId.toString());
    delete this.purchaseResolve;
    delete this.purchaseReject;
};

FSPlatform.prototype.onOrderFail = function (errorCode) {
    this.purchaseReject &&
        this.purchaseReject("Order failed (" + errorCode + ")");
    delete this.purchaseResolve;
    delete this.purchaseReject;
};

FSPlatform.prototype.onOrderCancel = function () {
    this.purchaseReject && this.purchaseReject("Order cancelled");
    delete this.purchaseResolve;
    delete this.purchaseReject;
};

window.SocialPlatformConstructor = FSPlatform;
