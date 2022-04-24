using UnityEditor;
using UnityEditor.Rendering.Universal;

[CustomEditor(typeof(CustomURP), true)]
public class CustomURPEditor : Editor {
    private Editor originalEditor;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Custom URP Properties", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(CustomURP.detailGrassBillboardShader)));
        EditorGUILayout.Space();

        if(originalEditor == null)
        {
            originalEditor = Editor.CreateEditor(target, typeof(UniversalRenderPipelineAssetEditor));
        }
        originalEditor.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();
    }
}