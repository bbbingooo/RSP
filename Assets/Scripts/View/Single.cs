using Assets.Scripts.Data;
using Assets.Scripts.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class Single : MonoBehaviour
    {
        [SerializeField] private SingleGameScreen _gameScreen;

        [SerializeField] private Image _avatarImage;
        [SerializeField] private TextMeshProUGUI _balanceText;

        [SerializeField] private ButtonClick _backButton;
        [SerializeField] private ButtonClick _startButton;

        private async void Start()
        {
            _gameScreen.gameObject.SetActive(false);
            Subscribe();
            var sprite = await WebService.DownloadAvatar(Constants.UserId);
            _avatarImage.sprite = sprite;
            var balance  = await WebService.DownloadBalance(Constants.UserId);
            _balanceText.text = $"{balance}";
        }

        private void Subscribe()
        {
            _backButton.Clicked += Back;
            _startButton.Clicked += StartGame;
        }

        private void Unsubscribe()
        {
            _backButton.Clicked -= Back;
            _startButton.Clicked -= StartGame;
        }

        private void StartGame()
        {
            _startButton.Clicked -= StartGame;
            _gameScreen.gameObject.SetActive(true);
        }

        private void Back()
        {
            Unsubscribe();
            SceneManager.LoadScene(Constants.MainMenuLabel);
        }
    }
}