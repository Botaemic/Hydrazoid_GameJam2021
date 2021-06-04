using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid
{
    public static class EventList
    {
        public static Action RequestTraits;
        public static Action<CharacterTraits.Trait[]> OnSendingTraits;
        public static Action<List<CharacterTraits.Trait>> OnRollingTraits;
        public static Action OnTraitsActivated;

        public static Action RequestAvailablePerks;
        public static Action<List<CharacterPerks.Perk>> OnSendingAvailablePerks;
        public static Action RollPerks;
        public static Action<List<CharacterPerks.Perk>> OnPerksSelection;
        public static Action<CharacterPerks.Perk> OnPerkChosen;
        public static Action<CharacterPerks.Perk> OnPerkActivation;

        public static Action RequestUpgrades;
        public static Action<List<CharacterUpgrades.Upgrade>> OnSendingUpgrades;
        public static Action OnUpgradePurchase;

        public static Action<int> OnMaxHealthChange;

        public static Action ActivatePerkPanel;
        public static Action DeActivatePerkPanel;

        public static Action RequestCashUpdate;
        public static Action<int> OnCashChange;
        public static Action<int> OnCashPickup;
        public static Action<int> OnCashPayment;

        public static Action<Vector3, int> OnKill;
    }
}
