using UnityEditor;
using UnityEngine;

namespace LevelGenerator.Code.Editor
{
    [CustomEditor(typeof(Generator))]
    public class GeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Generator myScript = (Generator)target;
            if (GUILayout.Button("Add Section Template"))
            {
                myScript.AddSectionTemplate();
            }

            if (GUILayout.Button("Add Dead End Template"))
            {
                myScript.AddDeadEndTemplate();
            }
        }
    }
}
