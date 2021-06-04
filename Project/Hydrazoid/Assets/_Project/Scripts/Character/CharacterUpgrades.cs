using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hydrazoid
{
    public class CharacterUpgrades : MonoBehaviour
    {
        [System.Serializable]
        public class Upgrade
        {
            [SerializeField] private UpgradeNames _upgradeName;
            public UpgradeNames UpgradeName { get { return _upgradeName; } }

            [SerializeField] private StatNames _statToImpact;
            public StatNames StatToImpact { get { return _statToImpact; } }

            [SerializeField] private float _impactAmount;
            public float ImpactAmount { get { return _impactAmount; } }

            [SerializeField] private int _price;
            public int Price { get { return _price; } }

            [TextArea(1, 10)]
            [SerializeField] private string _description;
            public string Description { get { return _description; } }

            [SerializeField] private bool _hasUpgrade = false;
            public bool HasUpgrade { get { return _hasUpgrade; } }

            public void SaveStatus()
            {
                PlayerPrefs.SetInt(UpgradeName.ToString(), Convert.ToInt32(HasUpgrade));
            }

            public void LoadStatus()
            {
                _hasUpgrade = Convert.ToBoolean(PlayerPrefs.GetInt(_upgradeName.ToString()));
            }

            public void ResetSatus()
            {
                PlayerPrefs.SetInt(UpgradeName.ToString(), 0);
            }

            public void EnableUpgrade()
            {
                _hasUpgrade = true;
            }
        }

        //This class needs to load from and save to playerprefs. Needs to reset the playerprefs when character progress is reset.
        [SerializeField] private Upgrade[] _upgrades;
        private List<Upgrade> _upgradesList = new List<Upgrade>();
        //private List<Upgrade> _availableUpgrades = new List<Upgrade>();

        private void OnEnable()
        {
            EventList.RequestUpgrades += GenerateUpgradesList;
        }

        private void OnDisable()
        {
            EventList.RequestUpgrades -= GenerateUpgradesList;
            SaveUpgradeStatus();
        }

        public float ReturnUpgradeEffect(StatNames statToImpactRequest)
        {
            float combinedUpgradeImpact = 0;

            foreach (Upgrade upgrade in _upgrades)
            {
                if (statToImpactRequest == upgrade.StatToImpact && upgrade.HasUpgrade)
                {
                    combinedUpgradeImpact += upgrade.ImpactAmount;
                }
            }
            return combinedUpgradeImpact;
        }

        //Needs to be triggered after purchasing an upgrade
        public void SaveUpgradeStatus()
        {
            foreach (Upgrade upgrade in _upgrades)
            {
                upgrade.SaveStatus();
            }
        }

        //Needs to be triggered prior to upgrade effects being requested (at the start of every run at least)
        public void LoadUpgradeStatus()
        {
            foreach (Upgrade upgrade in _upgrades)
            {
                upgrade.LoadStatus();
            }
        }

        //Needs to be triggered when resetting character progress (new game, not new run)
        public void ResetUpgradeStatus()
        {
            foreach (Upgrade upgrade in _upgrades)
            {
                upgrade.ResetSatus();
            }
        }

        /*private void GenerateAvailableUpgradesList()
        {
            _availableUpgrades.Clear();

            foreach (Upgrade upgrade in _upgrades)
            {
                if (!upgrade.HasUpgrade)
                {
                    _availableUpgrades.Add(upgrade);
                }
            }

            EventList.OnSendingAvailableUpgrades?.Invoke(_availableUpgrades);
        }*/

        private void GenerateUpgradesList()
        {
            Debug.Log("Request for Upgrade list received");

            _upgradesList.Clear();
            _upgradesList = _upgrades.ToList();

            EventList.OnSendingUpgrades?.Invoke(_upgradesList);
        }
    }
}
