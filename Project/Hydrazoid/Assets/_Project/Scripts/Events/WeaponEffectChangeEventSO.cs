using Hydrazoid.Weapons;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Hydrazoid
{
	[CreateAssetMenu(fileName = "WeaponEffectChangeEventSO", menuName = "Events/Weapon Effect Change")]
	public class WeaponEffectChangeEventSO : ScriptableObject
	{
		public UnityAction<Dictionary<WeaponType, WeaponEffectSO>> OnWeaponEffectChanged;

		public void RaiseEvent(Dictionary<WeaponType, WeaponEffectSO> weaponEffectDictionary)
		{
			OnWeaponEffectChanged?.Invoke(weaponEffectDictionary);
		}
	}
}