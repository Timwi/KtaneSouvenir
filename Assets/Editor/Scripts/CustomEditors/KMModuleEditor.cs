using UnityEditor;
using UnityEngine;

public abstract partial class KMDelegateEditor : Editor
{
    protected static readonly bool DelegateEditorsActive;
    
    protected bool SkipBase;
}

[CustomEditor(typeof(KMBombModule))]
public class KMBombModuleEditor : KMDelegateEditor
{
    public override void OnInspectorGUI()
    {
        if (target != null)
        {
            serializedObject.Update();

            var moduleTypeProperty = serializedObject.FindProperty("ModuleType");
            EditorGUILayout.PropertyField(moduleTypeProperty);
            moduleTypeProperty.stringValue = moduleTypeProperty.stringValue.Trim();

            var moduleDisplayNameProperty = serializedObject.FindProperty("ModuleDisplayName");
            EditorGUILayout.PropertyField(moduleDisplayNameProperty);
            moduleDisplayNameProperty.stringValue = moduleDisplayNameProperty.stringValue.Trim();
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("RequiresTimerVisibility"));

            serializedObject.ApplyModifiedProperties();
            
            if(DelegateEditorsActive)
            {
                SkipBase = true;
                base.OnInspectorGUI();
            }
        }
    }
}

[CustomEditor(typeof(KMNeedyModule))]
public class KMNeedyModuleEditor : KMDelegateEditor
{
    public override void OnInspectorGUI()
    {
        if (target != null)
        {
            serializedObject.Update();
            
            var countdownTimeProperty = serializedObject.FindProperty("CountdownTime");
            EditorGUILayout.PropertyField(countdownTimeProperty);
            countdownTimeProperty.floatValue = Mathf.Max(countdownTimeProperty.floatValue, 0f);
            
            var resetDelayMinProperty = serializedObject.FindProperty("ResetDelayMin");
            EditorGUILayout.PropertyField(resetDelayMinProperty);
            resetDelayMinProperty.floatValue = Mathf.Max(resetDelayMinProperty.floatValue, 0f);
            
            var resetDelayMaxProperty = serializedObject.FindProperty("ResetDelayMax");
            EditorGUILayout.PropertyField(resetDelayMaxProperty);
            resetDelayMaxProperty.floatValue = Mathf.Max(resetDelayMaxProperty.floatValue, resetDelayMinProperty.floatValue);

            var moduleTypeProperty = serializedObject.FindProperty("ModuleType");
            EditorGUILayout.PropertyField(moduleTypeProperty);
            moduleTypeProperty.stringValue = moduleTypeProperty.stringValue.Trim();

            var moduleDisplayNameProperty = serializedObject.FindProperty("ModuleDisplayName");
            EditorGUILayout.PropertyField(moduleDisplayNameProperty);
            moduleDisplayNameProperty.stringValue = moduleDisplayNameProperty.stringValue.Trim();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("RequiresTimerVisibility"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("WarnAtFiveSeconds"));

            serializedObject.ApplyModifiedProperties();
            
            if(DelegateEditorsActive)
            {
                SkipBase = true;
                base.OnInspectorGUI();
            }
        }
    }
}
