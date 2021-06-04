using UnityEngine;

namespace Hydrazoid.Weapons
{
    [CreateAssetMenu(fileName = "NewGrowEffect", menuName = "Weapons/Grow Effect")]
    public class GrowEffectSO : WeaponEffectSO
    {
        [SerializeField] private float _growSpeed = 1f;
        [SerializeField] private float _growMultiplier = 1.5f;
        
        public override void Execute(Damageable target, Character weaponOwner)
        {

           // obj.transform.localScale = obj.transform.localScale * _growMultiplier;

        }
    }
}