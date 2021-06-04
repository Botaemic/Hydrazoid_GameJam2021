using Hydrazoid.Weapons;
using UnityEngine;

namespace Hydrazoid
{
   public abstract class Character : MonoBehaviour
   {
        [SerializeField] protected CharacterStats _characterStats = null;
        //[SerializeField] protected WeaponBase _currentWeapon = null;
        [SerializeField] protected Health _health = null;
        [SerializeField] protected CharacterAnimator _animator = null;

        protected WeaponController _weaponController = null;
        protected Transform _transform = null;

        public WeaponBase Weapon { get => _weaponController.Weapon; }
        
        protected virtual void Start()
        {
            _transform = transform;
            _characterStats = GetComponent<CharacterStats>();
            _weaponController = GetComponent<WeaponController>();
            _weaponController.Owner = this;

            _health = GetComponent<Health>();
            _health.OnDeath += OnDeath;

        }

        private void OnDisable()
        {
            _health.OnDeath -= OnDeath;
        }

        protected virtual void OnDeath()
        {
            if (TryGetComponent(out EnemyWallet enemyWallet))
            {
                enemyWallet.OnDeath();
            }
            Destroy(gameObject);
        }

        public virtual float MeleeDamage 
        { 
            get => _weaponController.Weapon.Damage * (1f + _characterStats.Damage + _characterStats.DamageMelee);
        }

        public virtual float RangedDamage
        {
            get => _weaponController.Weapon.Damage * (1f + _characterStats.Damage + _characterStats.DamageRanged);
        }
    }
}