using UnityEngine;

namespace Hydrazoid.MenuManagement
{
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField] protected MenuOpenCloseEvent _menuOpenCloseEvent;

        public abstract void Show(float delay);

        public abstract void Hide(float delay);

        public virtual void OpenMenu(Menu menu)
        {
            _menuOpenCloseEvent.RaiseEvent(new Menu[] { menu }, new Menu[] { null });
        }

        public virtual void CloseMenu(Menu menu)
        {
            _menuOpenCloseEvent.RaiseEvent(new Menu[] { null }, new Menu[] { menu });
        }
    }
}