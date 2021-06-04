using UnityEngine;
using UnityEngine.Events;

namespace Hydrazoid.MenuManagement
{
    [CreateAssetMenu(fileName = "MenuOpenCloseEvent", menuName = "Menu Event/OpenClose")]
    public class MenuOpenCloseEvent : ScriptableObject
    {
        public UnityAction<Menu[], Menu[]> openCloseEvent;
        public void RaiseEvent(Menu[] menusToOpen, Menu[] menusToClose = null)
        {
            openCloseEvent.Invoke(menusToOpen, menusToClose);
        }
    }
}