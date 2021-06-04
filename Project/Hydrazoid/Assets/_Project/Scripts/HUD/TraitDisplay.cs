using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid
{
    public class TraitDisplay : MonoBehaviour
    {
        [SerializeField] private TraitDisplayItem _displayItem;

        private void OnEnable()
        {
            EventList.OnRollingTraits += DisplayActiveTraits;
        }

        private void OnDisable()
        {
            EventList.OnRollingTraits -= DisplayActiveTraits;
        }

        private void DisplayActiveTraits(List<CharacterTraits.Trait> selectedTraits)
        {
            if (selectedTraits.Count > 0)
            {
                foreach (CharacterTraits.Trait trait in selectedTraits)
                {
                    TraitDisplayItem newDisplayItem = Instantiate(_displayItem, transform);

                    newDisplayItem.InstantiateTraitDisplayItem(trait);
                }
            }
        }
    }
}

