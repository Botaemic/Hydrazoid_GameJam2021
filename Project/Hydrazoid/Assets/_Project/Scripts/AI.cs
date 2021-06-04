using Hydrazoid.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hydrazoid
{

    public class AI : Character
    {
        //TODO raycast for visual
        //TODO positive visual = shooting

        [SerializeField] private float _detectionRange = 10f;
        [SerializeField] private float _weaponsRange = 15f;
        [SerializeField] private float _damage = 5f;
        [SerializeField] private float _movementSpeed = 5f;

        private Animator animator = null; //TODO change to CHaracterAnimotor
        private NavMeshAgent _navAgent = null;
        private Vector3 _moveVelocity = Vector3.zero;
        private Character _player = null;
        private SphereCollider _detectionField = null;
        

        protected override void Start()
        {
            base.Start();

            animator = GetComponentInChildren<Animator>();
            _navAgent = GetComponent<NavMeshAgent>();
            _detectionField = GetComponentInChildren<SphereCollider>();

            _transform = transform;

            _navAgent.speed = _movementSpeed;
            _detectionField.radius = _detectionRange;
        }

        void Update()
        {
            if (Time.timeScale == 0) { return; }

            _moveVelocity = _navAgent.velocity;
            //animator.SetFloat("Speed", _moveVelocity.magnitude);
            
            if (_player != null)
            {
                RotateTowardsPlayer(_player.transform.position);
                if (IsPlayerInRange())
                {
                    Attack(true);
                    _navAgent.isStopped = true;
                }
                else
                {
                    Attack(false);
                    _navAgent.isStopped = false;
                    _navAgent.SetDestination(_player.transform.position);
                }
            } 
        }

        private void RotateTowardsPlayer(Vector3 point)
        {
            transform.LookAt(point);
        }

        private void Attack(bool isAttacking)
        {
            if (isAttacking)
            {
                Weapon.PullTrigger();
            }
            else
            {
                Weapon.ReleaseTrigger();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Player player))
            {
                _player = player.GetComponent<Character>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //player not in detection range anymore, go check the last known position
            if (_player)
            {
                _navAgent.SetDestination(_player.transform.position);
                _player = null;
            }
        }

        private bool IsPlayerInRange()
        {
            //detect which weapon AI has
            return _transform.position.DistanceTo(_player.transform) < _weaponsRange;
        }

        //For now fixed damage based on serializedField instead of dynamic character stats and perks
        public override float MeleeDamage => _damage;
        public override float RangedDamage => _damage;

    }
}