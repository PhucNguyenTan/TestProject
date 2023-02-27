
using UnityEditor;
using UnityEngine;

public class GetPoseData
{
    const string _lleg = "LLeg";
    const string _rleg = "RLeg";
    const string _target = "_target";
    const string _hint = "_hint";
    const string _path = "Assets/Data/Objects/";


    [MenuItem("Assets/Anim_Pose/Humanoid/Get Current Pose data", priority = 1001)]
    private static void GetData()
    {
        GameObject selectedGameObject = Selection.activeObject as GameObject;
        var pose = ScriptableObject.CreateInstance<HumanPose>();

        if (selectedGameObject == null)
        {
            Debug.LogError("Please choose a transform for this");
            return;
        }

        pose.Human = ExtractDataLimb(selectedGameObject.transform);
        if (pose.Human == null)
        {
            Debug.LogError("Rig is incorrect, please check!");
            return;
        }
        AssetDatabase.CreateAsset(pose, _path + Selection.activeContext.name);
        AssetDatabase.SaveAssets();
    }

    private static HumanPart ExtractDataLimb(Transform parent)
    {
        Transform lleg = parent.Find(_lleg + _target);
        Transform rleg = parent.Find(_rleg + _target);
        if(lleg == null || rleg == null)
        {
            return null;
        }
        return new HumanPart(lleg.position, rleg.position);

    }
}
