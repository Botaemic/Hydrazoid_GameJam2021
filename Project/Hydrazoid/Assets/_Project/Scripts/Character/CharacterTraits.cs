using Hydrazoid.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid
{
    //NEEDS TO BE SET AS _PERSISTENT = TRUE IN THE INSPECTOR! But needs to be destroyed when resetting character/starting new run
    public class CharacterTraits : MonoBehaviour
    {
        [System.Serializable]
        public class Trait
        {
            [SerializeField] private TraitNames _traitName;
            public TraitNames TraitName { get { return _traitName; } }

            [SerializeField] private List<TraitEffect> _statsToImpact = new List<TraitEffect>();
            public List<TraitEffect> StatsToImpact { get { return _statsToImpact; } }

            [SerializeField] private List<TraitNames> _incompatibleWith = new List<TraitNames>();
            public List<TraitNames> IncompatibleWith { get { return _incompatibleWith; } }

            [SerializeField] private bool _hasTrait = false;
            public bool HasTrait { get { return _hasTrait; } set { _hasTrait = value; } }

            [TextArea(1, 10)]
            [SerializeField] private string _traitDescription;
            public string TraitDescription { get { return _traitDescription; } }
        }

        [System.Serializable]
        public class TraitEffect
        {
            [SerializeField] private StatNames _statToImpact;
            public StatNames StatToImpact { get { return _statToImpact; } }
            [SerializeField] private float _impactAmount;
            public float ImpactAmount { get { return _impactAmount; } }
        }

        [SerializeField] private Trait[] _traits;
        private List<Trait> _activeTraits = new List<Trait>();

        private void OnEnable()
        {
            EventList.RequestTraits += SendTraits;
            EventList.OnRollingTraits += ActiveTraits;
        }

        private void OnDisable()
        {
            EventList.RequestTraits -= SendTraits;
            EventList.OnRollingTraits -= ActiveTraits;
        }

        private void SendTraits()
        {
            EventList.OnSendingTraits?.Invoke(_traits);
        }

        private void ActiveTraits(List<Trait> selectedTraits)
        {
            foreach (Trait trait in _traits)
            {
                trait.HasTrait = false;
            }

            _activeTraits = selectedTraits;

            foreach (Trait trait in _activeTraits)
            {
                trait.HasTrait = true;
            }

            EventList.OnTraitsActivated?.Invoke();
        }

        public float ReturnTraitEffect(StatNames statToImpactRequest)
        {
            float combinedTraitImpact = 0;

            foreach (Trait trait in _activeTraits)
            {
                if (trait.HasTrait)
                {
                    foreach (TraitEffect traitEffect in trait.StatsToImpact)
                    {
                        if (statToImpactRequest == traitEffect.StatToImpact)
                        {
                            combinedTraitImpact += traitEffect.ImpactAmount;
                        }
                    }
                }
            }

            return combinedTraitImpact;
        }
    }
}


