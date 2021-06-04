using Hydrazoid.SceneManagement;
using UnityEngine;

namespace Hydrazoid
{
    public class PlayerDeathExit: MonoBehaviour
    {
        [SerializeField] private GameSceneSO _nextSceneToLoad = null;

        [Header("Events")]
        [SerializeField] private TeleportEventSO _teleportEvent = null;
        [SerializeField] private SceneLoadEvent _sceneLoadEvent = null;


        private GameSceneSO _nextSceneToUnload = null;
        /*private void Start()
        {
            
            _teleportEvent.teleportEvent += Teleport;
        }*/

        private void OnEnable()
        {
            _teleportEvent.teleportEvent += Teleport;
        }
        private void OnDisable()
        {
            _teleportEvent.teleportEvent -= Teleport;
        }

        public void OnDeath()
        {
            _nextSceneToUnload = FindObjectOfType<LevelManager>().CurrentGameScene;
            _sceneLoadEvent?.RaiseEvent(new GameSceneSO[] { _nextSceneToLoad }, null, false);
        }

        private void Teleport(Vector3 location)
        {
            transform.position = location;
            _sceneLoadEvent?.RaiseEvent(null, new GameSceneSO[] { _nextSceneToUnload }, false);
        }
    }
}