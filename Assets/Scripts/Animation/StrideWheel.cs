using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TestProject.Animation.Manager
{
    public class StrideWheel : MonoBehaviour
    {
        public UnityAction<Transform> CurrentRotation;
        [SerializeField]
        KeyCode _forwardKey = KeyCode.W;
        [SerializeField]
        KeyCode _backKey = KeyCode.S;
        [SerializeField]
        float _acceleration = 1f;
        [SerializeField]
        float _maxSpeed = 5f;
        [SerializeField]
        float _velocity;

        private void Start()
        {
            _velocity = 0;
        }

        private void Update()
        {
            var forward = Input.GetKey(_forwardKey);
            var backward = Input.GetKey(_backKey);
            if ((forward && backward) || (!forward && !backward))
            {
                if (_velocity == 0) return;
                _velocity = _velocity < 0 ?
                    _velocity + _acceleration * Time.deltaTime :
                    _velocity - _acceleration * Time.deltaTime;
            }

            if (forward)
            {
                
                _velocity = Mathf.Min( _velocity + _acceleration * Time.deltaTime, _maxSpeed);
            }
            else if(backward)
            {
                _velocity = Mathf.Max(_velocity - _acceleration * Time.deltaTime, -_maxSpeed);
            }

            transform.Rotate(Time.deltaTime * _velocity, 0f, 0f);
            CurrentRotation?.Invoke(transform);
        }
    }
}

