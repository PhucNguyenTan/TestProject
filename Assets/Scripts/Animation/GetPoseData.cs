
using System.IO;
using UnityEditor;
using UnityEngine;
using Assets.Animation.Utilities;
using System.Collections.Generic;

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

    HumanPose human;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        dataName = EditorGUILayout.TextField("Data name", dataName);
        tempClass = target as TestEditor;
        var transform = tempClass.gameObject.transform;

        EditorGUI.BeginChangeCheck();

        //??? THis help create a HumanPose picker but don't quite know how it works.
        human = EditorGUILayout.ObjectField("Loaded Pose", human, typeof(HumanPose), true) as HumanPose;

        if (GUILayout.Button("Load Pose"))
        {
            if(human != null) {
                LoadData(transform, human);
            }
        }

        if(GUILayout.Button("Flip Pose"))
        {
            if(human != null)
            {
                FlipData(transform, human);
                SaveData(transform, dataName);
            }
        }

        // ??? Should look into this maybe
        //if (GUILayout.Button("Choose Object"))
        //{
            //{
            //    //create a window picker control ID
            //    currentPickerWindow = EditorGUIUtility.GetControlID(FocusType.Passive) + 100;

            //    Object effectGO = null;

            //    if (Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == currentPickerWindow)
            //    {

            //        effectGO = EditorGUIUtility.GetObjectPickerObject();
            //        currentPickerWindow = -1;

            //        //name of selected object from picker
            //        Debug.Log(effectGO.name);
            //    }
            //}

            ////use the ID you just created
            //EditorGUIUtility.ShowObjectPicker<GameObject>(null, false, "ee", currentPickerWindow);
        //}
        


        if (GUILayout.Button("Save Pose"))
        {
            if(transform == null)
            {
                Debug.Log("Can't get transform of game object  " + target.name);
                return;
            }
            SaveData(transform, dataName);
        }
    }

    void FlipData(Transform parent, HumanPose pose)
    {
        var lleg = parent.RecursiveFindChild(_lleg + _target);
        var rleg = parent.RecursiveFindChild(_rleg + _target);
        var lhint = parent.RecursiveFindChild(_lleg + _hint);
        var rhint = parent.RecursiveFindChild(_rleg + _hint);

        var tempLLegPos = pose.LFootPos;
        var tempLLegHint = pose.LLegHint;
        var tempLLegRot = pose.LFootRot;

        lleg.localPosition = Vector3.Reflect(pose.RFootPos, Vector3.right);
        lleg.localRotation = new Quaternion(pose.LFootRot.x, -pose.LFootRot.y, pose.LFootRot.z, pose.LFootRot.w);
        lhint.localPosition = Vector3.Reflect(pose.RLegHint, Vector3.right);

        rleg.localPosition = Vector3.Reflect(tempLLegPos, Vector3.right);
        rleg.localRotation = new Quaternion(pose.LFootRot.x, -pose.LFootRot.y, pose.LFootRot.z, pose.LFootRot.w);
        rhint.localPosition = Vector3.Reflect(tempLLegHint, Vector3.right);
    }


    void LoadData(Transform parent, HumanPose pose)
    {
        var lleg = parent.RecursiveFindChild(_lleg + _target);
        var rleg = parent.RecursiveFindChild(_rleg + _target);
        var lhint = parent.RecursiveFindChild(_lleg + _hint);
        var rhint = parent.RecursiveFindChild(_rleg + _hint);

        lleg.localPosition = pose.LFootPos;
        lleg.localRotation = pose.LFootRot;
        lhint.localPosition = pose.LLegHint;
        rleg.localPosition = pose.RFootPos;
        rleg.localRotation = pose.RFootRot;
        rhint.localPosition = pose.RLegHint;
    }

    void SaveData(Transform selectedGameObject, string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("Please add name before saving pose!");
            return;
        }

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

    static void ExtractDataLimb(Transform parent, HumanPose pose)
    {
        var lleg = parent.RecursiveFindChild(_lleg + _target);
        var rleg = parent.RecursiveFindChild(_rleg + _target);
        var lhint = parent.RecursiveFindChild(_lleg + _hint);
        var rhint = parent.RecursiveFindChild(_rleg + _hint);

        if (lleg == null || rleg == null)
        {
            return;
        }
        pose.SetAllData(lleg.localPosition,
            lleg.localRotation,
            lhint.localPosition,
            rleg.localPosition,
            rleg.localRotation,
            rhint.localPosition);

    }
}
