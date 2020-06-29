using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FizreFox.Server;
using HappyGames.SocialAPI;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    private static Dictionary<string, Sprite> _cache = new Dictionary<string, Sprite>();

    public SocialAPIManager SocialAPIManager;

    [SerializeField]
    private PlayersFactory _playersFactory;

    [SerializeField]
    private SocialAPIJSBridge _socialAPIJSBridge;

    void Awake()
    {
        SocialAPIManager = new SocialAPIManager(_socialAPIJSBridge);
        SocialAPIManager.Initialize();
        SocialAPIManager.UserProfileLoaded += (x) => StartCoroutine(OnPlayerDataLoaded(x));
        SocialAPIManager.UserProfileLoadingFailed += (x) => Debug.Log("UserProfileLoadingFailed");
        SocialAPIManager.LoadUserProfile();
        SocialAPIManager.UserFriendsLoading += (x) => OnUserFriendsLoaded(x);
        SocialAPIManager.UserFriendsLoadingFailed += (x) => OnUserFriendsLoaded(x);
        SocialAPIManager.LoadUserFriends();

        ServerRequests.GetAllUsers(GetAllUsersCallBack, () => Debug.Log("Error"));
    }

    private void OnUserFriendsLoaded(string data)
    {
        Debug.Log(data);
        Debug.LogWarning(data);
        Debug.LogError(data);
    }
    private void GetAllUsersCallBack(string responce)
    {
        User[] users = JsonConvert.DeserializeObject<User[]>(responce);
        StartCoroutine(LoadingData(users));
    }
    private IEnumerator OnPlayerDataLoaded(SocialProfile profile)
    {
        yield return TryLoadTexturesToCache(new[] { profile.Avatar });
        _playersFactory.AddPlayer(_cache.First(x => x.Key == profile.Avatar).Value, profile.Name);
    }

    private IEnumerator LoadingData(User[] users)
    {
        List<string> imgUrls = new List<string>();
        Dictionary<User, Sprite> playersData = new Dictionary<User, Sprite>();

        foreach (var imgUrl in users)
        {
            if (imgUrl.Avatar.Contains("//")) imgUrls.Add(imgUrl.Avatar);
        }

        yield return TryLoadTexturesToCache(imgUrls.ToArray());

        foreach (var user in users)
            playersData.Add(user, _cache.FirstOrDefault(x => x.Key == user.Avatar).Value);

        _playersFactory.Initialized(playersData);
    }

    public static IEnumerator TryLoadTexturesToCache(string[] imageURL, float waitTime = 0)
    {
        var deltaTime = 0f;
        for (int i = 0; i < imageURL.Length; i++)
        {
            if (_cache.ContainsKey(imageURL[i])) continue;

            using (var request = UnityWebRequestTexture.GetTexture(imageURL[i]))
            {
                var response = request.SendWebRequest();
                if (waitTime <= 0)
                {
                    yield return response;
                }
                else
                {
                    while (!response.isDone || (request.isHttpError && request.isNetworkError))
                    {
                        deltaTime += Time.deltaTime;
                        if (deltaTime >= waitTime) yield break;
                        yield return null;
                    }
                }

                if (!request.isHttpError && !request.isNetworkError)
                {
                    var texture2d = DownloadHandlerTexture.GetContent(request);
                    var imageSprite = Sprite.Create(texture2d, new Rect(0.0f, 0.0f, texture2d.width, texture2d.height), new Vector2(.5f, .5f), 100);
                    _cache[imageURL[i]] = imageSprite;
                }
            }
        }
    }
}
