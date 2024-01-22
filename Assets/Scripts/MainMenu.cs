using Assets.Scripts.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        private WebService _webService;

        [SerializeField] private Image _avatarImage;

        [SerializeField] private ButtonClick _singlePlayerButton;
        [SerializeField] private ButtonClick _multiPlayerButton;
        [SerializeField] private ButtonClick _quitButton;

        private async void Start()
        {
            _webService = new WebService();
            Subscribe();
            _avatarImage.sprite = await _webService.DownloadAvatar();
        }

        private void Quit() 
        {
            Unsubscribe();
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log($"{this.name} : {this.GetType()} : {System.Reflection.MethodBase.GetCurrentMethod()?.Name}");
#endif
#if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
            Application.Quit();
#elif (UNITY_WEBGL)
            Application.OpenURL("about:blank");
#endif
        }

        private void Subscribe()
        {
            _singlePlayerButton.Clicked += Quit;
            _multiPlayerButton.Clicked += Quit;
            _quitButton.Clicked += Quit;
        }

        private void Unsubscribe()
        {
            _singlePlayerButton.Clicked -= Quit;
            _multiPlayerButton.Clicked -= Quit;
            _quitButton.Clicked -= Quit;
        }
    }
}