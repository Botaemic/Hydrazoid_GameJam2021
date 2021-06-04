using UnityEngine;

namespace Hydrazoid.SceneManagement
{
    public class LoadingScreen : ILoadingScreen
    {

        [SerializeField] private TMPro.TextMeshProUGUI _loadingText = null;
        [SerializeField] private UnityEngine.UI.Slider _progressBar = null;

        public override bool ShowLoadingScreen { set => gameObject.SetActive(value); }
        public override float Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                UpdateProgressBar();
            }
        }

        private float _progress = 0f;

        private void UpdateProgressBar()
        {
            SetSlider(_progressBar, _progress);
            UpdateText();
        }

        private void UpdateText()
        {
            _loadingText.text = "Loading... " + (int)(_progress * 100f) + "%";
        }

        private void SetSlider(UnityEngine.UI.Slider slider, float value)
        {
            value = Mathf.Clamp(value, slider.minValue, slider.maxValue);
            slider.value = value;
        }
    }
}