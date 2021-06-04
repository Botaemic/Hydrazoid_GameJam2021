using Hydrazoid.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hydrazoid
{
    public class SceneExit : MonoBehaviour
    {
        [SerializeField] private GameSceneSO _sceneToUnload = null;
        [SerializeField] private GameSceneSO _nextSceneToLoad = null;
        [SerializeField] private TeleportEventSO _teleportEvent = null;

        [SerializeField] private SceneLoadEvent _sceneLoadEvent = null;


        private void Start()
        {
            _teleportEvent.teleportEvent += Teleport;
        }

        private void OnDisable()
        {
            _teleportEvent.teleportEvent -= Teleport;
        }

        private void Teleport(Vector3 location)
        {
            _sceneLoadEvent?.RaiseEvent(null, new GameSceneSO[] {_sceneToUnload}, false);
        }

        public void Transport()
        {
            _sceneLoadEvent?.RaiseEvent(new GameSceneSO[] { _nextSceneToLoad }, null, false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCharacter player))
            {
                Transport();
            }
        }
    }
}