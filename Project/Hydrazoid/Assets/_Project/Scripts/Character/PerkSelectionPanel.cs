using Hydrazoid;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Hydrazoid
{
    public class PerkSelectionPanel : MonoBehaviour
    {
        [SerializeField] private List<PerkCard> _perkCards = new List<PerkCard>();
        private List<CharacterPerks.Perk> _selectedPerks = new List<CharacterPerks.Perk>();

        private void OnEnable()
        {
            EventList.OnPerksSelection += GetPerkSelection;
            EventList.RollPerks?.Invoke();
        }

        private void OnDisable()
        {
            EventList.OnPerksSelection -= GetPerkSelection;
        }

        private void GetPerkSelection(List<CharacterPerks.Perk> selectedPerks)
        {
            _selectedPerks = selectedPerks;

            if (_selectedPerks == null)
            {
                if (_perkCards.Count > 0)
                {
                    foreach (PerkCard perkCard in _perkCards)
                    {
                        perkCard.gameObject.SetActive(false);
                    }
                }
                return;
            }

            FillPerkCards();
        }

        private void FillPerkCards()
        {
            if (_perkCards.Count > 0)
            {
                for (int i = 0; i < _perkCards.Count; i++)
                {
                    if (_selectedPerks.Count > i)
                    {
                        _perkCards[i].ReceiveCardData(_selectedPerks[i]);
                        _perkCards[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        _perkCards[i].gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                Debug.Log("No Perk Cards Found!");
            }
        }
    }
}

