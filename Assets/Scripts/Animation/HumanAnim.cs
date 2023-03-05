using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestProject.Animation.Manager;
using Assets.Animation.Utilities;

namespace TestProject.Animation
{
    public class HumanAnim : MonoBehaviour
    {
        [SerializeField] StrideWheel _wheel;
        [SerializeField] HumanPose _poseWalkL;
        [SerializeField] HumanPose _poseWalkR;
        [SerializeField] HumanPose _poseStand;
        [SerializeField] HumanPose _poseKneeL;
        [SerializeField] HumanPose _poseKneeR;

        Transform _footL;
        Transform _footR;
        Transform _legL;
        Transform _legR;
        HumanPose _currentPose;

        float _rotationPercent;
        float _lerpZ;
        float _max = 0.5f;
        float _min = -0.5f;
        float _time;

        private void Start()
        {
            _time = 0f;
            _min = -0.5f;
            _max = 0.5f; 
            _footL = transform.RecursiveFindChild("LLeg_target");
            _footR =  transform.RecursiveFindChild("RLeg_target");
            _legL = transform.RecursiveFindChild("LLeg_hint");
            _legR = transform.RecursiveFindChild("RLeg_hint");
        }

        private void Update()
        {
            //_lerpZ = _rotationPercent > 0.5f
            //   ? Mathf.Lerp(_min, _max, Mathf.InverseLerp(0.5f, 1f, _rotationPercent))
            //   : Mathf.Lerp(_max, _min, Mathf.InverseLerp(0f, 0.5f, _rotationPercent));
            //transform.position = new Vector3(transform.position.x, transform.position.y, _lerpZ);
            if (IsInRange(_rotationPercent, 0, 0.25f) && _currentPose != _poseKneeL)
                SetPose(_poseKneeL);
            else if (IsInRange(_rotationPercent, 0.25f, 0.5f) && _currentPose != _poseWalkL)
                SetPose(_poseWalkL);
            else if (IsInRange(_rotationPercent, 0.5f, 0.75f) && _currentPose != _poseKneeR)
                SetPose(_poseKneeR);
            else if (IsInRange(_rotationPercent, 0.75f, 1f) && _currentPose != _poseWalkR)
                SetPose(_poseWalkR);
        }

        private void OnEnable()
        {
            _wheel.CurrentRotation += Move;
        }

        private void OnDisable()
        {
            _wheel.CurrentRotation -= Move;
        }

        private void Move(Transform transformOut)
        {
            _rotationPercent = Mathf.Atan2(transformOut.up.z, transformOut.up.y);
            _rotationPercent += Mathf.PI;
            _rotationPercent /= 2f * Mathf.PI;
        }

        void SetPose(HumanPose pose)
        {
            _footL.localPosition = pose.LFootPos;
            _footR.localPosition = pose.RFootPos;
            _footL.localEulerAngles = pose.LFootRot;
            _footR.localEulerAngles = pose.RFootRot;
            _legL.localPosition = pose.LLegHint;
            _legR.localPosition = pose.RLegHint;
            _currentPose = pose;
        }

        bool IsInRange(float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max) == value;
        }
    }

}
