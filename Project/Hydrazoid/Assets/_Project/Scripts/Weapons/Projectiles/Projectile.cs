using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid.Weapons
{
    public class Projectile : MonoBehaviour
    {
        //TODO create Init method.
        [SerializeField] protected LayerMask _layers = default;
        
        protected RaycastHit _hit = default;
        protected float _step = 0f;

        public virtual WeaponBase Owner
        {
            get => _ownerWeapons;
            set
            {
                _ownerWeapons = value;
                var colliders = _ownerWeapons.GetComponents<Collider>();
                _ownerColliders.Clear();
                _ownerColliders.AddRange(colliders);
                _layers &= ~(_ownerWeapons.OwnLayer);
            }
        }
        public virtual WeaponEffectSO WeaponEffect { get; set; }

        public virtual float MuzzleVelocity
        {
            get => _muzzleVelocity;
            set
            {
                _muzzleVelocity = value;
                _step = (_transform.forward * Time.deltaTime * _muzzleVelocity).magnitude * 2f;
            }
        }

        public virtual float LifeTime
        {
            get => _lifeTime;
            set
            {
                _lifeTime = value;
                _timer = _lifeTime;
            }
        }

        protected float _damage = 10f;  // Might not be used anymore since weaponEffect system.
        protected float _muzzleVelocity = 100f;
        protected float _lifeTime = 5f;
        protected WeaponBase _ownerWeapons = null;
        protected List<Collider> _ownerColliders = new List<Collider>();
        protected float _timer = Mathf.Infinity;
        protected Transform _transform = null;


        //Awake is use because Properties can be accessed before Start() has run
        protected virtual void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _timer = _lifeTime;
        }

        protected virtual void Update()
        {
            //if (ObstacleInOurWay()) { Hit(_hit); }
            if (Physics.Raycast(_transform.position, _transform.forward, out _hit, _step, _layers))
            {
                Hit(_hit);
            }
            else
            {
                _timer -= Time.deltaTime;
                if (_timer < Mathf.Epsilon) { Destroy(); }

                _transform.position += _transform.forward * Time.deltaTime * _muzzleVelocity;
            }
        }

        protected virtual void Hit(RaycastHit hit)
        {
            if (hit.collider.gameObject.TryGetComponent(out Damageable obj))
            {
                WeaponEffect.Execute(obj, _ownerWeapons.HeldBy.GetComponent<Character>());
            }
            Destroy();
        }

        //protected bool ObstacleInOurWay()
        //{
        //    // step is fixed size... move it
        //    //float step = (_transfrom.forward * Time.deltaTime * _muzzleVelocity).magnitude * 2f;
        //    if (Physics.Raycast(_transfrom.position, _transfrom.forward, out _hit, _step, _layers))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        protected virtual void Destroy()
        {
            Destroy(gameObject);
        }
    }
}