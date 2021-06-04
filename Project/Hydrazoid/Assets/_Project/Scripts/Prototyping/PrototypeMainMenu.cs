using Hydrazoid.MenuManagement;
using Hydrazoid.SceneManagement;
using UnityEngine;

public class PrototypeMainMenu : Menu
{
    [SerializeField] private SceneLoadEvent _sceneLoadEvent = null;
    [Header("Scene Management")]
    [SerializeField] private GameSceneSO[] _scenesToUnload = null; //TODO easy mode change to active loaded scenes?

    public override void Hide(float delay)
    {
        gameObject.SetActive(false);
    }

    public override void Show(float delay)
    {
        gameObject.SetActive(true);
    }

    public void OnCharacterControlScenePressed(GameSceneSO loadScene)
    {
        _sceneLoadEvent.RaiseEvent(new GameSceneSO[] { loadScene }, _scenesToUnload, true);
    }

    public void OnExitPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
