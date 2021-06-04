using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid
{
    public class UpgradeShopCanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject _shopWindow;

        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }

        private void Start()
        {
            _shopWindow.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                //Enable or disable shop window
                if (_shopWindow.activeInHierarchy)
                {
                    CloseShopWindow();
                }
                else
                {
                    OpenShopWindow();
                }
            }
        }

        public void OpenShopWindow()
        {
            _shopWindow.SetActive(true);
            Time.timeScale = 0;
        }

        public void CloseShopWindow()
        {
            _shopWindow.SetActive(false);
            Time.timeScale = 1;
        }
    }
}

