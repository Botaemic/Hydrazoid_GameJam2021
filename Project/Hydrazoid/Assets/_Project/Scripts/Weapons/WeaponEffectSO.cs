using UnityEngine;

namespace Hydrazoid.Weapons
{
    public abstract class WeaponEffectSO : ScriptableObject 
    {
        public abstract void Execute(Damageable target, Character weaponOwner);
    }
}