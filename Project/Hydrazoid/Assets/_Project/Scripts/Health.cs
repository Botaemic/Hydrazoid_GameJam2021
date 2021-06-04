using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Hydrazoid
{
    [System.Serializable]
    public class Health : MonoBehaviour
    {
        public UnityAction OnDeath;
        public UnityAction<float> OnHealthChanged;

        [SerializeField] private float _maximumHealth = 100f;
        [SerializeField] private Bar _healthBar = null;

        public float GetHealthAmount { get => _health.GetStatAmount; }
        public float GetHealthAmountNormalized { get => _health.GetStatAmountNormalized; }

        private SingleStat _health = null;
        //private Bar _currentHealthBar = null;
        private bool _destroyed = false;
        [SerializeField]private float _currentHealth;

        private void Awake()
        {
            _health = new SingleStat(_maximumHealth);
            if(_healthBar) { _healthBar.Initialize(_health); /*SpawnHealthBar();*/  }
            
        }

        private void OnEnable()
        {
            OnHealthChanged += HealthChanged;
            SetMaximumHealthAmount(_maximumHealth);
        }

        private void OnDisable()
        {
            OnHealthChanged -= HealthChanged;
        }

        private void HealthChanged(float amount)
        {
            if (amount > 0)
            {
                DecreaseHealthAmount(amount);
            }
            else
            {
                IncreaseHealthAmount(-amount);
            }
            if (_health.GetStatAmount <= Mathf.Epsilon) { OnDeath?.Invoke(); }
        }

        public void IncreaseHealthAmount(float amount)
        {
            if (!_destroyed)
            {
                _health.IncreaseStatAmount(amount);
            }
        }

        public void DecreaseHealthAmount(float amount)
        {
            _healthBar?.gameObject.SetActive(true);
            _health.DecreaseStatAmount(amount);

            if (_health.GetStatAmount <= Mathf.Epsilon) { OnDeath?.Invoke(); }
        }

        public void SetMaximumHealthAmount(float amount)
        {
            _maximumHealth = amount;
            _health.SetStatMaximumAmount(amount);
        }
        
        public void SetCurrentHealthAmount(float amount)
        {
            _health.SetCurrentStatAmount(amount);
        }

        private void SpawnHealthBar()
        {
            //_currentHealthBar = Instantiate(_healthBar, _spawnPoint.position, Quaternion.identity);
            //_currentHealthBar.transform.SetParent(_healthBarCanvas.transform,false); 
            //_currentHealthBar.Initialize(_health);
        }

        //TODO remove... is only for easy debug
        private void Update()
        {
            _currentHealth = _health.GetStatAmount;
        }
    }
}