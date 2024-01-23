using Assets.Scripts.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Logic
{
    public class WebService
    {
        public static async UniTask<double> DownloadBalance(string userId)
        {
            double result = 0;

            using var request = UnityWebRequest.Get($"{Constants.ServerUrl}get_user_by_uuid/{userId}/");
            try
            {
                await request.SendWebRequest();
            }
            catch
            {
                // ignored
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error!");
            }
            else
            {
                var data = JsonUtility.FromJson<UserBalance>(request.downloadHandler.text);
                result = data.balance_ton;
            }

            return result;
        }

        public static async UniTask<Sprite> DownloadAvatar(string userId)
        {
            Sprite result = null;

            using var request = UnityWebRequest.Get($"{Constants.ServerUrl}avatar/{userId}/");
            try
            {
                await request.SendWebRequest();
            }
            catch
            {
                // ignored
            }
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error!");
            }
            else
            {
                var data = JsonUtility.FromJson<UserAvatar>(request.downloadHandler.text);
                if (data.img_link != "") result = await DownloadImage(data.img_link);
            }

            return result;
        }

        private static async UniTask<Sprite> DownloadImage(string url)
        {
            using var request = UnityWebRequest.Get(url);
            try
            {
                await request.SendWebRequest();
            }
            catch
            {
                // ignored
            }

            var tex = ((DownloadHandlerTexture) request.downloadHandler).texture;
            var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2((float)tex.width / 2, (float)tex.height / 2));

            return sprite;
        }
    }
}