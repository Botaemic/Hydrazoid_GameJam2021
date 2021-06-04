using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid
{
    public class FloatingCoin : MonoBehaviour
    {
        [SerializeField] private float _floatUpTargetSpeed = 5f;
        [SerializeField] private float _lerpFloatSpeed = 1f;
        [SerializeField] private float _startFadeTimer = 1f;
        [SerializeField] private float _fadeSpeed = 0.5f;
        [SerializeField] private float _increaseScaleAmount = 0.2f;
        [SerializeField] private float _decreaseScaleAmount = 0.2f;

        //private int scoreValue;
        private Color coinColor;

        private Rigidbody myRigidBody = null;
        private MeshRenderer myMeshRenderer = null;

        private void Awake()
        {
            myRigidBody = GetComponent<Rigidbody>();
            myMeshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            coinColor = myMeshRenderer.material.color;
        }

        private void Update()
        {
            UpdateFloatUpVelocity();
            UpdateXVelocity();
            UpdateZCVelocity();
            UpdateFade();
        }

        private void UpdateFloatUpVelocity()
        {
            float currentFloatUpSpeed = Mathf.Lerp(myRigidBody.velocity.y, _floatUpTargetSpeed, _lerpFloatSpeed * Time.deltaTime);
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, currentFloatUpSpeed, myRigidBody.velocity.z);
        }

        private void UpdateXVelocity()
        {
            if (myRigidBody.velocity.x > 0f)
            {
                float currentXSpeed = myRigidBody.velocity.x;
                currentXSpeed -= _lerpFloatSpeed * Time.deltaTime;

                if (currentXSpeed < 0f)
                {
                    currentXSpeed = 0f;
                }

                myRigidBody.velocity = new Vector3(currentXSpeed, myRigidBody.velocity.y, myRigidBody.velocity.z);
            }
            else if (myRigidBody.velocity.x < 0f)
            {
                float currentXSpeed = myRigidBody.velocity.x;
                currentXSpeed += _lerpFloatSpeed * Time.deltaTime;

                if (currentXSpeed > 0f)
                {
                    currentXSpeed = 0f;
                }

                myRigidBody.velocity = new Vector3(currentXSpeed, myRigidBody.velocity.y, myRigidBody.velocity.z);
            }
        }

        private void UpdateZCVelocity()
        {
            if (myRigidBody.velocity.z > 0f)
            {
                float currentZSpeed = myRigidBody.velocity.z;
                currentZSpeed -= _lerpFloatSpeed * Time.deltaTime;

                if (currentZSpeed < 0f)
                {
                    currentZSpeed = 0f;
                }

                myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, myRigidBody.velocity.y, currentZSpeed);
            }
            else if (myRigidBody.velocity.x < 0f)
            {
                float currentZSpeed = myRigidBody.velocity.z;
                currentZSpeed += _lerpFloatSpeed * Time.deltaTime;

                if (currentZSpeed > 0f)
                {
                    currentZSpeed = 0f;
                }

                myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, myRigidBody.velocity.y, currentZSpeed);
            }
        }

        private void UpdateFade()
        {
            if (_startFadeTimer > 0)
            {
                _startFadeTimer -= Time.deltaTime;
                transform.localScale += Vector3.one * _increaseScaleAmount * Time.deltaTime;
            }
            else
            {
                coinColor.a -= _fadeSpeed * Time.deltaTime;
                myMeshRenderer.material.color = coinColor;

                transform.localScale -= Vector3.one * _decreaseScaleAmount * Time.deltaTime;

                if (myMeshRenderer.material.color.a < 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void Setup(int coinValue)
        {
            EventList.OnCashPickup?.Invoke(coinValue);
        }
    }
}

