using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FizreFox.Server;
using HappyGames.SocialAPI;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace FizreFox
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Current { get; private set;}

        public static UsersManager UsersManager {get; private set;}

        public SocialAPIManager SocialAPIManager;

        [SerializeField]
        private PlayersFactory _playersFactory;

        [SerializeField]
        private SocialAPIJSBridge _socialAPIJSBridge;

        [SerializeField]
        private Sprite _noAvatarSprite;

        void Awake()
        {
            NetworkImage.AddToCahe("noAvatar", _noAvatarSprite);
            UsersManager = new UsersManager();
            Current = this;
            SocialAPIManager = new SocialAPIManager(_socialAPIJSBridge);
            SocialAPIManager.Initialize();
            SocialAPIManager.UserProfileLoaded += (x) => OnPlayerDataLoaded(x);
            SocialAPIManager.UserProfileLoadingFailed += (x) => Debug.Log("UserProfileLoadingFailed");
            SocialAPIManager.LoadUserProfile();
            SocialAPIManager.UserFriendsLoading += OnUserFriendsLoaded;
            SocialAPIManager.UserFriendsLoadingFailed += OnUserFriendsLoaded;
            SocialAPIManager.LoadUserFriends();

            SocialAPIManager.ProductPurchased += OnProductPurchased;
            SocialAPIManager.ProductPurchaseFailed += OnProductPurchasedFail;

            ServerRequests.GetAllUsers(GetAllUsersCallBack, () => Debug.Log("GetAllUsersError"));
        }

        private void OnProductPurchased(string productId, string trransactionId)
        {
            UsersManager.CurrentUser.Donate += float.Parse(productId);
            ServerRequests.SuccessProductBuy(UsersManager.CurrentUser, productId);
            UsersManager.CurrentUser.DataUpdated.Invoke();
        }
        private void OnProductPurchasedFail(string productId, string trransactionId)
        {
            Debug.Log("OnProductPurchasedFail");
        }
        private void OnUserFriendsLoaded(string data)
        {
            List<User> friendsUsers = JsonConvert.DeserializeObject<VkUserData[]>(data).Select(u => u.ConvertToUser()).ToList();
            friendsUsers.RemoveAll(x => x.UserName.ToLower() == "deleted ");
            friendsUsers.Where(x => 
            string.IsNullOrEmpty(x.Avatar) || 
            x.Avatar == "https://vk.com/images/deactivated_200.png" || 
            x.Avatar == "https://vk.com/images/camera_200.png?ava=1").ToList().ForEach(x => x.Avatar = "noAvatar");
            UsersManager.FriendsUsers = friendsUsers;

            if (UsersManager.FriendsUsers.Count > 0)  
            {
                StartCoroutine(NetworkImage.TryLoadTexturesToCache(friendsUsers.Select(x => x.Avatar).ToArray()));
                ServerRequests.GetUsersData(friendsUsers, UpdateUsersData, () => Debug.Log("GetUsersData Fail"));
            }

        }
        private void GetAllUsersCallBack(string responce)
        {
            List<User> bdUsers = JsonConvert.DeserializeObject<User[]>(responce).ToList();
            bdUsers.Remove(bdUsers.FirstOrDefault(user => user.UserId == UsersManager.CurrentUser.UserId));
            bdUsers.RemoveAll(x => x.UserName.ToLower() == "deleted ");

            bdUsers.Where(x => 
            string.IsNullOrEmpty(x.Avatar) || 
            x.Avatar == "https://vk.com/images/deactivated_200.png" || 
            x.Avatar == "https://vk.com/images/camera_200.png?ava=1").ToList().ForEach(x => x.Avatar = "noAvatar");

            UsersManager.WorldTopUsers = bdUsers;

             if (UsersManager.WorldTopUsers.Count > 0)  
                StartCoroutine(NetworkImage.TryLoadTexturesToCache(bdUsers.Select(x => x.Avatar).ToArray()));

            if (UsersManager.WorldTopUsers.Count > 0) 
                _playersFactory.SetWorldUsers();
        }
        private void OnPlayerDataLoaded(SocialProfile profile)
        {
            User user = new User();
            user.UserName = profile.Name;
            user.Avatar = profile.Avatar;
            user.UserId = profile.Id;

            UsersManager.CurrentUser = user;
            ServerRequests.UpdateUserInfo(UsersManager.CurrentUser, null, null);
            ServerRequests.GetUsersData(new List<User>{ UsersManager.CurrentUser}, AddUserView, () => Debug.Log("GetUsersData Fail"));

            StartCoroutine(NetworkImage.TryLoadTexturesToCache(new[] { profile.Avatar }));
        }

        private void AddUserView(string responceData)
        {
             List<User> responceUsers = JsonConvert.DeserializeObject<User[]>(responceData).ToList();
              if (responceUsers.Any(x => x.UserId == UsersManager.CurrentUser.UserId)) 
              {
                UsersManager.CurrentUser.Donate = responceUsers.First(x => x.UserId == UsersManager.CurrentUser.UserId).Donate;
                UsersManager.CurrentUser.DataUpdated.Invoke();
              }
        }
        private void UpdateUsersData(string responceData)
        {
            List<User> responceUsers = JsonConvert.DeserializeObject<User[]>(responceData).ToList();
            if (responceUsers.Any(x => x.UserId == UsersManager.CurrentUser.UserId)) 
                UsersManager.CurrentUser.Donate = responceUsers.First(x => x.UserId == UsersManager.CurrentUser.UserId).Donate;

            foreach (var user in responceUsers)
            {
                if (UsersManager.FriendsUsers.Any(u => u.UserId == user.UserId)) 
                    UsersManager.FriendsUsers.First(x => x.UserId == user.UserId).Donate = user.Donate;
            }
        }
    }
}
               