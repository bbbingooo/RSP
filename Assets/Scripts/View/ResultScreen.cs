using UnityEngine;

namespace Assets.Scripts.View
{
    public class ResultScreen : MonoBehaviour
    {
        [SerializeField] private ButtonClick _restartButton;

        private void OnEnable()
        {
            _restartButton.Clicked += Close;
        }

        private void Close()
        {
            _restartButton.Clicked -= Close;
            gameObject.SetActive(false);
        }
    }
}