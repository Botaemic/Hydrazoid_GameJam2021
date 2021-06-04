using UnityEngine;

namespace Hydrazoid.SceneManagement
{
    public class GameSceneSO : ScriptableObject
    {
        public enum GameSceneType
        {
            Location,
            Menu,
            PersistentManagers,
            Gameplay
        }

        [SerializeField] protected string _name;
        [SerializeField] [Multiline] protected string _description;
        [SerializeField] protected string _sceneName;
        [SerializeField] protected Sprite _sprite;
        
        public string Name => _name;
        public string Description => _description;
        public string SceneName => _sceneName;
        public Sprite Sprite => _sprite;
    }
}



