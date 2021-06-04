using Hydrazoid.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hydrazoid
{
    //NEEDS TO BE SET AS _PERSISTENT = TRUE IN THE INSPECTOR! But needs to be destroyed when resetting character/starting new run
    public class CharacterPerks : MonoBehaviour
    {
        [System.Serializable]
        public class Perk
        {
            [SerializeField] private PerkNames _perkName;
            public PerkNames PerkName { get { return _perkName; } }

            [SerializeField] private List<PerkEffect> _statsToImpact = new List<PerkEffect>();
            public List<PerkEffect> StatsToImpact { get { return _statsToImpact; } }

            [SerializeField] private int _price;
            public int Price { get { return _price; } }

            [SerializeField] private bool _hasPerk = false;
            public bool HasPerk { get { return _hasPerk; } set { _hasPerk = value; } }

            [TextArea(1,10)]
            [SerializeField] private string _perkDescription;
            public string PerkDescription { get { return _perkDescription; } }
        }

        [System.Serializable]
        public class PerkEffect
        {
            [SerializeField] private StatNames _statToImpact;
            public StatNames StatToImpact { get { return _statToImpact; } }
            [SerializeField] private float _impactAmount;
            public float ImpactAmount { get { return _impactAmount; } }
        }

        [SerializeField] private Perk[] _perks;
        private List<Perk> _availablePerks = new List<Perk>();

        private void OnEnable()
        {
            EventList.RequestAvailablePerks += GenerateAvailablePerks;
            EventList.OnPerkChosen += ActivatePerk;
        }

        private void OnDisable()
        {
            EventList.RequestAvailablePerks -= GenerateAvailablePerks;
            EventList.OnPerkChosen -= ActivatePerk;
        }

        private void Start()
        {
            ResetPerksStatus();
        }

        private void ResetPerksStatus()
        {
            foreach (Perk perk in _perks)
            {
                perk.HasPerk = false;
            }
        }

        private void ActivatePerk(Perk perkToActivate)
        {
            foreach (Perk perk in _perks)
            {
                if (perk == perkToActivate)
                {
                    perk.HasPerk = true;
                    EventList.OnPerkActivation?.Invoke(perk);
                }
            }
        }

        private void GenerateAvailablePerks()
        {
            _availablePerks.Clear();

            foreach (Perk perk in _perks)
            {
                if (!perk.HasPerk)
                {
                    _availablePerks.Add(perk);
                }
            }

            //Send the _available perks
            EventList.OnSendingAvailablePerks?.Invoke(_availablePerks);
        }
    }
}

