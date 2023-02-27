using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Anim/HumanoidPose")]
public class HumanPose : ScriptableObject
{
    public HumanPart Human;

    public void SetAllData()
    {

    }
}

public class HumanPart
{
    [Header("Left Leg")]
    public Vector3 LFootPos;
    public Vector3 LLegHint;
    public Vector3 LFootRot;
    [Header("Right Leg")]
    public Vector3 RFootPos;
    public Vector3 RLegHint;
    public Vector3 RFootRot;

    public HumanPart(Vector3 lFootPos, Vector3 rFootPos//,
                                                       //Vector3 lLegHint = Vector3.zero, Vector3 rLegHint = null,
                                                       //Vector3 lFootRot = null, Vector3 rFootRot = null
        )
    {
        LFootPos = lFootPos;
        RFootPos = rFootPos;
    }
}
