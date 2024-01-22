using Assets.Scripts.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Logic
{
    public class WebService
    {
        private const string ServerUrl = "https://new.tonbingo.com/avatar/";
        private const string UserId = "15027401";

        private UserAvatar _userAvatar;

        public async UniTask<Sprite> DownloadAvatar()
        {
            using var request = UnityWebRequest.Get($"{ServerUrl}{UserId}/");
            try
            {
                await request.SendWebRequest();
            }
            catch
            {
                // ignored
            }

            return await ProcessResponse(request);
        }

        private static async UniTask<Sprite> ProcessResponse(UnityWebRequest request)
        {
            Sprite result = null;

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error!");
            }
            else
            {
                var data = JsonUtility.FromJson<UserAvatar>(request.downloadHandler.text);
                result = await DownloadImage(data.img_link);
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