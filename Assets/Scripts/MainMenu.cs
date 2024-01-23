using Assets.Scripts.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        private const string SingleLabel = "Single";
        private const string MultiLabel = "Multi";

        private WebService _webService;

        [SerializeField] private Image _avatarImage;
        [SerializeField] private TextMeshProUGUI _balanceText;

        [SerializeField] private ButtonClick _singlePlayerButton;
        [SerializeField] private ButtonClick _multiPlayerButton;
        [SerializeField] private ButtonClick _quitButton;

        private async void Start()
        {
            _webService = new WebService();
            Subscribe();
            var sprite = await _webService.DownloadAvatar();
            _avatarImage.sprite = sprite;
            var balance  = await _webService.DownloadBalance();
            _balanceText.text = $"{balance}";
        }

        private void PlaySingle() => 
            SceneManager.LoadScene(SingleLabel);

        private void PlayMulti() => 
            SceneManager.LoadScene(MultiLabel);

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
            _singlePlayerButton.Clicked += PlaySingle;
            _multiPlayerButton.Clicked += PlayMulti;
            _quitButton.Clicked += Quit;
        }

        private void Unsubscribe()
        {
            _singlePlayerButton.Clicked -= PlaySingle;
            _multiPlayerButton.Clicked -= PlayMulti;
            _quitButton.Clicked -= Quit;
        }
    }
}