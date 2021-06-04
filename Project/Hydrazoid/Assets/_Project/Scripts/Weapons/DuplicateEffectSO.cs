using UnityEngine;
using UnityEngine.AI;

namespace Hydrazoid.Weapons
{
    [CreateAssetMenu(fileName = "NewDuplicateEffect", menuName = "Weapons/Duplicate Effect")]
    public class DuplicateEffectSO : WeaponEffectSO
    {
        [SerializeField] private float _distance = 1f;
        public override void Execute(Damageable target, Character weaponOwner)
        {
            if (RandomPoint(target.transform.position, _distance, out Vector3 position))
            {
                GameObject go = Instantiate(target.gameObject, position, Quaternion.identity);
            }

            //TODO grow effect on duplicate.
        }


        //https://docs.unity3d.com/540/Documentation/ScriptReference/NavMesh.SamplePosition.html
        private bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector3 randomPoint = center + Random.insideUnitSphere * range;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }
            }
            result = Vector3.zero;
            return false;
        }

    }
}