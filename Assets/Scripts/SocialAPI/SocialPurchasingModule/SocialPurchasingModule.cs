using UnityEngine;
using UnityEngine.Purchasing.Extension;
using HappyGames.SocialAPI;

namespace HappyGames.Purchasing
{
    public class SocialPurchasingModule : AbstractPurchasingModule
    {
        //private LocalizationService _localizationService;
        //private Server _server;
        private SocialAPIManager _socialAPIManager;

        // public SocialPurchasingModule(LocalizationService localizationService, Server server, SocialAPIManager socialApiManager)
        // {
        //     _localizationService = localizationService;
        //     _server = server;
        //     _socialAPIManager = socialApiManager;
        // }

        public override void Configure()
        {
            //RegisterStore(
              //  SocialPurchasingStore.STORE_NAME,
                //Application.platform == RuntimePlatform.WebGLPlayer ?
                  //  new SocialPurchasingStore(_localizationService, _server, _socialAPIManager) : null
            //);
        }
    }
}
