using UnityEngine;

namespace Hydrazoid
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] private Health _health = null;

        void Start()
        {
            _health = GetComponent<Health>();
        }

        public void TakeDamage(float damage)
        {
            _health.DecreaseHealthAmount(damage);
        }
    }
}