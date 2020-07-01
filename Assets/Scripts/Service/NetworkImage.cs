using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace FizreFox
{
    public class NetworkImage : Image
    {
        private static Dictionary<string, Sprite> _cache = new Dictionary<string, Sprite>();

        private string _loadedURL;

        public void LoadImage(string imageURL, bool cacheOnly = false)
        {
            if (!string.IsNullOrEmpty(imageURL))
            {
                if (imageURL != _loadedURL)
                {
                    Sprite imageSprite;

                    if (_cache.TryGetValue(imageURL, out imageSprite))
                    {
                        overrideSprite = imageSprite;
                        return;
                    }

                    if (!cacheOnly)
                        StartCoroutine(FetchTexture(imageURL));
                }
            }
            else
            {
                overrideSprite = null;
            }
        }
        
        public void LoadImage(Sprite sprite)
        {
            _loadedURL = null;
            overrideSprite = sprite;
        }

        private IEnumerator FetchTexture(string imageURL)
        {
            using (var request = UnityWebRequestTexture.GetTexture(imageURL))
            {
                yield return request.SendWebRequest();

                if (!request.isHttpError && !request.isNetworkError)
                {
                    var texture2d = DownloadHandlerTexture.GetContent(request);
                    var imageSprite = Sprite.Create(texture2d, new Rect(0.0f, 0.0f, texture2d.width, texture2d.height), new Vector2(.5f, .5f), 100);
                    overrideSprite = imageSprite;
                    _cache[imageURL] = imageSprite;
                    _loadedURL = imageURL;
                }
            }
        }

        public static void AddToCahe(string key, Sprite value)
        {
            _cache.Add(key, value);
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
}
