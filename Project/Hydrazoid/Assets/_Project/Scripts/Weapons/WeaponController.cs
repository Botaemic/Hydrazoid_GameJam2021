using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private WeaponBase _currentWeapon = null;

        //fast and dirty solution
        [SerializeField] private Transform _rifleMount = null;
        [SerializeField] private Transform _pistolMount = null;
        [SerializeField] private Transform _meleeMount = null;
        [SerializeField] private Transform _rocketLauncherMount = null;

        public WeaponBase Weapon { get => _currentWeapon;  }
        public Character Owner  { get; set; }

        void Start()
        {
            Owner = GetComponent<Character>();
            if (_currentWeapon != null) {  _currentWeapon.HeldBy = Owner.gameObject; }
        }

        public void PullTrigger()
        {
            _currentWeapon?.PullTrigger();
        }

        public void ReleaseTrigger()
        {
            _currentWeapon?.ReleaseTrigger();
        }

        public WeaponBase SwapWeapon(WeaponBase newWeapon)
        {
            WeaponBase oldWeapon = _currentWeapon;
            _currentWeapon = newWeapon;

            if(_currentWeapon == null) { return oldWeapon; }

            _currentWeapon.HeldBy = Owner.gameObject; 
         
            //again fast and dirty
            switch (_currentWeapon.WeaponType)
            {
                case WeaponType.MELEE:
                    _currentWeapon.transform.position = _meleeMount.position;
                    _currentWeapon.transform.parent = _meleeMount;
                    break;
                case WeaponType.RIFLE:
                    _currentWeapon.transform.position = _rifleMount.position;
                    _currentWeapon.transform.parent = _rifleMount;
                    break;
                case WeaponType.PISTOL:
                    _currentWeapon.transform.position = _pistolMount.position;
                    _currentWeapon.transform.parent = _pistolMount;
                    break;
                case WeaponType.ROCKETLAUNCHER:
                    _currentWeapon.transform.position = _rocketLauncherMount.position;
                    _currentWeapon.transform.parent = _rocketLauncherMount;
                    break;
                default:
                    break;
            }

            _currentWeapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
            _currentWeapon?.PlayEquipSound();

            return oldWeapon;
        }
    }
}