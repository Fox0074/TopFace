function createUUID4() {
    var uuid = "",
        ii;
    for (ii = 0; ii < 32; ii += 1) {
        switch (ii) {
            case 8:
            case 20:
                uuid += "-";
                uuid += ((Math.random() * 16) | 0).toString(16);
                break;

            case 12:
                uuid += "-";
                uuid += "4";
                break;

            case 16:
                uuid += "-";
                uuid += ((Math.random() * 4) | 8).toString(16);
                break;

            default:
                uuid += ((Math.random() * 16) | 0).toString(16);
        }
    }
    return uuid;
}

function OKPlatform() {
    this.params = FAPI.Util.getRequestParameters();
    this.loggedUserId = this.params["logged_user_id"];
    this.auhtSig = this.params["auth_sig"];
    this.sessionKey = this.params["session_key"];
    this.apiServer = this.params["api_server"];
    this.apiConnection = this.params["apiconnection"];
    this.purchaseTransactionId = "";
    this.purchaseResolve = undefined;
    this.purchaseReject = undefined;

    window.API_callback = this.handleAPICallback.bind(this);
}

OKPlatform.prototype.handleAPICallback = function (method, result, data) {
    if (method === "showPayment") {
        console && console.log(method, result, data);

        if (result === "ok") {
            this.purchaseResolve &&
                this.purchaseResolve(this.purchaseTransactionId);
        } else {
            this.purchaseReject && this.purchaseReject(data || result);
        }

        delete this.purchaseTransactionId;
        delete this.purchaseResolve;
        delete this.purchaseReject;
    }
};

OKPlatform.prototype.initialize = function (onSuccess, onFailure) {
    FAPI.init(this.apiServer, this.apiConnection, onSuccess, onFailure);
};

OKPlatform.prototype.getAuthFields = function () {
    return {
        apiType: 1,
        apiUid: this.loggedUserId,
        authSig: this.auhtSig,
        sessionKey: this.sessionKey,
    };
};

OKPlatform.prototype.getInstallReferrer = function () {
    return "";
};

OKPlatform.prototype.getDefaultLocale = function () {
    return "ru";
};

OKPlatform.prototype.buyProduct = function (
    productId,
    productTitle,
    productDescription,
    productPrice,
    _productImage,
    onSuccess,
    onFailure
) {
    this.purchaseTransactionId = createUUID4();
    this.purchaseResolve = onSuccess;
    this.purchaseReject = onFailure;

    FAPI.UI.showPayment(
        productTitle,
        productDescription,
        productId,
        productPrice,
        null,
        JSON.stringify({ tid: this.purchaseTransactionId }),
        "ok",
        true
    );
};

OKPlatform.prototype.getUserFriends = function (onSuccess, onFailure) {
    onFailure("Error");
};

OKPlatform.prototype.loadUserProfile = function (onSuccess, onFailure) {
    FAPI.Client.call(
        {
            method: "users.getCurrentUser",
            fields: "FIRST_NAME,LAST_NAME,GENDER,PIC128X128",
        },
        function (status, data, error) {
            if (status == "ok") {
                var avatar =
                    data.pic128x128 && data.pic128x128.indexOf("api.ok.ru") >= 0
                        ? ""
                        : data.pic128x128;

                onSuccess({
                    firstName: data.first_name,
                    lastName: data.last_name,
                    gender: data.gender,
                    avatar: avatar,
                });
            } else {
                onFailure(error.error_msg);
            }
        }
    );
};

window.SocialPlatformConstructor = OKPlatform;
