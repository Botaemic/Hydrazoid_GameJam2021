using Hydrazoid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid
{
    public class PerkCanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject _perkPanel;

        private void OnEnable()
        {
            EventList.ActivatePerkPanel += ActivatePerkPanel;
            EventList.DeActivatePerkPanel += DeActivatePerkPanel;
        }

        private void OnDisable()
        {
            EventList.ActivatePerkPanel -= ActivatePerkPanel;
            EventList.DeActivatePerkPanel -= DeActivatePerkPanel;
        }

        private void Start()
        {
            ActivatePerkPanel();
        }

        private void Update()
        {
            //THIS IS PURELY FOR TESTING. NEEDS TO BE REMOVED!!!!
            /*if (Input.GetKeyDown(KeyCode.G))
            {
                if (_perkPanel.activeInHierarchy)
                {
                    DeActivatePerkPanel();
                }
                else
                {
                    ActivatePerkPanel();
                }
            }*/
        }

        private void ActivatePerkPanel()
        {
            _perkPanel.SetActive(true);
            Time.timeScale = 0;
        }

        private void DeActivatePerkPanel()
        {
            _perkPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}

