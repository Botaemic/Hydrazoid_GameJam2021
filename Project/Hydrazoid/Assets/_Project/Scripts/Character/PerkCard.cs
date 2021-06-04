using Hydrazoid;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hydrazoid
{
    public class PerkCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;

        private CharacterPerks.Perk _perk = null;

        public void ReceiveCardData(CharacterPerks.Perk perk)
        {
            _perk = perk;
            _name.text = perk.PerkName.ToString();
            _description.text = perk.PerkDescription;
        }

        public void ReturnChosenPerk()
        {
            EventList.OnPerkChosen?.Invoke(_perk);
            EventList.DeActivatePerkPanel?.Invoke();
        }
    }
}

