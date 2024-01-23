using Assets.Scripts.Data;
using Assets.Scripts.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class SingleGameScreen : MonoBehaviour
    {
        private int _playerScore;
        private int _computerScore;
        [SerializeField] private TextMeshProUGUI _playerScoreText;
        [SerializeField] private TextMeshProUGUI _computerScoreText;

        [SerializeField] private Image _playerAvatar;
        [SerializeField] private Image _computerAvatar;

        [SerializeField] private ButtonClick _stoneButton;
        [SerializeField] private ButtonClick _scissorsButton;
        [SerializeField] private ButtonClick _paperButton;

        [SerializeField] private ResultScreen _resultScreen;

        private async void Start()
        {
            _playerAvatar.sprite = await WebService.DownloadAvatar(Constants.UserId);
            _computerAvatar.sprite = await WebService.DownloadAvatar(Constants.UserId);
            _resultScreen.gameObject.SetActive(false);

            Subscribe();
        }

        private void OnDestroy() => 
            Unsubscribe();

        private void Subscribe()
        {
            _stoneButton.Clicked += PlayStone;
            _scissorsButton.Clicked += PlayScissors;
            _paperButton.Clicked += PlayPaper;
        }

        private void Unsubscribe()
        {
            _stoneButton.Clicked -= PlayStone;
            _scissorsButton.Clicked -= PlayScissors;
            _paperButton.Clicked -= PlayPaper;
        }

        private void PlayStone() => 
            ProcessResult(SingleGameService.SingleGame(0));

        private void PlayScissors() => 
            ProcessResult(SingleGameService.SingleGame(1));

        private void PlayPaper() => 
            ProcessResult(SingleGameService.SingleGame(2));

        private void ProcessResult(int gameResult)
        {
            switch (gameResult)
            {
                case 1:
                    _computerScore++;
                    _computerScoreText.text = $"{_computerScore}";
                    break;
                case 2:
                    _playerScore++;
                    _playerScoreText.text = $"{_playerScore}";
                    break;
            }

            _resultScreen.gameObject.SetActive(true);
        }
    }
}