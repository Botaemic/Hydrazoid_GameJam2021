using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid
{
    public class ControlInstructionsCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject _instructionsPanel;

        public void CloseInstructionsPanel()
        {
            _instructionsPanel.SetActive(false);
        }
    }
}

