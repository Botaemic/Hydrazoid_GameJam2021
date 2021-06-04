using Hydrazoid.SceneManagement;
using Hydrazoid.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hydrazoid
{
    //quick dirty class for keeping track on which level we playing
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameSceneSO _gameScene;
        [SerializeField] private VoidEventChannelSO _voidEvent;
        public GameSceneSO CurrentGameScene => _gameScene;

        private void Start()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_gameScene.SceneName));
            _voidEvent.RaiseEvent();
        }
    }
}