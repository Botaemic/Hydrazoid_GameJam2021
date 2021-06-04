using Hydrazoid.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace Hydrazoid
{
    [CreateAssetMenu(fileName = "WeaponDropEventSO", menuName = "Events/Weapon Drop")]
    public class WeaponDropEventSO : ScriptableObject
    {
        public UnityAction<WeaponBase, Vector3> OnWeaponDrop;

        public void RaiseEvent(WeaponBase weapon, Vector3 position)
        {
            OnWeaponDrop?.Invoke(weapon, position);
        }
    }
}