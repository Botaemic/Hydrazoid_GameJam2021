using Hydrazoid.Extensions;
using Hydrazoid.SceneManagement;
using UnityEngine;

namespace Hydrazoid
{

    //Due to some perfomrance glitches this has an timer added
    //TODO find better solution
    public class SceneEntrance : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint = null;
        [SerializeField] private TeleportEventSO _teleportEvent = null;
        [SerializeField] private float _maximumRange = 20f;

        private float _timer = 0f;
        private bool _triggered = false;
        private void Start()
        {
            //FindObjectOfType<PlayerCharacter>().transform.position = _spawnPoint.position; // too slow
            GameManager.Instance.Player.transform.position = _spawnPoint.position;
            _teleportEvent.RaiseEvent(_spawnPoint.position);
        }

        private void Update()
        {
            
            _timer += Time.deltaTime;
            if (_timer < 1f && !_triggered && !IsPlayerTeleported())
            {
                GameManager.Instance.Player.transform.position = _spawnPoint.position;
                _teleportEvent.RaiseEvent(_spawnPoint.position);
                _triggered = true;
            }
        }

        private bool IsPlayerTeleported()
        {
            
            return GameManager.Instance.Player.transform.position.DistanceTo(transform) < _maximumRange; 
        }
    }
}