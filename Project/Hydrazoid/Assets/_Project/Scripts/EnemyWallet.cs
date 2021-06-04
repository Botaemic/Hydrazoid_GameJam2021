using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid
{
    public class EnemyWallet : MonoBehaviour
    {
        [SerializeField] private int _minCashRange = 10;
        [SerializeField] private int _maxCashRange = 25;
        
        private int _cash = 25;

        private void Awake()
        {
            _cash = Random.Range(_minCashRange, _maxCashRange + 1);
        }

        public void OnDeath()
        {
            EventList.OnKill?.Invoke(transform.position, _cash);
        }
    }
}

