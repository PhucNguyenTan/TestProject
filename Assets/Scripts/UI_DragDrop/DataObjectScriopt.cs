using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UI_DragDrop
{
    [CreateAssetMenu(menuName = "UI_test/DragDrop")]
    public class DataObjectScriopt : ScriptableObject
    {
        public Texture2D[] Textures;
    }
}