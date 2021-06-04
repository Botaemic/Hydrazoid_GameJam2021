using UnityEngine;
using UnityEngine.UI;

namespace Hydrazoid
{
    public class FadingBar : Bar
    {
        [SerializeField] private Gradient _foregroundGradient = default;
        [SerializeField] private Color _backgroundColor = Color.black;
        [SerializeField] private Color _fadingColor = Color.red;

        [SerializeField] private float updateSpeedInSeconds = 0.1f;

        private Image _bar = null;
        private Image _fadingImage = null;
        private float currentBackgroundValue = 1f;
        private Transform _text = null;
        private Text _barText = null;
        private Transform _transform = null;
        private Camera _camera = null;
        

        private void Start()
        {
            _camera = Camera.main;
            _transform = transform;

            //TODO serialize this
            _bar = transform.Find("Bar").GetComponent<Image>();
            _bar.fillAmount = 1f;
            _bar.color = _foregroundGradient.Evaluate(1f); ;

            transform.Find("Background").GetComponent<Image>().color = _backgroundColor;
            _fadingImage = transform.Find("FadingImage").GetComponent<Image>();
            _fadingImage.color = _fadingColor;

            _text = transform.Find("Text"); // If there is a text object then find it.
            if (_text) { _barText = _text.GetComponent<Text>(); }
            
        }

        public override void Initialize(SingleStat newStat)
        {
            base.Initialize(newStat);
            if (_barText != null) { _barText.text = FormatString(); }
        }


        protected override void UpdateBar()
        {
            if (_bar)
            {
                _bar.fillAmount = _stat.GetStatAmountNormalized;
                _bar.color = _foregroundGradient.Evaluate(_stat.GetStatAmountNormalized);
                //if (_barText) { _barText.text = FormatString(); }
            }
        }

        private void LateUpdate()
        {
            if (_bar)
            {
                //Quaternion rotation = _camera.transform.rotation;
                //_transform.LookAt(_transform.position + rotation * Vector3.forward, rotation * Vector3.up);

                if (_stat.GetStatAmount <= Mathf.Epsilon) // stat is 0
                {
                    //_barText.text = _stat.GetStatAmount.ToString("-") + "/" + _stat.GetStatMaximumAmount.ToString("#.");
                    _fadingImage.fillAmount = 0f;
                    _bar.color = _foregroundGradient.Evaluate(_stat.GetStatAmountNormalized);
                    return;
                }

                if (_fadingImage.fillAmount > _bar.fillAmount)
                {
                    currentBackgroundValue -= Time.deltaTime * updateSpeedInSeconds;
                    _fadingImage.fillAmount = currentBackgroundValue;
                    return;
                }

                if (_fadingImage.fillAmount < _bar.fillAmount) //Fading image is smaller then the current health
                {
                    currentBackgroundValue = _bar.fillAmount;
                    _fadingImage.fillAmount = _bar.fillAmount;
                    return;
                }
            }
        }

        private string FormatString()
        {
            return _stat.GetStatAmount.ToString("#.") + "/" + _stat.GetStatMaximumAmount.ToString("#.");
        }
    }
}