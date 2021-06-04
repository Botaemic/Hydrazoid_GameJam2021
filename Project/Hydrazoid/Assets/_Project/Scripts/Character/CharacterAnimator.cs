using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;

    protected static string DEATH = "Dead";

    protected static int _death = 0;

    protected virtual void Start()
    {
        if(_animator == null) 
        { 
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                _animator = GetComponentInChildren<Animator>();
            }
        }
        _death = Animator.StringToHash(DEATH);
    }

    public virtual void PlayDeathAnimation()
    {
        _animator.SetBool(_death, true);
    }

}
