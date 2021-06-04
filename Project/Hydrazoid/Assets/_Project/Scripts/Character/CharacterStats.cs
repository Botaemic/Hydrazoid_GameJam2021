using Hydrazoid.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid
{
    public class CharacterStats : MonoBehaviour
    {
        #region Starting Stats
        //Starting Stats
        private const int HEALTH = 100;
        private const int ARMOR = 0;
        private const float MOVEMENTSPEED = 10f;
        private const float ATTACKRANGEMELEE = 1f;
        private const float ATTACKRANGERANGED = 10f;
        private const float ACCURACY = 50f;
        private const float CRITCHANCE = 15f;
        private const float DAMAGE = 1f;
        private const float DAMAGEMELEE = 1f;
        private const float DAMAGERANGED = 1f;
        private const float VISIONRANGESTART = 0f;
        private const float VISIONRANGEEND = 10f;
        private const float DODGECHANCE = 10f;
        private const int HEALTHREGENOVERTIME = 0;
        private const int MAXAMMOCAPACITY = 500;
        private const float AMMOPICKUPBONUS = 1f;
        private const float CASHGAINMODIFIER = 0f;
        #endregion

        #region Run Stats
        //Run Stats
        private int _maxHealth;
        public int MaxHealth { get => _maxHealth; }
        private int _armor;
        public int Armor { get => _armor; }
        private float _movementSpeed;
        public float MovementSpeed { get => _movementSpeed; }
        private float _attackRangeMelee;
        public float AttackRangeMelee { get => _attackRangeMelee; }
        private float _attackRangeRanged;
        public float AttackRangeRanged { get => _attackRangeRanged; }
        private float _accuracy;
        public float Accuracy { get => _accuracy; }
        private float _critChance;
        public float CritChance { get => _critChance; }
        private float _damage;
        public float Damage { get => _damage; }
        private float _damageMelee;
        public float DamageMelee { get => _damageMelee; }
        private float _damageRanged;
        public float DamageRanged { get => _damageRanged; }
        private float _visionRangeStart;
        public float VisionRangeStart { get => _visionRangeStart; }
        private float _visionRangeEnd;
        public float VisionRangeEnd { get => _visionRangeEnd; }
        private float _dodgeChance;
        public float DodgeChance { get => _dodgeChance; }
        private int _healthRegenOverTime;
        public int HealthRegenOverTime { get => _healthRegenOverTime; }
        private int _maxAmmoCapacity;
        public int MaxAmmoCapacity { get => _maxAmmoCapacity; }
        private float _ammoPickupBonus;
        public float AmmoPickupBonus { get => _ammoPickupBonus; }
        private float _cashGainModifier;
        public float CashGainModifier { get => _cashGainModifier; }
        #endregion

        //Conditions
        private bool _oily = false;
        public bool Oily { get => _oily; }

        #region References
        private CharacterPerks _characterPerks;
        private CharacterTraits _characterTraits;
        private CharacterUpgrades _characterUpgrades;
        #endregion

        private void Awake()
        {
            _characterPerks = GetComponent<CharacterPerks>();
            _characterTraits = GetComponent<CharacterTraits>();
            _characterUpgrades = GetComponent<CharacterUpgrades>();
        }

        private void OnEnable()
        {
            EventList.OnTraitsActivated += InitializeStatsForRun;
            EventList.OnUpgradePurchase += InitializeStatsForRun;
            EventList.OnPerkActivation += OnPerkSelection;
        }

        private void OnDisable()
        {
            EventList.OnTraitsActivated -= InitializeStatsForRun;
            EventList.OnUpgradePurchase -= InitializeStatsForRun;
            EventList.OnPerkActivation -= OnPerkSelection;
        }

        private void Start()
        {
            InitializeStatsForRun();
        }

        private void InitializeStatsForRun()
        {
            //Need to make sure Upgrades are loaded
            _characterUpgrades.LoadUpgradeStatus();

            _maxHealth = HEALTH + Mathf.RoundToInt(_characterUpgrades.ReturnUpgradeEffect(StatNames.Health)) + Mathf.RoundToInt(_characterTraits.ReturnTraitEffect(StatNames.Health)); //Add upgrades and traits
            EventList.OnMaxHealthChange?.Invoke(MaxHealth);
            _armor = ARMOR + Mathf.RoundToInt(_characterUpgrades.ReturnUpgradeEffect(StatNames.Armor)) + Mathf.RoundToInt(_characterTraits.ReturnTraitEffect(StatNames.Armor)); //Add upgrades and traits
            _movementSpeed = MOVEMENTSPEED + _characterUpgrades.ReturnUpgradeEffect(StatNames.MovementSpeed) + _characterTraits.ReturnTraitEffect(StatNames.MovementSpeed); //Add upgrades and traits
            _attackRangeMelee = ATTACKRANGEMELEE + _characterUpgrades.ReturnUpgradeEffect(StatNames.AttackRangeMelee) + _characterTraits.ReturnTraitEffect(StatNames.AttackRangeMelee); //Add upgrades and traits
            _attackRangeRanged = ATTACKRANGERANGED + _characterUpgrades.ReturnUpgradeEffect(StatNames.AttackRangeRanged) + _characterTraits.ReturnTraitEffect(StatNames.AttackRangeRanged); //Add upgrades and traits
            _accuracy = ACCURACY + _characterUpgrades.ReturnUpgradeEffect(StatNames.Accuracy) + _characterTraits.ReturnTraitEffect(StatNames.Accuracy); //Add upgrades and traits
            _critChance = CRITCHANCE + _characterUpgrades.ReturnUpgradeEffect(StatNames.CritChance) + _characterTraits.ReturnTraitEffect(StatNames.CritChance); //Add upgrades and traits
            _damage = DAMAGE + _characterUpgrades.ReturnUpgradeEffect(StatNames.Damage) + _characterTraits.ReturnTraitEffect(StatNames.Damage); //Add upgrades and traits
            _damageMelee = DAMAGEMELEE + _characterUpgrades.ReturnUpgradeEffect(StatNames.DamageMelee) + _characterTraits.ReturnTraitEffect(StatNames.DamageMelee); //Add upgrades and traits
            _damageRanged = DAMAGERANGED + _characterUpgrades.ReturnUpgradeEffect(StatNames.DamageRanged) + _characterTraits.ReturnTraitEffect(StatNames.DamageRanged); //Add upgrades and traits
            _visionRangeStart = VISIONRANGESTART + _characterUpgrades.ReturnUpgradeEffect(StatNames.VisionRangeStart) + _characterTraits.ReturnTraitEffect(StatNames.VisionRangeStart); //Add traits
            _visionRangeEnd = VISIONRANGEEND + _characterUpgrades.ReturnUpgradeEffect(StatNames.VisionRangeEnd) + _characterTraits.ReturnTraitEffect(StatNames.VisionRangeEnd); //Add upgrades and traits
            _dodgeChance = DODGECHANCE + _characterUpgrades.ReturnUpgradeEffect(StatNames.DodgeChance) + _characterTraits.ReturnTraitEffect(StatNames.DodgeChance); //Add upgrades and traits
            _healthRegenOverTime = HEALTHREGENOVERTIME + Mathf.RoundToInt(_characterUpgrades.ReturnUpgradeEffect(StatNames.HealthRegenOverTime)) + Mathf.RoundToInt(_characterTraits.ReturnTraitEffect(StatNames.HealthRegenOverTime)); //Add upgrades and traits
            _maxAmmoCapacity = MAXAMMOCAPACITY + Mathf.RoundToInt(_characterUpgrades.ReturnUpgradeEffect(StatNames.MaxAmmoCapacity)) + Mathf.RoundToInt(_characterTraits.ReturnTraitEffect(StatNames.MaxAmmoCapacity)); //Add upgrades and traits
            _ammoPickupBonus = AMMOPICKUPBONUS + _characterUpgrades.ReturnUpgradeEffect(StatNames.AmmoPickupBonus) + _characterTraits.ReturnTraitEffect(StatNames.AmmoPickupBonus); //Add upgrades and traits
            _cashGainModifier = CASHGAINMODIFIER + _characterUpgrades.ReturnUpgradeEffect(StatNames.CashGainModifier) + _characterTraits.ReturnTraitEffect(StatNames.CashGainModifier); //Add upgrades and traits
        }  

        private void OnPerkSelection(CharacterPerks.Perk perk)
        {
            switch (perk.PerkName)
            {
                case PerkNames.Drunk:
                    Debug.Log("Drunk Perk Now Activated!");
                    //Vision needs to be made blurry
                    break;
                case PerkNames.Shield:
                    //If implemented then right mouse button will need to enable shield
                    break;
                case PerkNames.OiledUp:
                    //Weapon need to get a random chance to slip from the players hands upon attack
                    _oily = true;
                    break;
                case PerkNames.Grenades:
                    //Unlimited supply of grenades need to be enabled
                    break;
                case PerkNames.LizardSkin:
                    //Deactivate the oiled up perk. So take away the dodge bonus and the random chance of the weapon slipping for the players hands
                    if (Oily)
                    {
                        _oily = false;
                        //Take away dodge bonus.
                    }
                    break;
                case PerkNames.Bandolier:
                    //Nothing to do here at the moment
                    break;
                case PerkNames.Speed:
                    //Nothing to do here at the moment
                    break;
                case PerkNames.Glasses:
                    //Nothing to do here at the moment (Just make sure that Vision Range Start and Vision Range End are set to precise numbers)
                    break;
                case PerkNames.Hamburger:
                    //Scale the belly up
                    break;
                case PerkNames.Diet:
                    //Scale the belly down
                    break;
                default:
                    break;
            }

            if (perk.StatsToImpact.Count > 0)
            {
                foreach (CharacterPerks.PerkEffect perkEffect in perk.StatsToImpact)
                {
                    switch (perkEffect.StatToImpact)
                    {
                        case StatNames.Health:

                            _maxHealth += Mathf.RoundToInt(perkEffect.ImpactAmount);

                            EventList.OnMaxHealthChange?.Invoke(MaxHealth);

                            break;
                        case StatNames.Armor:
                            _armor += Mathf.RoundToInt(perkEffect.ImpactAmount);
                            break;
                        case StatNames.MovementSpeed:
                            _movementSpeed *= (1 + perkEffect.ImpactAmount);
                            break;
                        case StatNames.AttackRangeMelee:
                            _attackRangeMelee += perkEffect.ImpactAmount;
                            break;
                        case StatNames.AttackRangeRanged:
                            _attackRangeRanged += perkEffect.ImpactAmount;
                            break;
                        case StatNames.Accuracy:
                            _accuracy += perkEffect.ImpactAmount;
                            break;
                        case StatNames.CritChance:
                            _critChance += perkEffect.ImpactAmount;
                            break;
                        case StatNames.Damage:
                            _damage += perkEffect.ImpactAmount;
                            break;
                        case StatNames.DamageMelee:
                            _damageMelee += perkEffect.ImpactAmount;
                            break;
                        case StatNames.DamageRanged:
                            _damageRanged += perkEffect.ImpactAmount;
                            break;
                        case StatNames.VisionRangeStart:
                            _visionRangeStart += perkEffect.ImpactAmount;
                            break;
                        case StatNames.VisionRangeEnd:
                            _visionRangeEnd += perkEffect.ImpactAmount;
                            break;
                        case StatNames.DodgeChance:
                            _dodgeChance += perkEffect.ImpactAmount;
                            break;
                        case StatNames.HealthRegenOverTime:
                            _healthRegenOverTime += Mathf.RoundToInt(perkEffect.ImpactAmount);
                            break;
                        case StatNames.MaxAmmoCapacity:
                            _maxAmmoCapacity += Mathf.RoundToInt(perkEffect.ImpactAmount);
                            break;
                        case StatNames.AmmoPickupBonus:
                            _ammoPickupBonus += perkEffect.ImpactAmount;
                            break;
                        case StatNames.CashGainModifier:
                            _cashGainModifier += perkEffect.ImpactAmount;
                            break;
                        default:
                            break;
                    }
                }
            }

        }
    }
}