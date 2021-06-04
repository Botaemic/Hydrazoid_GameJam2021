using UnityEngine;
using UnityEngine.Events;

namespace Hydrazoid
{
    public class SingleStat
    {
        public UnityAction OnStatChanged;

        private static float STAT_MINIMUM = 0f;
        private float _currentValue = 10f;
        private float _maxValue = 10f;

        public float GetStatAmountNormalized { get => (_currentValue / _maxValue); }
        public float GetStatAmount { get => _currentValue; protected set => _currentValue = value; }
        public float GetStatMaximumAmount { get => _maxValue; protected set => _maxValue = value; }


        public SingleStat(float maximumValue)
        {
            SetStatMaximumAmount( maximumValue);
            _currentValue = maximumValue;
        }

        public void SetStatMaximumAmount(float amount)
        {
            _maxValue = amount;
            InvokeCallback();
        }

        public void IncreaseStatAmount(float amount)
        {
            _currentValue += amount;
            _currentValue = Mathf.Clamp(_currentValue, STAT_MINIMUM, _maxValue);
            InvokeCallback();
        }

        public void DecreaseStatAmount(float amount)
        {
            _currentValue -= amount;
            _currentValue = Mathf.Clamp(_currentValue, STAT_MINIMUM, _maxValue);
            InvokeCallback();
        }

        public void SetCurrentStatAmount(float amount)
        {
            _currentValue = amount;
            _currentValue = Mathf.Clamp(_currentValue, STAT_MINIMUM, _maxValue);
            InvokeCallback();
        }

        private void InvokeCallback()
        {
            OnStatChanged?.Invoke();
        }
    }
}