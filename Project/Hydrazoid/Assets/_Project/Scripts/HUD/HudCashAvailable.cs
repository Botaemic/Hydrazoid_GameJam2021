using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hydrazoid
{
    public class HudCashAvailable : MonoBehaviour
    {
        private Text _cashText;

        private void OnEnable()
        {
            EventList.OnCashChange += UpdateCashDisplay;
        }

        private void OnDisable()
        {
            EventList.OnCashChange -= UpdateCashDisplay;
        }

        private void Awake()
        {
            _cashText = GetComponent<Text>();
            EventList.RequestCashUpdate?.Invoke();
        }

        private void UpdateCashDisplay(int amount)
        {
            _cashText.text = amount.ToString();
        }
    }
}

