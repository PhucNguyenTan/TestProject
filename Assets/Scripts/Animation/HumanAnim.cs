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
            if (IsInRange(_rotationPercent, 0, 0.2f))
            {
                SetPose(InterpolatePoses(_poseStand, _poseKneeL,
                    Mathf.InverseLerp(0, 0.2f, _rotationPercent)));
            }
            else if (IsInRange(_rotationPercent, 0.2f, 0.4f))
            {
                SetPose(InterpolatePoses(_poseKneeL, _poseWalkL,
                    Mathf.InverseLerp(0.2f, 0.4f, _rotationPercent)));
            }
            else if (IsInRange(_rotationPercent, 0.4f, 0.6f))
            {
                _currentPose = _poseStand;
                SetPose(InterpolatePoses(_poseWalkL, _poseKneeR,
                    Mathf.InverseLerp(0.4f, 0.6f, _rotationPercent)));
            }
            else if (IsInRange(_rotationPercent, 0.6f, 0.8f))
            {
                _currentPose = _poseStand;
                SetPose(InterpolatePoses(_poseKneeR, _poseWalkR,
                    Mathf.InverseLerp(0.6f, 0.8f, _rotationPercent)));
            }
            else if (IsInRange(_rotationPercent, 0.8f, 1f))
            {
                _currentPose = _poseStand;
                SetPose(InterpolatePoses(_poseWalkR, _poseStand,
                    Mathf.InverseLerp(0.8f, 1f, _rotationPercent)));
            }
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
            _footL.localRotation = pose.LFootRot;
            _footR.localRotation = pose.RFootRot;
            _legL.localPosition = pose.LLegHint;
            _legR.localPosition = pose.RLegHint;
        }

        HumanPose InterpolatePoses(HumanPose ogPose, HumanPose toPose, float value)
        {
            var newPose = new HumanPose();
            newPose.SetAllData(
                Vector3.Lerp(ogPose.LFootPos, toPose.LFootPos, value),
                Quaternion.Lerp(ogPose.LFootRot, toPose.LFootRot, value),
                Vector3.Lerp(ogPose.LLegHint, toPose.LLegHint, value),
                Vector3.Lerp(ogPose.RFootPos, toPose.RFootPos, value),
                Quaternion.Lerp(ogPose.RFootRot, toPose.RFootRot, value),
                Vector3.Lerp(ogPose.RLegHint, toPose.RLegHint, value));
            return newPose;
        }

        bool IsInRange(float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max) == value;
        }

        //Vector3 SetFootRotation(Vector3 ogRot, Vector3 toRot)
        //{
        //    var rotationDif = Quaternion.FromToRotation(ogRot, toRot);
        //    if(rotationDif.eulerAngles.y > 180f)
        //    {
        //        rotationDif.eulerAngles.y > 180f
        //    }
        //    else
        //    {
                
        //    }
        //}
    }

}
