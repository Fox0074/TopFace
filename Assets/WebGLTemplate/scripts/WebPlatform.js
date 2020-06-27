function WebPlatform() {}

WebPlatform.prototype.initialize = function (onSuccess, _onFailure) {
    onSuccess();
};

WebPlatform.prototype.getAuthFields = function () {
    var params = new URLSearchParams(window.location.search);

    return {
        apiType: 0,
        apiUid: params.get("userId") || "1",
        authSig: "-",
        sessionKey: "-",
    };
};

WebPlatform.prototype.getInstallReferrer = function () {
    return "";
};

WebPlatform.prototype.getDefaultLocale = function () {
    return "en";
};

WebPlatform.prototype.buyProduct = function (
    productId,
    _productTitle,
    _productDescription,
    _productPrice,
    _productImage,
    onSuccess,
    onFailure
) {
    if (
        confirm("Вы действительно хотите приобрести продукт " + productId + "?")
    ) {
        onSuccess(
            productId,
            Math.floor(Math.random() * Number.MAX_SAFE_INTEGER).toString()
        );
    } else {
        onFailure("Purchase cancelled");
    }
};

WebPlatform.prototype.loadUserProfile = function (onSuccess, _onFailure) {
    onSuccess({
        firstName: "User",
        lastName: "Happy",
        avatar: "",
        gender: 0,
    });
};

window.SocialPlatformConstructor = WebPlatform;
