using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hydrazoid
{
    public class TraitDisplayItem : MonoBehaviour
    {
        private Text _nameText;

        private void Awake()
        {
            _nameText = GetComponent<Text>();
        }

        public void InstantiateTraitDisplayItem(CharacterTraits.Trait trait)
        {
            _nameText.text = "  - " + trait.TraitName.ToString();
        }
    }
}

