function getUrlParameter(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)");
    var results = regex.exec(location.search);
    return results === null
        ? ""
        : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function VKPlatform() {
    this.viewerId = getUrlParameter("viewer_id");
    this.authKey = getUrlParameter("auth_key");
    this.initialApiResult = this.extractInitialApiResult();
    this.purchaseResolve = undefined;
    this.purchaseReject = undefined;

    VK.addCallback("onOrderSuccess", this.onOrderSuccess.bind(this));
    VK.addCallback("onOrderFail", this.onOrderFail.bind(this));
    VK.addCallback("onOrderCancel", this.onOrderCancel.bind(this));
}

/**
 * method=users.get&fields=photo_200,sex&form=JSON&v=5.103
 */
VKPlatform.prototype.extractInitialApiResult = function () {
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

VKPlatform.prototype.initialize = function (onSuccess, onFailure) {
    VK.init(onSuccess, onFailure, "5.103");
};

VKPlatform.prototype.getAuthFields = function () {
    return {
        apiType: 2,
        apiUid: this.viewerId,
        authSig: this.authKey,
        sessionKey: "-",
    };
};

VKPlatform.prototype.getInstallReferrer = function () {
    return "";
};

VKPlatform.prototype.getDefaultLocale = function () {
    return "ru";
};


VKPlatform.prototype.getUserFriends = function (onSuccess, onFailure) {
        VK.Api.call("friends.get",function(data) {onSuccess(data)}, function(data) { onFailure(data)});
};

VKPlatform.prototype.buyProduct = function (
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

    VK.callMethod("showOrderBox", {
        type: "item",
        item: productId,
    });
};

VKPlatform.prototype.loadUserProfile = function (onSuccess, onFailure) {
    if (this.initialApiResult && this.initialApiResult.length > 0) {
        var userData = this.initialApiResult[0];
        var avatar =
            userData["photo_200"] &&
            userData["photo_200"].indexOf("vk.com") >= 0
                ? ""
                : userData["photo_200"];

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

VKPlatform.prototype.onOrderSuccess = function (orderId) {
    this.purchaseResolve && this.purchaseResolve(orderId.toString());
    delete this.purchaseResolve;
    delete this.purchaseReject;
};

VKPlatform.prototype.onOrderFail = function (errorCode) {
    this.purchaseReject &&
        this.purchaseReject("Order failed (" + errorCode + ")");
    delete this.purchaseResolve;
    delete this.purchaseReject;
};

VKPlatform.prototype.onOrderCancel = function () {
    this.purchaseReject && this.purchaseReject("Order cancelled");
    delete this.purchaseResolve;
    delete this.purchaseReject;
};

window.SocialPlatformConstructor = VKPlatform;
