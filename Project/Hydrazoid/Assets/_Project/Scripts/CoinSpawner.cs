using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid
{
    public class CoinSpawner : MonoBehaviour
    {
        [Header("Settings for Floating Coins")]
        [SerializeField] private int _coinValue = 1;
        [SerializeField] private float _minXForRandomRange = -2f;
        [SerializeField] private float _maxXForRandomRange = 2f;
        [SerializeField] private float _minYForRandomRange = 5f;
        [SerializeField] private float _maxYForRandomRange = 10f;
        [SerializeField] private float _minZForRandomRange = -2f;
        [SerializeField] private float _maxZForRandomRange = 2f;
        [SerializeField] private float _coinSpawnInterval = 0.01f;

        [SerializeField] private Rigidbody _coinPrefab;
        [SerializeField] private AudioClip _coinSound;

        private CharacterStats _playerCharacterStats;

        private void OnEnable()
        {
            EventList.OnKill += SpawnCoinsOnKill;
        }

        private void OnDisable()
        {
            EventList.OnKill -= SpawnCoinsOnKill;
        }

        private void Start()
        {
            _playerCharacterStats = FindObjectOfType<PlayerCharacter>().GetComponent<CharacterStats>();
        }

        private void SpawnCoinsOnKill(Vector3 position, int cashAmount)
        {
            if (_playerCharacterStats)
            {
                int numberOfCoins = Mathf.RoundToInt(cashAmount / _coinValue * (1 + _playerCharacterStats.CashGainModifier));
                StartCoroutine(SpawnCoins(position, _coinValue, numberOfCoins));
                //Debug.Log($"Spawning coins: {numberOfCoins}");
            }
            else
            {
                int numberOfCoins = Mathf.RoundToInt(cashAmount / _coinValue);
                StartCoroutine(SpawnCoins(position, _coinValue, numberOfCoins));
            }
            
        }

        private IEnumerator SpawnCoins(Vector3 position, int coinValue, int numberOfCoins)
        {
            for (int i = 0; i < numberOfCoins; i++)
            {
                Quaternion rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                Rigidbody newCoin = Instantiate(_coinPrefab, position, rotation);
                FloatingCoin newCoinSetup = newCoin.gameObject.AddComponent<FloatingCoin>();
                newCoinSetup.Setup(coinValue);
                newCoin.velocity = new Vector3(Random.Range(_minXForRandomRange, _maxXForRandomRange), Random.Range(_minYForRandomRange, _maxYForRandomRange), Random.Range(_minZForRandomRange, _maxZForRandomRange));

                AudioSource.PlayClipAtPoint(_coinSound, Camera.main.transform.position);

                yield return new WaitForSeconds(_coinSpawnInterval);
            }
        }
    }
}

