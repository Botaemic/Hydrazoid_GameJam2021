using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hydrazoid
{
    public class UpgradeShop : MonoBehaviour
    {
        [SerializeField] private UpgradeShopItem _shopItem;
        [SerializeField] private Text _cashAvailable;

        private void OnEnable()
        {
            EventList.OnSendingUpgrades += GetUpgradesList;
            EventList.OnCashChange += CashAvailable;
            EventList.RequestUpgrades?.Invoke();
        }

        private void OnDisable()
        {
            EventList.OnSendingUpgrades -= GetUpgradesList;
            EventList.OnCashChange -= CashAvailable;
        }

        private void GetUpgradesList(List<CharacterUpgrades.Upgrade> upgradeList)
        {
            Debug.Log("List Received");
            foreach (CharacterUpgrades.Upgrade upgrade in upgradeList)
            {
                //Create a shopitem and child it under the upgradeshop
                UpgradeShopItem newShopItem = Instantiate(_shopItem, transform);

                //Pass on the upgrade name and the upgrade price to the new shop item
                newShopItem.InstantiateShopItem(upgrade);
            }
        }

        private void CashAvailable(int amount)
        {
            _cashAvailable.text = $"Cash: {amount}";
        }
    }
}

