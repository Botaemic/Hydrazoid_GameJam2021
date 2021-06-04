using UnityEngine;
using UnityEngine.Events;

namespace Hydrazoid
{
    [CreateAssetMenu(fileName = "TeleportEvent", menuName = "Teleporter/Event")]
    public class TeleportEventSO : ScriptableObject
    {
        public UnityAction<Vector3> teleportEvent;
        public void RaiseEvent(Vector3 transportLocation)
        {
            teleportEvent?.Invoke(transportLocation);
        }
    }
}