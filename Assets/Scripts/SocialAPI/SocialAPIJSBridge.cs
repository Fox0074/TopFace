using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

namespace HappyGames.SocialAPI
{
    public class SocialAPIJSBridge : MonoBehaviour
    {
        public SocialAPIManager Manager { get; set; }

#if UNITY_WEBGL && !UNITY_EDITOR
        public void Initialize()
        {
            initializeSocialAPI("OnSocialAPIInitialized", "OnSocialAPIInitializationFailed");
        }

        public SocialAuthFields GetAuthFields()
        {
            try
            {
                var json = getSocialAuthFields();
                return JsonConvert.DeserializeObject<SocialAuthFields>(json, SocialJSBridgeJsonConverter.Settings);
                return null;
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Unable to get and parse social auth data: {0}", e);
                return null;
            }
        }

        public string GetInstallReferrer()
        {
            return getSocialInstallReferrer();
        }

        public string GetDefaultLocale()
        {
            return getSocialDefaultLocale();
        }

        public void BuyProduct(string productId, SocialPurchaseData purchaseData)
        {
            buySocialProduct(
                productId,
                purchaseData.ProductTitle,
                purchaseData.ProductDescription,
                Decimal.ToInt32(purchaseData.ProductPrice * 100),
                purchaseData.ProductImage,
                "OnProductPurchased",
                "OnProductPurchaseFailed"
            );
        }

        public void LoadUserProfile()
        {
            loadSocialUserProfile("OnUserProfileLoaded", "OnUserProfileLoadingFailed");
        }

        public void GetUserFriends()
        {
            getUserFriends("OnUserFriendsLoaded", "OnUserFriendsLoadedFailed");
        }

        internal void OnUserFriendsLoaded(string profileData)
        {
            Manager.OnUserFriendsLoading(profileData);
        }

        internal void OnUserFriendsLoadedFailed(string profileData)
        {
            Manager.OnUserFriendsLoadingFailed(profileData);
        }


        [DllImport("__Internal")]
        private static extern void initializeSocialAPI(string onSuccess, string onFailure);

        [DllImport("__Internal")]
        private static extern string getSocialAuthFields();

        [DllImport("__Internal")]
        private static extern string getSocialInstallReferrer();

        [DllImport("__Internal")]
        private static extern string getSocialDefaultLocale();

        [DllImport("__Internal")]
        private static extern void buySocialProduct(string productId, string productTitle,
            string productDescription, int productPrice, string productImage, string onSuccess, string onFailure);

        [DllImport("__Internal")]
        private static extern void loadSocialUserProfile(string onSuccess, string onFailure);

        [DllImport("__Internal")]
        private static extern void getUserFriends(string onSuccess, string onFailure);
#else
        public void Initialize()
        {
            Manager.OnSocialAPIInitialized();
        }

        public SocialAuthFields GetAuthFields()
        {
            return new SocialAuthFields
            {
                ApiType = 0,
                ApiUid = "1",
                SessionKey = "-",
                AuthSig = "-"
            };
        }

        public string GetInstallReferrer()
        {
            return "";
        }

        public string GetDefaultLocale()
        {
            return "en";
        }

        public void BuyProduct(string productId, SocialPurchaseData purchaseData)
        {
            Manager.OnProductPurchaseFailed(productId, "Not support");
        }

        public void GetUserFriends()
        {
            Manager.OnUserFriendsLoadingFailed("Error");
        }

        public void LoadUserProfile()
        {
            Manager.OnUserProfileLoaded(new SocialProfile
            {
                FirstName = "User",
                LastName = "Happy",
                Avatar = "",
                Gender = SocialGender.Male
            });
        }
#endif

        internal void OnSocialAPIInitialized()
        {
            Manager.OnSocialAPIInitialized();
        }

        internal void OnSocialAPIInitializationFailed(string error)
        {
            Manager.OnSocialAPIInitializationFailed(error);
        }

        internal void OnProductPurchased(string data)
        {
            try
            {
                //var obj = JObject.Parse(data);
                //Manager.OnProductPurchased((string)obj["productId"], (string)obj["transactionId"]);
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Unable to parse purchase result object: {0}", e);
            }
        }

        internal void OnProductPurchaseFailed(string data)
        {
            try
            {
                //var obj = JObject.Parse(data);
                //Manager.OnProductPurchaseFailed((string)obj["productId"], (string)obj["error"]);
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Unable to parse purchase result object: {0}", e);
            }
        }

        internal void OnUserProfileLoaded(string profileData)
        {
            try
            {
                var profile = JsonConvert.DeserializeObject<SocialProfile>(profileData, SocialJSBridgeJsonConverter.Settings);
                Manager.OnUserProfileLoaded(profile);
            }
            catch
            {
                OnUserProfileLoadingFailed("Unable to parse social profile data");
            }
        }

        internal void OnUserProfileLoadingFailed(string error)
        {
            Manager.OnUserProfileLoadingFailed(error);
        }
    }
}
