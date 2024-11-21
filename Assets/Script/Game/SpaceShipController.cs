using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;

namespace Assets.Script.Game
{
    public class SpaceShipController : MonoBehaviour
    {
        [SerializeField] private SpaceShipData spaceShipData;
        [SerializeField] private float proximityThreshold = 0.1f;

        private float _targetHeight;
        private Vector3 _direction;

        private float _currentVelocity;

        private void Start()
        {
            
        }

        private void Update()
        {
            UpdatePosition();
        }

        public void SetUpData(SpaceShipData newData)
        {
            spaceShipData = newData;
        }

        public void UpdateTargetPosition(Vector3 targetPosition)
        {
            targetPosition = TouchController.ScreenToWorldPoint(targetPosition);

            _targetHeight = targetPosition.x;

            _direction = _targetHeight > transform.position.x ? Vector3.right : Vector3.left;
        }

        private void UpdatePosition()
        {
            if (_direction == Vector3.zero) return;

            _currentVelocity += spaceShipData.Acceleration * Time.deltaTime;
            _currentVelocity = Mathf.Clamp(_currentVelocity, 0, spaceShipData.MaxVelocity);

            transform.position = transform.position + _direction * _currentVelocity;

            float _proximity = transform.position.x - _targetHeight;

            if (Mathf.Abs(_proximity) < proximityThreshold)
            {
                _direction = Vector3.zero;
                _currentVelocity = 0f;
                _targetHeight = 0f;
            }
        }
    }
}