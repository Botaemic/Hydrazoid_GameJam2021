using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hydrazoid
{
    public class NewRunTraitSpawner : MonoBehaviour
    {
        private CharacterTraits.Trait[] _traitArray;
        private List<CharacterTraits.Trait> _traitList;
        [SerializeField]private List<CharacterTraits.Trait> _selectedTraits = new List<CharacterTraits.Trait>();

        private void OnEnable()
        {
            EventList.OnSendingTraits += ReceiveTraits;
        }

        private void OnDisable()
        {
            EventList.OnSendingTraits -= ReceiveTraits;
        }

        private void Start()
        {
            EventList.RequestTraits?.Invoke();
            EventList.OnRollingTraits?.Invoke(RollCharacterTraits());
        }

        private void ReceiveTraits(CharacterTraits.Trait[] traitArray)
        {
            _traitArray = traitArray;
        }

        public List<CharacterTraits.Trait> RollCharacterTraits()
        {
            _traitList = _traitArray.ToList();

            int timesToRoll = Random.Range(0, (_traitArray.Length / 2) + 1);
            //Debug.Log($"Times to Roll: {timesToRoll}");

            for (int i = 0; i < timesToRoll; i++)
            {
                int roll = Random.Range(0, _traitList.Count);
                //Debug.Log($"{i + 1} Rolled {roll}");

                _selectedTraits.Add(_traitList[roll]);
                //Debug.Log($"Winning trait: {_traitList[roll].TraitName}");

                //Take note of the winning TraitName
                TraitNames traitToRemove = _traitList[roll].TraitName;

                //Remove the incompatible traits first
                if (_traitList[roll].IncompatibleWith.Count > 0)
                {
                    foreach (TraitNames traitName in _traitList[roll].IncompatibleWith)
                    {
                        for (int index = _traitList.Count - 1; index >= 0; index--)
                        {
                            if (_traitList[index].TraitName == traitName)
                            {
                                _traitList.Remove(_traitList[index]);
                            }
                        }
                    }
                }

                //Now remove the winning trait
                foreach (CharacterTraits.Trait trait in _traitList)
                {
                    if (trait.TraitName == traitToRemove)
                    {
                        _traitList.Remove(trait);
                        break;
                    }
                }

                //foreach (CharacterTraits.Trait trait in _traitList)
                //{
                //    Debug.Log($"Still in _traitList: {trait.TraitName}");
                //}
            }

            //foreach (CharacterTraits.Trait trait in _selectedTraits)
            //{
            //    Debug.Log($"In the final selected list: {trait.TraitName}");
            //}

            return _selectedTraits;
        }
    }
}

