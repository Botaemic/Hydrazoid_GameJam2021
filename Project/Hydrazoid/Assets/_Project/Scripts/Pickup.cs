using Hydrazoid.Weapons;
using UnityEngine;

namespace Hydrazoid
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private Transform _hoverLocation = null;
        [SerializeField] private WeaponBase _hoverObject = null;
        
        [Header("For placed pickups use the prefab slot, else leave empty")]
        [SerializeField] private WeaponBase _weaponPrefab = null;

        public WeaponBase Weapon 
        {
            get
            {
                return _hoverObject;
            }
            set 
            {
                _hoverObject = value;
                if (_hoverObject != null)
                {
                    _hoverObject.transform.parent = _hoverLocation;
                    _hoverObject.transform.localPosition = new Vector3(0, 0, 0);
                    _hoverObject.transform.rotation = Quaternion.Euler(0, 90, 90);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }

        public void Initialize(WeaponBase weaponPrefab)
        {
            _hoverObject = Instantiate(weaponPrefab, _hoverLocation.position, Quaternion.Euler(0, 90, 90), _hoverLocation);
            _hoverObject.transform.localPosition = new Vector3(0, 0, 0);
            _hoverObject.transform.rotation = Quaternion.Euler(0, 90, 90);
            //dirty but works for now
            _hoverObject.Initialize(WeaponSpawnManager.Instance.GetWeaponEffect(_hoverObject.WeaponType));
        }

        private void Start()
        {
            if (_weaponPrefab == null) { return; }
            _hoverObject = Instantiate(_weaponPrefab, _hoverLocation.position, Quaternion.Euler(0, 90, 90), _hoverLocation);
        }

    }
}