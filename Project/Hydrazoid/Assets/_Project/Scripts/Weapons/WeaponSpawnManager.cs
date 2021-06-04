using Hydrazoid.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hydrazoid.Weapons
{
    public class WeaponSpawnManager : Singleton<WeaponSpawnManager>
    {
        [SerializeField] private bool _spawnRandomWeapons = false;
        [SerializeField] private Pickup _pickupPrefab = null;
        [SerializeField] private List<WeaponBase> _weaponList = new List<WeaponBase>();
        [SerializeField] private int _availableAmountOfWeapons = 4;
        //TODO create effects
        [SerializeField] private List<WeaponEffectSO> _availableWeaponEffects = new List<WeaponEffectSO>();

        [Header("Events")]
        //[SerializeField] private WeaponEffectChangeEventSO _weaponEffectChangeEvent = null;
        [SerializeField] private WeaponDropEventSO _weaponDropEvent = null;
        [SerializeField] private VoidEventChannelSO _voidEvent;

        private System.Random rnd = new System.Random();

        private Dictionary<WeaponType, WeaponEffectSO> _weaponEffectDictionary = new Dictionary<WeaponType, WeaponEffectSO>();
        
        public override void Awake()
        {
            base.Awake();
            _voidEvent.OnEventRaised += OnLevelLoaded;
            _weaponDropEvent.OnWeaponDrop += OnDropWeapon;
        }
        private void OnDisable()
        {
            _voidEvent.OnEventRaised -= OnLevelLoaded;
            _weaponDropEvent.OnWeaponDrop -= OnDropWeapon;
        }

        private void OnLevelLoaded()
        {
            //Build new dictionary tha matches random effect typ a WeaponType
            _weaponEffectDictionary.Clear();
            foreach (WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
            {
                _weaponEffectDictionary.Add(weaponType, GetRandomWeaponEffect());
            }

            if (!_spawnRandomWeapons) { return; }
            for (int i = 0; i < _availableAmountOfWeapons; i++)
            {
                WeaponBase newWeapon = GetRandomWeapon();
                Vector3 position = GetRandomLocationOnNavMesh();
                SpawnPickup(newWeapon, position);
            }
        }

        private void SpawnPickup(WeaponBase weapon, Vector3 position)
        {
            Pickup pickup = Instantiate(_pickupPrefab, position, Quaternion.identity);
            pickup.Initialize(weapon);
        }

        public WeaponEffectSO GetRandomWeaponEffect()
        {
            return _availableWeaponEffects[rnd.Next(_availableWeaponEffects.Count)];
        }

        public WeaponEffectSO GetWeaponEffect(WeaponType weaponType)
        {
            return _weaponEffectDictionary[weaponType];
        }

        private WeaponBase GetRandomWeapon()
        {
            return _weaponList[rnd.Next(_weaponList.Count)];
        }

        private Vector3 GetRandomLocationOnNavMesh()
        {
            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

            // Pick the first indice of a random triangle in the nav mesh
            int t = UnityEngine.Random.Range(0, navMeshData.indices.Length - 3);

            // Select a random point on it
            Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], UnityEngine.Random.value);
            Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], UnityEngine.Random.value);

            return point;
        }

        private void OnDropWeapon(WeaponBase weapon, Vector3 position)
        {
            Pickup pickup = Instantiate(_pickupPrefab, position, Quaternion.identity);
            pickup.Weapon = weapon;
        }
    }
}