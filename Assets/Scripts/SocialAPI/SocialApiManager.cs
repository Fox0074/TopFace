using System;

namespace HappyGames.SocialAPI
{
    public class SocialAPIManager
    {
        public event Action SocialAPIInitialized = delegate { };
        public event Action<string> SocialAPIInitializationFailed = delegate { };

        public event Action<string, string> ProductPurchased = delegate { };
        public event Action<string, string> ProductPurchaseFailed = delegate { };

        public event Action<SocialProfile> UserProfileLoaded = delegate { };
        public event Action<string> UserProfileLoadingFailed = delegate { };

        public bool Initialized { get; private set; }
        public int ApiType { get; private set; }
        public string ApiUid { get; private set; }
        public string AuthSig { get; private set; }
        public string SessionKey { get; private set; }
        public string InstallReferrer { get; private set; }

        private SocialAPIJSBridge _bridge;

        public SocialAPIManager(SocialAPIJSBridge bridge)
        {
            _bridge = bridge;
            _bridge.Manager = this;
        }

        public void Initialize()
        {
            _bridge.Initialize();
        }

        public string GetDefaultLocale()
        {
            return _bridge.GetDefaultLocale();
        }

        public void BuyProduct(string productId, SocialPurchaseData purchaseData)
        {
            _bridge.BuyProduct(productId, purchaseData);
        }

        public void LoadUserProfile()
        {
            _bridge.LoadUserProfile();
        }

        internal void OnSocialAPIInitialized()
        {
            var authFields = _bridge.GetAuthFields();
            if (authFields != null)
            {
                ApiType = authFields.ApiType;
                ApiUid = authFields.ApiUid;
                AuthSig = authFields.AuthSig;
                SessionKey = authFields.SessionKey;
            }
            else
            {
                OnSocialAPIInitializationFailed("Unable to get auth fields");
                return;
            }

            InstallReferrer = _bridge.GetInstallReferrer();
            Initialized = true;
            SocialAPIInitialized();
        }

        internal void OnSocialAPIInitializationFailed(string error)
        {
            SocialAPIInitializationFailed(error);
        }

        internal void OnProductPurchased(string productId, string transactionId)
        {
            ProductPurchased(productId, transactionId);
        }

        internal void OnProductPurchaseFailed(string productId, string error)
        {
            ProductPurchaseFailed(productId, error);
        }

        internal void OnUserProfileLoaded(SocialProfile profile)
        {
            UserProfileLoaded(profile);
        }

        internal void OnUserProfileLoadingFailed(string error)
        {
            UserProfileLoadingFailed(error);
        }
    }
}
