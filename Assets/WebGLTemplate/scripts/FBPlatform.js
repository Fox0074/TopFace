function getUrlParameter(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)");
    var results = regex.exec(location.search);
    return results === null
        ? ""
        : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function FBPlatform() {
    this.appId = getUrlParameter("app_id");
    this.installReferrer = getUrlParameter("gr_id");
    this.userId = "";
    this.sessionKey = "";
}

FBPlatform.prototype.initialize = function (onSuccess, onFailure) {
    if (!this.appId) {
        onFailure("AppId not found");
        return;
    }

    FB.init({
        appId: this.appId,
        autoLogAppEvents: true,
        xfbml: true,
        version: "v7.0",
    });

    var self = this;

    FB.getLoginStatus(function (response) {
        if (response.status === "connected") {
            self.userId = response.authResponse.userID;
            self.sessionKey = response.authResponse.signedRequest;
            onSuccess();
        } else {
            FB.login(function (response) {
                if (response.status === "connected") {
                    self.userId = response.authResponse.userID;
                    self.sessionKey = response.authResponse.signedRequest;
                    onSuccess();
                } else {
                    onFailure("User not logged in facebook");
                }
            });
        }
    });
};

FBPlatform.prototype.getAuthFields = function () {
    return {
        apiType: 3,
        apiUid: this.userId,
        authSig: "",
        sessionKey: this.sessionKey,
    };
};

FBPlatform.prototype.getInstallReferrer = function () {
    return this.installReferrer;
};

FBPlatform.prototype.getDefaultLocale = function () {
    return "en";
};

FBPlatform.prototype.buyProduct = function (
    _productId,
    _productTitle,
    _productDescription,
    _productPrice,
    _productImage,
    _onSuccess,
    _onFailure
) {};

FBPlatform.prototype.loadUserProfile = function (onSuccess, onFailure) {
    FB.api(
        "me",
        {
            fields: "picture,first_name,last_name",
        },
        function (response) {
            if (!response || response.error) {
                onFailure("Unable to load user profile");
            } else {
                onSuccess({
                    firstName: response["first_name"],
                    lastName: response["last_name"],
                    avatar:
                        response["picture"] &&
                        !response["picture"]["data"]["is_silhouette"]
                            ? response["picture"]["data"]["url"]
                            : "",
                    gender: 0,
                });
            }
        }
    );
};

window.SocialPlatformConstructor = FBPlatform;
