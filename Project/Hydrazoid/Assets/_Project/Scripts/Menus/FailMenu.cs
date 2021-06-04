using Hydrazoid.MenuManagement;
using Hydrazoid.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid
{
    public class FailMenu : Menu
    {
        [Header("Scene Management")]
        [SerializeField] private GameSceneSO[] _scenesToload = null; //TODO easy mode change to active loaded scenes?
        [SerializeField] private GameSceneSO[] _scenesToUnload = null; //TODO easy mode change to active loaded scenes?

        [Header("Events")]
        [SerializeField] private SceneLoadEvent _sceneLoadEvent = null;

        public override void Hide(float delay)
        {
            gameObject.SetActive(false);
        }

        public override void Show(float delay)
        {
            gameObject.SetActive(true);
        }

        public void OnNewRunPressed()
        {
            _sceneLoadEvent.RaiseEvent(_scenesToload, _scenesToUnload, true);
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
}