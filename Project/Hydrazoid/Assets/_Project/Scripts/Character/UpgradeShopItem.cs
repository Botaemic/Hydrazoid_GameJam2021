using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hydrazoid
{
    public class UpgradeShopItem : MonoBehaviour
    {
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _priceText;
        [SerializeField] private Text _descriptionText;
        private Button _thisButton;

        private CharacterUpgrades.Upgrade _upgrade;

        private int _cashAvailable = 0;

        private void OnEnable()
        {
            EventList.OnCashChange += UpdateCashAvailable;
        }

        private void OnDisable()
        {
            EventList.OnCashChange -= UpdateCashAvailable;
            Destroy(gameObject);
        }

        private void Awake()
        {
            _thisButton = GetComponent<Button>();
        }

        public void InstantiateShopItem (CharacterUpgrades.Upgrade upgrade)
        {
            _upgrade = upgrade;
            _nameText.text = _upgrade.UpgradeName.ToString();
            _priceText.text = _upgrade.Price.ToString();
            _descriptionText.text = _upgrade.Description;

            EventList.RequestCashUpdate?.Invoke();

            CheckUpgradeStatus();
        }

        //TODO: Need to tell the player how they can access the upgrades shop.
        //      Need to make sure resetting progress resets the upgrades properly.

        private void CheckUpgradeStatus()
        {
            

            if (_upgrade.HasUpgrade)
            {
                _thisButton.interactable = false;
                
                ColorBlock colors = _thisButton.colors;
                colors.disabledColor = new Color32(0, 0, 255, 200);

                _thisButton.colors = colors;
            }
            else if (_upgrade.Price > _cashAvailable)
            {
                _thisButton.interactable = false;

                ColorBlock colors = _thisButton.colors;
                colors.disabledColor = new Color32(255, 0, 0, 200);

                _thisButton.colors = colors;
            }
        }

        private void UpdateCashAvailable(int availableCash)
        {
            _cashAvailable = availableCash;

            CheckUpgradeStatus();
        }

        public void PurchaseUpgrade()
        {
            EventList.OnCashPayment?.Invoke(_upgrade.Price);
            _upgrade.EnableUpgrade();
            _upgrade.SaveStatus();
            EventList.OnUpgradePurchase?.Invoke();

            CheckUpgradeStatus();
        }
    }
}

