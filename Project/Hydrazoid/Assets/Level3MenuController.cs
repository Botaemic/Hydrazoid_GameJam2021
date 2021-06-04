using Hydrazoid.MenuManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hydrazoid
{
    public class Level3MenuController : MonoBehaviour
    {
        [SerializeField] private Menu _succesMenu = null;
        [SerializeField] private Menu _failMenu = null;

        private void Start()
        {
            
            if(GameManager.Instance.Player.Dead)
            {
                _failMenu.Show(0f);
            }
            else
            {
                _succesMenu.Show(0f);
            }
        }
    }
}