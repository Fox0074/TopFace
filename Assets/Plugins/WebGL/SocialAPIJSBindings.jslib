var SocialAPILib = {
    $SocialAPI: {
        createPlatformInstance: function () {
            if (!this.platform) {
                if (!window.SocialPlatformConstructor) {
                    throw new Error(
                        "Unable to resolve SocialPlatformConstructor"
                    );
                }

                this.platform = new window.SocialPlatformConstructor();
            }
        },

        getDefaultLocale: function () {
            SocialAPI.createPlatformInstance();
            return this.platform.getDefaultLocale();
        },

        initialize: function (successMethodName, failureMethodName) {
            SocialAPI.createPlatformInstance();
            this.platform.initialize(
                function () {
                    SocialAPI.sendMessage(successMethodName);
                },
                function (error) {
                    SocialAPI.sendMessage(failureMethodName, String(error));
                }
            );
        },

        getAuthFields: function () {
            return JSON.stringify(this.platform.getAuthFields());
        },

        getInstallReferrer: function () {
            return this.platform.getInstallReferrer();
        },

        buyProduct: function (
            productId,
            productTitle,
            productDescription,
            productPrice,
            productImage,
            successMethodName,
            failureMethodName
        ) {
            this.platform.buyProduct(
                productId,
                productTitle,
                productDescription,
                productPrice,
                productImage,
                function (transactionId) {
                    SocialAPI.sendMessage(
                        successMethodName,
                        JSON.stringify({
                            productId: productId,
                            transactionId: transactionId,
                        })
                    );
                },
                function (error) {
                    SocialAPI.sendMessage(
                        failureMethodName,
                        JSON.stringify({
                            productId: productId,
                            error: error,
                        })
                    );
                }
            );
        },

        loadUserProfile: function (successMethodName, failureMethodName) {
            this.platform.loadUserProfile(
                function (profile) {
                    SocialAPI.sendMessage(
                        successMethodName,
                        JSON.stringify(profile)
                    );
                },
                function (error) {
                    SocialAPI.sendMessage(failureMethodName, String(error));
                }
            );
        },

        getUserFriends: function (successMethodName, failureMethodName) {
            this.platform.getUserFriends(
                function (profile) {
                    SocialAPI.sendMessage(
                        successMethodName,
                        JSON.stringify(profile)
                    );
                },
                function (error) {
                    SocialAPI.sendMessage(failureMethodName, String(error));
                }
            );
        },

        sendMessage: function (method, value) {
            if (typeof value === "undefined") {
                SendMessage("SocialAPIJSBridge", method);
            } else {
                SendMessage("SocialAPIJSBridge", method, value);
            }
        },
    },

    getSocialDefaultLocale: function () {
        var data = SocialAPI.getDefaultLocale();
        var bufferSize = lengthBytesUTF8(data) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(data, buffer, bufferSize);
        return buffer;
    },

    initializeSocialAPI: function (successMethodName, failureMethodName) {
        SocialAPI.initialize(
            Pointer_stringify(successMethodName),
            Pointer_stringify(failureMethodName)
        );
    },

    getSocialAuthFields: function () {
        var data = SocialAPI.getAuthFields();
        var bufferSize = lengthBytesUTF8(data) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(data, buffer, bufferSize);
        return buffer;
    },

    getSocialInstallReferrer: function () {
        var data = SocialAPI.getInstallReferrer();
        var bufferSize = lengthBytesUTF8(data) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(data, buffer, bufferSize);
        return buffer;
    },

    buySocialProduct: function (
        productId,
        productTitle,
        productDescription,
        productPrice,
        productImage,
        successMethodName,
        failureMethodName
    ) {
        SocialAPI.buyProduct(
            Pointer_stringify(productId),
            Pointer_stringify(productTitle),
            Pointer_stringify(productDescription),
            productPrice / 100,
            Pointer_stringify(productImage),
            Pointer_stringify(successMethodName),
            Pointer_stringify(failureMethodName)
        );
    },

    loadSocialUserProfile: function (successMethodName, failureMethodName) {
        SocialAPI.loadUserProfile(
            Pointer_stringify(successMethodName),
            Pointer_stringify(failureMethodName)
        );
    },

    getUserFriends: function (successMethodName, failureMethodName) {
        SocialAPI.getUserFriends(
            Pointer_stringify(successMethodName),
            Pointer_stringify(failureMethodName)
        );
    },
};

autoAddDeps(LibraryManager.library, "$SocialAPI");
mergeInto(LibraryManager.library, SocialAPILib);
