using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StrideWheel : MonoBehaviour
{
    static public UnityAction<Transform> CurrentRotation;
    [SerializeField]
    KeyCode _forwardKey = KeyCode.W;
    [SerializeField]
    KeyCode _backKey = KeyCode.S;
    [SerializeField]
    float _speed;

    private void Update()
    {
        var forward = Input.GetKey(_forwardKey);
        var backward = Input.GetKey(_backKey);
        if ((forward && backward) || (!forward && !backward)) return;

        if (forward)
        {
            transform.Rotate(Time.deltaTime * _speed, 0f, 0f);
        }
        else
        {
            transform.Rotate(-Time.deltaTime * _speed, 0f, 0f);
        }
        CurrentRotation?.Invoke(transform);
    }
}
