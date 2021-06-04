using Hydrazoid.MenuManagement;
using Hydrazoid.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Hydrazoid
{
    public class MainMenu : Menu
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

        public void OnPlayPressed()
        {
            _sceneLoadEvent.RaiseEvent(_scenesToload, _scenesToUnload, true);
        }

        public void OnResetPressed()
        {
            //Reset all the saved info from wallet en upgrades.
            string[] upgradeNames = Enum.GetNames(typeof(UpgradeNames));

            foreach (string name in upgradeNames)
            {
                PlayerPrefs.SetInt(name, 0);
            }

            PlayerPrefs.SetInt("cash", 0);
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