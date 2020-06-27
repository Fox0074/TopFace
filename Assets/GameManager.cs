using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    private static Dictionary<string, Sprite> _cache = new Dictionary<string, Sprite>();

    Dictionary<string,string> _fakePlayersData = new Dictionary<string, string>();

    [SerializeField]
    private PlayersFactory _playersFactory;
    void Awake()
    {
        _fakePlayersData.Add("https://sun9-38.userapi.com/c858524/v858524650/3cefe/6l49evu15Wg.jpg","Юрий Хисматуллин");
        _fakePlayersData.Add("https://sun1-28.userapi.com/ZJC4ajp7vsks5ocrx9ML-kH5e70PCRRGmdq1LA/3jiNWoaiqTQ.jpg","MayFox");
        _fakePlayersData.Add("https://sun9-25.userapi.com/c851016/v851016767/a72fd/aXqQtc3yaWc.jpg","Андрей Башкирцев");
        _fakePlayersData.Add("https://sun9-45.userapi.com/c850732/v850732160/4d596/2q3iKRu5C6Q.jpg","Эмиль Якупов");
        _fakePlayersData.Add("https://sun9-9.userapi.com/c639617/v639617265/2f5ce/_fDP4EUCw7o.jpg","Константин Моисеенко");

        StartCoroutine(LoadingData());
    }

    private IEnumerator LoadingData()
    {
        List<string> imgUrls = new List<string>();
        Dictionary<Sprite,string> playersData = new Dictionary<Sprite, string>();

        foreach (var imgUrl in _fakePlayersData)
        {
            imgUrls.Add(imgUrl.Key);
        }
        yield return TryLoadTexturesToCache(imgUrls.ToArray());

        foreach(var fpd in _fakePlayersData)
            playersData.Add(_cache.First(x => x.Key == fpd.Key).Value,fpd.Value);

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
