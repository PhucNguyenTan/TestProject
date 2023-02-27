using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Anim/HumanoidPose")]
public class HumanPose : ScriptableObject
{
    [Header("Left Leg")]
    public Vector3 LFootPos;
    public Vector3 LLegHint;
    public Vector3 LFootRot;
    [Header("Right Leg")]
    public Vector3 RFootPos;
    public Vector3 RLegHint;
    public Vector3 RFootRot;

    public void SetAllData(Vector3 lFootPos, Vector3 rFootPos)
    {
        LFootPos = lFootPos;
        RFootPos = rFootPos;
    }
}
