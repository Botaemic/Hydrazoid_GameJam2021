using UnityEngine;
using UnityEngine.Events;

namespace Hydrazoid.SceneManagement
{
    [CreateAssetMenu(fileName = "LoadGameEvent", menuName = "Game Event/Load")]
    public class SceneLoadEvent : ScriptableObject
    {
        public UnityAction<GameSceneSO[], GameSceneSO[], bool> sceneLoadEvent;
        public void RaiseEvent(GameSceneSO[] scenesToLoad, GameSceneSO[] scenesToUnload = null, bool displayLoadingScreen = false)
        {
            sceneLoadEvent?.Invoke(scenesToLoad, scenesToUnload, displayLoadingScreen);
        }
    }
}