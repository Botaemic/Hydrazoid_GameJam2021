using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid.Weapons
{
    [RequireComponent(typeof(AudioSource))]
    public class ProjectileWeapon : WeaponBase
    {
        [SerializeField] protected Projectile _projectile = null;
        [SerializeField] protected float _fireRate = 1f;
        
        protected float _timer = 0f;
        protected bool _canShoot = false;
        protected bool _isFiring = false;

        protected override void Start()
        {
            base.Start();
            _timer = _fireRate;
        }

        void Update()
        {
            if (!_canShoot)
            {
                _timer -= Time.deltaTime;
                if (_timer <= Mathf.Epsilon)
                {
                    _timer = _fireRate;
                    _canShoot = true;
                }
            }

            Shoot();
        }

        public override void PullTrigger()
        {
            _isFiring = true;
        }

        public override void ReleaseTrigger()
        {
            _isFiring = false;
        }


        private void Shoot()
        {
            if (_canShoot && _isFiring)
            {
                _canShoot = false;

                GetCurrentMuzzle(out Vector3 position, out Quaternion rotation);
                if (_muzzleFlashPrefab != null)
                {
                    var muzzleFlash = Instantiate(_muzzleFlashPrefab, position, rotation);
                    Destroy(muzzleFlash, 0.05f);
                }
                SpawnProjectile(position, rotation);
                _audioSource?.PlayOneShot(_shotSFX);
            }
        }

        private void SpawnProjectile(Vector3 position, Quaternion rotation)
        {
            var shot = Instantiate(_projectile, position, rotation);
            //Dirty way to do it, works for now.
            shot.Owner = this;
            shot.MuzzleVelocity = _muzzleVelocity;
            shot.LifeTime = (_range / _muzzleVelocity);
            shot.WeaponEffect = _weaponEffect;
        }
    }
}