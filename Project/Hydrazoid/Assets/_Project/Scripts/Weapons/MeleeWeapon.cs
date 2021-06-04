using UnityEngine;

namespace Hydrazoid.Weapons
{
    public class MeleeWeapon : WeaponBase
    {
        public override void PullTrigger()
        {
            // Does nothing
            return;
        }

        public override void ReleaseTrigger()
        {
            // Does nothing
            return;
        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Damageable>(out Damageable target))
            {
                _weaponEffect.Execute(target, HeldBy.GetComponent<Character>());
            }
        }

    }
}
