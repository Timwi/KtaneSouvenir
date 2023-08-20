using System.Linq;
using UnityEditor;
using UnityEngine;

public partial class CommunityFeaturesDownloader
{
    private class SourceEditorWindow : EditorWindow
    {
        private Vector2 ScrollPos;
        
        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Manage custom sources:");
            ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos, GUILayout.Height(200));
            for (int i = 0; i < SaveFile.CustomSources.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("-", GUILayout.Width(25)))
                {
                    SaveFile.CustomSources.RemoveAt(i);
                    EditorGUILayout.EndHorizontal();
                    goto finish;
                }
                SaveFile.CustomSources[i] = EditorGUILayout.TextField(SaveFile.CustomSources[i]);
                EditorGUILayout.EndHorizontal();
            }
            if (GUILayout.Button("+", GUILayout.Width(25)))
            {
                SaveFile.CustomSources.Add("");
                goto finish;
            }
            EditorGUILayout.EndScrollView();
            GUILayout.Space(5);
            if (GUILayout.Button("Save"))
            {
                EditorGUILayout.EndVertical();
                SaveFile.CustomSources = SaveFile.CustomSources.Where(s => !string.IsNullOrEmpty(s.Trim()))
                    .Select(s => s.Trim()).ToList();
                SavePlugins();
                ResetState();
                Close();
            }
            finish:
            EditorGUILayout.EndVertical();
        }
    }
}