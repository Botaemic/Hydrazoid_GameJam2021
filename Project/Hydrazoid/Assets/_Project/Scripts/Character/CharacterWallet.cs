using Hydrazoid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid
{
    public class CharacterWallet : MonoBehaviour
    {
        [SerializeField] private int _cash = 0;
        public int Cash { get => _cash; }

        private void OnEnable()
        {
            EventList.RequestCashUpdate += CashBalance;
            EventList.OnCashPickup += AddCash;
            EventList.OnCashPayment += SubtractCash;
            LoadCash();
        }

        private void OnDisable()
        {
            EventList.RequestCashUpdate -= CashBalance;
            EventList.OnCashPickup -= AddCash;
            EventList.OnCashPayment -= SubtractCash;
            SaveCash();
        }

        private void AddCash(int amount)
        {
            _cash += amount;
            EventList.OnCashChange?.Invoke(_cash);

        }

        private void SubtractCash(int amount)
        {
            _cash -= amount;
            EventList.OnCashChange?.Invoke(_cash);
        }

        public void SaveCash()
        {
            PlayerPrefs.SetInt("cash", _cash);
        }

        public void LoadCash()
        {
            _cash = PlayerPrefs.GetInt("cash");
            EventList.OnCashChange?.Invoke(_cash);
        }

        //Needs to be called when resetting character
        public void ResetCash()
        {
            PlayerPrefs.SetInt("cash", 0);
        }

        private void CashBalance()
        {
            EventList.OnCashChange?.Invoke(_cash);
        }
    }
}

