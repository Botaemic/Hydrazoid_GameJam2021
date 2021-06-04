using UnityEngine;


namespace Hydrazoid
{
    public class TimeOut : MonoBehaviour
    {
        void Start()
        {
            Destroy(gameObject, 2f);
        }

    }
}