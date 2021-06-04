using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Hydrazoid
{
    public class SeeThroughMask : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask = default;
        private Camera _mainCamera = null;
        private Vector3 _cameraPosition = Vector3.zero;
        private Transform _transform = null;
        void Start()
        {
            _mainCamera = Camera.main;
            _cameraPosition = _mainCamera.transform.position;
            _transform = transform;
        }

        void Update()
        {
            RaycastHit hit;
            _cameraPosition = _mainCamera.transform.position;

            if (Physics.Raycast(_cameraPosition, (_transform.position - _cameraPosition).normalized, out hit, Mathf.Infinity, _layerMask))
            {
                //TODO Remove CompareTag()
                if(hit.collider.gameObject.CompareTag("SeeThroughMask"))
                {
                    transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                }
                else
                {
                    transform.localScale = new Vector3(5f, 5f, 5f);
                }
            }
        }
    }
}