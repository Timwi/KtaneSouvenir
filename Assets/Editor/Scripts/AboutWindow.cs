using UnityEditor;
using UnityEngine;

public class AboutWindow : EditorWindow
{
	[MenuItem("Keep Talking ModKit/About", false, priority = 15000)]
	private static void ShowWindow()
	{
		var window = GetWindow<AboutWindow>();
		window.titleContent = new GUIContent("About modkit");
		window.position = new Rect(Screen.width / 2 - 250, Screen.height / 2 - 52, 500, 105);
		window.Show();
	}
	
	private void OnGUI()
	{
		GUILayout.Label("Keep Talking and Nobody Explodes Community Modkit");
		GUILayout.Label("Version: " + CommunityFeaturesDownloader.VERSION);
		if(CommunityFeaturesDownloader.LinkButton("GitHub"))
			Application.OpenURL("https://github.com/qkrisi/ktanemodkit");
		GUILayout.Space(30);
		if(GUILayout.Button("Close"))
			Close();
	}
}
