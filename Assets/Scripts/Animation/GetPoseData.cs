
using System.IO;
using UnityEditor;
using UnityEngine;
using Utilities;

[CustomEditor(typeof(TestEditor))]
public class GetPoseData : Editor
{
    const string _lleg = "LLeg";
    const string _rleg = "RLeg";
    const string _target = "_target";
    const string _hint = "_hint";
    const string _path = "Assets/Data/Objects/";
    TestEditor tempClass;
    string dataName;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        dataName = EditorGUILayout.TextField("Data name", dataName);
        tempClass = target as TestEditor;
        var transform = tempClass.gameObject.transform;

        if (GUILayout.Button("Create Pose Data"))
        {
            if(transform == null)
            {
                Debug.Log("Can't get transform of game object " + target.name);
            }
            SaveData(transform, dataName);


        }
    }

    private void LoadData()
    {

    }

    private void SaveData(Transform selectedGameObject, string name)
    {
        var pose = ScriptableObject.CreateInstance<HumanPose>();

        if (selectedGameObject == null)
        {
            Debug.LogError("Please choose a transform for this");
            return;
        }

        if (pose == null)
        {
            Debug.LogError("Rig is incorrect, please check!");
            return;
        }

        var filePath = _path + name + ".asset";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        ExtractDataLimb(selectedGameObject, pose);

        AssetDatabase.CreateAsset(pose, filePath);
        AssetDatabase.SaveAssets();
    }

    private static void ExtractDataLimb(Transform parent, HumanPose pose)
    {
        var lleg = parent.RecursiveFindChild(_lleg + _target);
        var rleg = parent.RecursiveFindChild(_rleg + _target);
        if(lleg == null || rleg == null)
        {
            return;
        }
        pose.SetAllData(lleg.position, rleg.position);

    }
}
