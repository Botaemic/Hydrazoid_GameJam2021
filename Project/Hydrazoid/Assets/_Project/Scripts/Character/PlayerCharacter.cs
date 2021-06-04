using Hydrazoid.SceneManagement;
using Hydrazoid.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace Hydrazoid
{

    public class PlayerCharacter : Character
    {
        [SerializeField] protected CharacterControls _controls = null;

        [Header("Events")]
        [SerializeField] protected WeaponEffectChangeEventSO _weaponEffectChangeEvent = null;
        [SerializeField] protected WeaponDropEventSO _weaponDropEvent = null;
        [SerializeField] protected SceneLoadEvent _sceneLoadEvent = null;

        protected CharacterPerks _characterPerks = null;
        protected PlayerDeathExit _playerDeathExit = null;

        private System.Random rnd = new System.Random();

        public bool Dead { get; set; } = false;

        private void OnEnable()
        {
            EventList.OnMaxHealthChange += ProcessMaxHealthChange;
            _weaponEffectChangeEvent.OnWeaponEffectChanged += ChangeWeaponEffect;
        }

        private void OnDisable()
        {
            EventList.OnMaxHealthChange -= ProcessMaxHealthChange;
            _weaponEffectChangeEvent.OnWeaponEffectChanged -= ChangeWeaponEffect;
        }

        protected override void Start()
        {
            base.Start();
            GameManager.Instance.Player = this;

            _controls = GetComponent<CharacterControls>();
            _characterPerks = GetComponent<CharacterPerks>();
            _playerDeathExit = GetComponent<PlayerDeathExit>();

            ProcessMaxHealthChange(_characterStats.MaxHealth);

            InvokeRepeating("OilyFun", 5f, 5f);
        }

        private void ChangeWeaponEffect(Dictionary<WeaponType, WeaponEffectSO> effects)
        {
            _weaponController.Weapon?.Initialize(effects[_weaponController.Weapon.WeaponType]);
        }

        private void ProcessMaxHealthChange(int amount)
        {
            if (_health.GetHealthAmountNormalized < 1f)
            {
                _health.SetMaximumHealthAmount(amount);
            }
            else
            {
                _health.SetMaximumHealthAmount(amount);
                _health.SetCurrentHealthAmount(amount);
            }
        }

        protected override void OnDeath()
        {
            Dead = true;
            _controls.Enabled = false;
            _animator.PlayDeathAnimation();
            _playerDeathExit.OnDeath();
        }


        /*private void LateUpdate()
        {
            if (_characterStats.Oily)
            {
                int chance = rnd.Next(100);
                if (chance < 15 && _weaponController.Weapon != null)
                {
                    _weaponDropEvent?.RaiseEvent(_weaponController.SwapWeapon(null), _transform.position);
                }
            }
        }*/

        private void OilyFun()
        {
            if (_characterStats.Oily)
            {
                int chance = rnd.Next(100);
                if (chance < 15 && _weaponController.Weapon != null)
                {
                    _weaponDropEvent?.RaiseEvent(_weaponController.SwapWeapon(null), _transform.position);
                }
            }
        }
    }
}