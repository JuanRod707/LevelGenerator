using UnityEditor;
using UnityEngine;

namespace JuanRod.LevelGenerator.Code.Editor
{
    [CustomEditor(typeof(Level))]
    public class LevelEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Level myScript = (Level)target;
            if (GUILayout.Button("Add Section Template"))
            {
                myScript.AddSectionTemplate();
            }
        }
    }
}
