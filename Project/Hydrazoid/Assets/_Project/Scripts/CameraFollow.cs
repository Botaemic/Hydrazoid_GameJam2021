using UnityEngine;

namespace Hydrazoid
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target = null;
        [SerializeField, Range(0f, 1f)] private float _smooth = .5f;
        [SerializeField] private Vector3 _offset = Vector3.zero;

        private Transform _transform = null;
        private Vector3 _followPosition = Vector3.zero;
        private Vector3 _smoothFollowPosition = Vector3.zero;

        void Start()
        {
            _transform = transform;
        }

        void LateUpdate()
        {
            _followPosition = _target.position + _offset;
            _smoothFollowPosition = Vector3.Lerp(_transform.position, _followPosition, _smooth);
            _transform.position = _smoothFollowPosition;
        }
    }
}