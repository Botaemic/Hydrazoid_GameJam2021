using Hydrazoid;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hydrazoid
{
    public class PerkSelectionSpawner : MonoBehaviour
    {
        private int _amountOfPerksToRoll = 3;

        private List<CharacterPerks.Perk> _availablePerksList = new List<CharacterPerks.Perk>();
        private List<CharacterPerks.Perk> _selectedPerks = new List<CharacterPerks.Perk>();

        // Perk Selector needs to roll for 3 perks to be selected from an available perks list.
        // Those perks need to then be displayed so the player can select one.
        // The selected perk then needs to be activated and, in some cases, a perk needs to be deactivated.
        // The selected perk also needs to be removed from the available perks list and, in some cases, a perk needs to be readded to the available perk list.

        private void OnEnable()
        {
            EventList.OnSendingAvailablePerks += ReceiveAvailablePerks;
            EventList.RollPerks += OnRollRequest;
        }

        private void OnDisable()
        {
            EventList.OnSendingAvailablePerks -= ReceiveAvailablePerks;
            EventList.RollPerks -= OnRollRequest;
        }

        private void ReceiveAvailablePerks(List<CharacterPerks.Perk> perks)
        {
            _availablePerksList = perks;
        }

        private void OnRollRequest()
        {
            EventList.RequestAvailablePerks?.Invoke();

            EventList.OnPerksSelection(RollPerkSelection());
        }

        private List<CharacterPerks.Perk> RollPerkSelection()
        {
            if (_availablePerksList.Count > 0)
            {
                _selectedPerks.Clear();

                for (int i = 0; i < _amountOfPerksToRoll; i++)
                {
                    if (_availablePerksList.Count > 0)
                    {
                        int roll = Random.Range(0, _availablePerksList.Count);
                        _selectedPerks.Add(_availablePerksList[roll]);
                        _availablePerksList.Remove(_selectedPerks[i]);
                    }
                }

                _availablePerksList.Clear();

                return _selectedPerks;
            }

            return null;
        }
    }

}
