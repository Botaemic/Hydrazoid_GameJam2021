using UnityEngine;

namespace Hydrazoid.Weapons
{
    [CreateAssetMenu(fileName = "NewDamageEffect", menuName = "Weapons/Damage Effect")]
    public class DamageEffectSO : WeaponEffectSO
    {
        [SerializeField] private float _staticDamageMultiplier = 1f;
        public override void Execute(Damageable target, Character weaponOwner)
        {
            target.TakeDamage(weaponOwner.RangedDamage * _staticDamageMultiplier);       
        }
    }
}