using Hydrazoid.Extensions;
using UnityEngine;

namespace Hydrazoid.Weapons
{
    public class Rocket : Projectile
    {
        [SerializeField] protected float _blastRadius = 5;
        [SerializeField] protected float _selfDamage = 50f;
        [SerializeField] protected AnimationCurve _damageFalloff = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));
        [SerializeField] protected GameObject _explosionPrefab = null;

        public override WeaponBase Owner
        {
            get => _ownerWeapons;
            set
            {
                _ownerWeapons = value;
                var colliders = _ownerWeapons.GetComponents<Collider>();
                _ownerColliders.Clear();
                _ownerColliders.AddRange(colliders);
            }
        }

        protected float _curveLength;

        protected override void Awake()
        {
            base.Awake();
            _curveLength = _damageFalloff[_damageFalloff.length - 1].time;
        }

        protected override void Hit(RaycastHit hit)
        {
            Explode();
        }


        protected virtual void Explode()
        {
            Collider[] collidersNear = Physics.OverlapSphere(transform.position, _blastRadius, _layers);
            foreach (Collider collider in collidersNear)
            {
                if(collider.TryGetComponent(out PlayerCharacter player))
                {
                    float distance = _transform.position.DistanceTo(player.transform);
                    float damageMultiplier = _damageFalloff.Evaluate(_curveLength / distance);
                    player.GetComponent<Damageable>().TakeDamage(_selfDamage * damageMultiplier);
                }
                else if(collider.TryGetComponent(out Character character))
                {
                    WeaponEffect.Execute(character.GetComponent<Damageable>(), _ownerWeapons.HeldBy.GetComponent<Character>());
                }
            }

            if(_explosionPrefab != null)
            {
                Instantiate(_explosionPrefab, _transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

    }
}