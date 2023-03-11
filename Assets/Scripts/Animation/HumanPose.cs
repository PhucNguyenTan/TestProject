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
    public Quaternion LFootRot;
    [Header("Right Leg")]
    public Vector3 RFootPos;
    public Vector3 RLegHint;
    public Quaternion RFootRot;

    public void SetAllData(Vector3 lFootPos, Quaternion lFootRot, Vector3 lLegHint,
        Vector3 rFootPos, Quaternion rFootRot, Vector3 rLegHint)
    {
        LFootPos = lFootPos;
        RFootPos = rFootPos;
        LLegHint = lLegHint;
        RLegHint = rLegHint;
        LFootRot = lFootRot;
        RFootRot = rFootRot;
    }
}
