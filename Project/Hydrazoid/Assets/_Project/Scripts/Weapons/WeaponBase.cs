using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid.Weapons
{
    public enum WeaponType
    {
        MELEE,
        RIFLE,
        PISTOL,
        ROCKETLAUNCHER
    }

    public abstract class WeaponBase : MonoBehaviour
    {
        //Dirty sollution
        public LayerMask OwnLayer = default;

        //TODO rearrange this
        [SerializeField] protected GameObject _heldBy = null;
        [SerializeField] protected WeaponEffectSO _weaponEffect = null;

        [SerializeField] protected float _muzzleVelocity = 100f;
        [SerializeField] protected Transform[] _muzzles = null;
        [SerializeField] protected WeaponType _weaponType = WeaponType.RIFLE;


        [Header("Weapon VFX")]
        [SerializeField] protected GameObject _muzzleFlashPrefab = null;
        [Header("Weapon SFX")]
        [SerializeField] protected AudioClip _shotSFX = null;
        [Header("Weapon equip SFX")]
        [SerializeField] protected AudioClip _equipSFX = null;


        public GameObject HeldBy { get => _heldBy; set => _heldBy = value; }
        public GameObject _owner { get; set; } = null;
        public WeaponType WeaponType =>_weaponType; 

        protected float _damage = 10f;
        protected float _range = 150f;
        protected float _firerate = 1f;
        protected AudioSource _audioSource = null;
        protected Transform _transform = null;


        private int _muzzleIndex = 0;

        public abstract void PullTrigger();
        public abstract void ReleaseTrigger();

        public float Damage { get => _damage; }


        protected virtual void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _transform = transform;
        }


        protected void GetCurrentMuzzle(out Vector3 position, out Quaternion rotation)
        {
            position = _muzzles[_muzzleIndex].position;
            rotation = _muzzles[_muzzleIndex].rotation;
            _muzzleIndex++;
            _muzzleIndex %= _muzzles.Length;
        }

        public virtual void Initialize(WeaponEffectSO effect)
        {
            _weaponEffect = effect;
        }

        public virtual void PlayEquipSound()
        {
            _audioSource?.PlayOneShot(_equipSFX);
        }

    }
}