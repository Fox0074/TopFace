using System;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace FizreFox.Server
{
    public static class ServerRequests 
    {
        private static string ServerAPIUrl = "https://fizerfox.tk/vkapi.php/";
        private static async void SendRequset(string uri, Action<string> onSuccess, Action onError)
        {
            UnityWebRequest request = UnityWebRequest.Get(uri);

                await request.SendWebRequest();

                if (!request.isHttpError && !request.isNetworkError)
                    onSuccess.Invoke(request.downloadHandler.text); 
                else 
                    onError.Invoke();
        }

        public static void GetAllUsers(Action<string> onSuccess, Action onError)
        {
            SendRequset(ServerAPIUrl + "?RequestType=GetAllUsersData", onSuccess, onError);
        }
    }
}