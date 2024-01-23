using Assets.Scripts.Data;
using Assets.Scripts.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class MainMenu : MonoBehaviour
    {

        [SerializeField] private Image _avatarImage;
        [SerializeField] private TextMeshProUGUI _balanceText;

        [SerializeField] private ButtonClick _singlePlayerButton;
        [SerializeField] private ButtonClick _multiPlayerButton;
        [SerializeField] private ButtonClick _quitButton;

        private async void Start()
        {
            Subscribe();
            var sprite = await WebService.DownloadAvatar(Constants.UserId);
            _avatarImage.sprite = sprite;
            var balance  = await WebService.DownloadBalance(Constants.UserId);
            _balanceText.text = $"{balance}";
        }

        private void PlaySingle()
        {
            Unsubscribe();
            SceneManager.LoadScene(Constants.SingleLabel);
        }

        private void PlayMulti()
        {
            Unsubscribe();
            SceneManager.LoadScene(Constants.MultiLabel);
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