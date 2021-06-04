using UnityEngine;

namespace Hydrazoid.SceneManagement
{
    public abstract class ILoadingScreen : MonoBehaviour
    {
        public abstract bool ShowLoadingScreen { set; }
        public abstract float Progress { get; set; }
    }
}