using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    float _rotationPercent;
    float _lerpZ;
    float _max = 0.5f;
    float _min = -0.5f;

    private void Update()
    {
        var time = 0f;
        if(_rotationPercent > 0.5f)
        {
            time = Mathf.InverseLerp(0.5f, 1f, _rotationPercent);
            _lerpZ = Mathf.Lerp(_min, _max, time);
        }
        else if(_rotationPercent >= 0f)
        {
             time = Mathf.InverseLerp(0f, 0.5f, _rotationPercent);
            _lerpZ = Mathf.Lerp(_max, _min, time);

        }
        //Debug.Log($"{_lerpZ} _ {_rotationPercent} _ {time}");
        transform.position = new Vector3(transform.position.x, transform.position.y, _lerpZ);
    }

    private void OnEnable()
    {
        StrideWheel.CurrentRotation += Move;
    }

    private void OnDisable()
    {
        StrideWheel.CurrentRotation -= Move;
    }

    private void Move(Transform transformOut)
    {
        _rotationPercent = Mathf.Atan2(transformOut.up.z, transformOut.up.y);
        _rotationPercent += Mathf.PI;
        _rotationPercent /= 2f * Mathf.PI;
    }
}
