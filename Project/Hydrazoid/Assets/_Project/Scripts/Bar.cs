using UnityEngine;

namespace Hydrazoid
{
    public abstract class Bar :  MonoBehaviour
    {
        protected SingleStat _stat = null;

        public virtual void Initialize(SingleStat newStat)
        {
            this._stat = newStat;
            this._stat.OnStatChanged += UpdateBar;
        }

        protected abstract void UpdateBar();

        protected virtual void OnEnable()
        {
            if (_stat != null)
            {
                this._stat.OnStatChanged += UpdateBar;
            }   
        }

        protected virtual void OnDisable()
        {
            this._stat.OnStatChanged -= UpdateBar;
        }
    }
}
