using Hydrazoid.Extensions;
using Hydrazoid.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid
{
    public class CharacterControls : MonoBehaviour
    {
        //[SerializeField] private float _movementSpeed = 5f;
        [SerializeField] private Animator _animator = null;
        //[SerializeField] private WeaponBase _currentWeapon = null;

        private WeaponController _weaponController = null;

        private Rigidbody _rb = null;
        private Camera _mainCamera = null;
        private CharacterStats _charStats = null;
        private Vector3 _moveVelocity = Vector3.zero;

        private Pickup _pickup = null;
        
        // Invisible plane for mouse pointer
        private Plane _groundPlane = default;

        public bool Enabled { get; set; } = true;
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _mainCamera = Camera.main;
            _charStats = GetComponent<CharacterStats>();
            //_groundPlane = new Plane(Vector3.up, Vector3.zero);
            _groundPlane = new Plane(Vector3.up, new Vector3(0f,2f,0f)); //Dirty

            _weaponController = GetComponent<WeaponController>();
        }

        private void Update()
        {
            if (Time.timeScale == 0) { return; }

            if(!Enabled) { _moveVelocity = Vector3.zero; return; }

            ProcessInput();
            ProcessRotation();

            ProcessPickupInput();

            //TODO convert to local directional x/y speedw
            _animator.SetFloat("X", _moveVelocity.normalized.x);
            _animator.SetFloat("Y", _moveVelocity.normalized.z);

            if (Input.GetMouseButton(0))
            {
                _weaponController.PullTrigger();
            }
            else
            {
                _weaponController.ReleaseTrigger();
            }
        }

        private void ProcessPickupInput()
        {
            if(Input.GetKeyDown(KeyCode.E) && _pickup != null)
            {
                _pickup.Weapon = _weaponController.SwapWeapon(_pickup.Weapon);
            }
        }

        private void ProcessInput()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");  //WS
            float verticalInput = Input.GetAxisRaw("Vertical");    //AD

            _moveVelocity = new Vector3(horizontalInput, 0, verticalInput);
            _moveVelocity = _moveVelocity.normalized * _charStats.MovementSpeed;
        }

        void FixedUpdate()
        {
            ProcessMovement();
        }

        private void ProcessMovement()
        {
            _rb.velocity = _moveVelocity;
        }

        private void ProcessRotation()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            float rayLenght;
            if(_groundPlane.Raycast(ray, out rayLenght))
            {
                Vector3 pointToLook = ray.GetPoint(rayLenght);
                Debug.DrawLine(ray.origin, pointToLook, Color.red);
                //pointToLook.y = transform.position.y
                transform.LookAt(pointToLook.With(y:transform.position.y));
            }
        }



        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Pickup pickup))
            {
                _pickup = pickup;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Pickup pickup))
            {
                _pickup = null;
            }
        }
    }
}