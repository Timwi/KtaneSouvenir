using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public partial class CommunityFeaturesDownloader : EditorWindow
{
    private class HelpPopup : EditorWindow
    {
        private void OnGUI()
        {
            GUILayout.Label("To add your plugin to the list, contact qkrisi on Discord.");
            GUILayout.BeginHorizontal();
            GUILayout.Label("You can find him on the", GUILayout.ExpandWidth(false));
            if(LinkButton("KTANE Discord server."))
                Application.OpenURL("https://discord.gg/K6uQMyBcYZ");
            GUILayout.EndHorizontal();
            GUILayout.Label("Make sure you have a build of your plugin ready alongside a proper documentation.");
            GUILayout.Space(30);
            if (GUILayout.Button("OK"))
            {
                HelpWindow = null;
                Close();
            }
        }
    }
    
    [MenuItem("Keep Talking ModKit/Plugins", false, priority = 950)]
    private static void ShowWindow()
    {
        var window = GetWindow<CommunityFeaturesDownloader>();
        window.titleContent = new GUIContent("KMPlugins");
        window.maximized = true;
        window.Show();
    }

    public const string VERSION = "2.0.3.1";
    public readonly Version PARSED_VERSION = new Version(VERSION);

    private static readonly string[] Sizes = {"KB", "MB", "GB", "TB" };
    private const int Divisor = 1000;

    private static string Convert(ulong bytes)
    {
        if (bytes < Divisor)
            return bytes + "B";
        for (int i = 1; i <= Sizes.Length; i++)
        {
            var value = bytes / (decimal)Math.Pow(Divisor, i);
            if (value < Divisor)
                return Math.Round(value, 2) + Sizes[i-1];
        }
        return bytes + "B";
    }

    private static LinkedList<WWW> FeaturesFetchList;
    private static LinkedListNode<WWW> FeaturesFetch;

    private static FeatureInfo[] Features;
    private static FeatureInfo CurrentFeature;

    private static string DataPath;
    private static string InfoFilePath;
    private static string BackupPath;

    private static FeaturesSave SaveFile;

    private static HelpPopup HelpWindow;
    private static SourceEditorWindow SourceWindow;
    private float time;
    private int dots = 1;
    private const int FeatureButtonWidth = 300;
    private const int ProgressBarHeight = 20;
    private const int DownloadButtonWidth = 100;
    private int RefreshButtonWidth = FeatureButtonWidth / 2;
    private readonly GUIStyle RichStyle = new GUIStyle();
    private readonly string[] DefaultSources = new string[]
    {
        "https://qkrisi.hu/ktane/kmplugins"
    };

    private Vector2 ScrollPos;
    
    
    private void CreateFeatureButton(FeatureInfo feature)
    {
        bool selected = CurrentFeature != null && feature.Name == CurrentFeature.Name;
        GUI.enabled = !selected;
        var selectPlugin = GUILayout.Button(String.Format("{0} (By {1})", feature.Name, feature.Author), GUILayout.Width(FeatureButtonWidth));
        GUI.enabled = true;
        if(selectPlugin && !selected)
        {
            CurrentFeature = feature;
            Repaint();
        }
    }

    private static void SavePlugins()
    {
        File.WriteAllText(InfoFilePath, JsonConvert.SerializeObject(SaveFile));
    }

    private void LoadPlugins()
    {
        if (File.Exists(InfoFilePath))
        {
            try
            {
                SaveFile = JsonConvert.DeserializeObject<FeaturesSave>(File.ReadAllText(InfoFilePath));
                return;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        if (SaveFile == null)
            SaveFile = new FeaturesSave
            {
                InstalledPlugins = new List<DownloadInfo>(),
                CustomSources = new List<string>()
            };
    }

    private static void ResetState()
    {
        FeaturesFetch = null;
        Features = null;
        CurrentFeature = null;
        SaveFile = null;
        GithubReleaseHandler.ReleaseCache.Clear();
    }

    internal static bool LinkButton(string text)
    {
        return GUILayout.Button(String.Format("<color=blue>{0}</color>", text),
            new GUIStyle(GUI.skin.label) { richText = true });
    }

    private void Update()
    {
        Repaint();
    }
    
    private void OnGUI()
    {
        if(SaveFile == null)
            LoadPlugins();
        if (FeaturesFetch == null)
        {
            FeaturesFetchList =
                new LinkedList<WWW>(DefaultSources.Concat(SaveFile.CustomSources).Distinct()
                    .Where(s => !string.IsNullOrEmpty(s.Trim())).Select(s => new WWW(s.Trim())));
            FeaturesFetch = FeaturesFetchList.First;
            return;
        }
        if (Event.current.type == EventType.Repaint)
        {
            time += Time.deltaTime;
            if (time >= 3)
            {
                time = 0;
                if (++dots == 4)
                    dots = 1;
            }
        }
        try
        {
            if (!FeaturesFetch.Value.isDone ||
                FeaturesFetch.Next != null ||
                Features != null && Features.Any(f => f.Handler != null && !f.Handler.Ready))
            {
                if(!string.IsNullOrEmpty(FeaturesFetch.Value.error))
                    Debug.LogErrorFormat("Failed to fetch source {0}: {1}", FeaturesFetch.Value.url, FeaturesFetch.Value.error);
                if (FeaturesFetch.Value.isDone && FeaturesFetch.Next != null)
                    FeaturesFetch = FeaturesFetch.Next;
                EditorGUILayout.HelpBox("Fetching plugins for the KTaNE Modkit" + new String('.', dots),
                    MessageType.Info, true);
                return;
            }

            /*if (!String.IsNullOrEmpty(FeaturesFetch.Value.error))
            {
                EditorGUILayout.HelpBox("Network error: " + FeaturesFetch.Value.error, MessageType.Error, true);
                if (GUILayout.Button("Retry"))
                    Reset();
                return;
            }*/

            FeatureInfo Downloading = null;
            float height = position.height - 45;
            if (Features != null && !Features.Any(f => f.Handler == null))
            {
                Downloading = Features.FirstOrDefault(f => f.Handler != null && !f.Handler.Downloader.Ready);
                if (Downloading != null)
                {
                    var progress = Downloading.Handler.Downloader.Progress;
                    EditorGUI.ProgressBar(new Rect(0, 0, position.width, ProgressBarHeight), progress.Progress,
                        String.Format("Downloading {0} {1}", Downloading.Name, progress.ConvertedProgress));
                    GUILayout.Space(ProgressBarHeight);
                    height -= ProgressBarHeight;
                }
            }
            else
            {
                var _features = new List<FeatureInfo>();
                foreach (var query in FeaturesFetchList)
                {
                    if(!string.IsNullOrEmpty(query.error) || string.IsNullOrEmpty(query.text.Trim()))
                        continue;
                    var deserializedFeatures = JsonConvert.DeserializeObject<FeatureInfo[]>(query.text.Trim());
                    if(deserializedFeatures != null)
                        _features.AddRange(deserializedFeatures);
                }

                Features = _features.GroupBy(f => f.Name).Select(g => g.First()).ToArray();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(false), GUILayout.Width(FeatureButtonWidth));
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(4);
            
            if (GUILayout.Button("Refresh", GUILayout.Width(RefreshButtonWidth)))
            {
                ResetState();
                goto finish;
            }

            EditorGUILayout.BeginVertical();
            GUILayout.Space(4);
            if (LinkButton("Edit sources"))
            {
                if (SourceWindow == null)
                {
                    SourceWindow = GetWindow<SourceEditorWindow>(true, "Edit sources", true);
                    SourceWindow.position = new Rect(Screen.width / 2 - SourceWindow.position.width / 2f,
                        Screen.height / 2 - 125, SourceWindow.position.width, 250);
                    SourceWindow.Show();
                }
                else SourceWindow.Focus();
            }
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.EndHorizontal();
            ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos, GUILayout.Height(height), GUILayout.Width(FeatureButtonWidth+20));
            foreach (var feature in Features)
                CreateFeatureButton(feature);
            EditorGUILayout.EndScrollView();
            if (LinkButton("Adding plugins"))
            {
                if (HelpWindow == null)
                {
                    HelpWindow = GetWindow<HelpPopup>(true, "Adding plugins", true);
                    HelpWindow.position = new Rect(Screen.width / 2 - 250, Screen.height / 2 - 52, 500, 105);
                    HelpWindow.Show();
                }
                else HelpWindow.Focus();
            }
            EditorGUILayout.EndVertical();
            if (CurrentFeature != null && CurrentFeature.Handler != null)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(false),
                    GUILayout.Width(position.width - FeatureButtonWidth - 40));
                GUILayout.Label(String.Format("<i>{0} (By {1})</i>", CurrentFeature.Name, CurrentFeature.Author), RichStyle);
                EditorGUILayout.BeginHorizontal();
                var AffectedFiles = new string[0];
                try
                {
                    AffectedFiles = CurrentFeature.Handler.Downloader.AffectedFiles;
                }
                catch {}
                GUI.enabled = Downloading == null;
                var downloadedPlugin = SaveFile.InstalledPlugins.FirstOrDefault(p => p.Name == CurrentFeature.Name);
                if (downloadedPlugin == null)
                {
                    if (CurrentFeature.ParsedMinVersion <= PARSED_VERSION && CurrentFeature.ParsedMaxVersion >= PARSED_VERSION)
                    {
                        var enableInstall = CurrentFeature.Handler.Draw();
                        if(!enableInstall)
                            GUILayout.Label("<color=red>No installation candidate found</color>", RichStyle, GUILayout.ExpandWidth(false));
                        else if (GUILayout.Button("Install", GUILayout.Width(DownloadButtonWidth)))
                            SaveFile.InstalledPlugins.Add(CurrentFeature.Handler.Download());
                    }
                    else
                    {
                        var versionInfo = CurrentFeature.ParsedMinVersion > PARSED_VERSION ? "minimum version: " + CurrentFeature.MinVersion : "maximum version: " + CurrentFeature.MaxVersion;
                        GUILayout.Label(string.Format(
                            "<color=red>This plugin is not compatible with this version of the modkit ({0}; {1})</color>",
                            VERSION, versionInfo), RichStyle, GUILayout.ExpandWidth(false));
                    }

                    SavePlugins();
                }
                else
                {
                    GUILayout.Label("<color=green>✓</color> Installed" + (String.IsNullOrEmpty(downloadedPlugin.Info)
                            ? ""
                            : String.Format(" ({0})", downloadedPlugin.Info)), RichStyle, GUILayout.ExpandWidth(false));
                    GUILayout.Space(5);
                    if (GUILayout.Button("Remove", GUILayout.Width(DownloadButtonWidth)))
                    {
                        foreach (var _file in downloadedPlugin.Files)
                        {
                            var file = _file;
                            if(file.EndsWith("/*"))
                                file = file.Remove(file.Length - 2);
                            var ModkitPath = Path.Combine(DataPath, file);
                            if (CurrentFeature.Integration)
                            {
                                var Backup = Path.Combine(BackupPath, file);
                                if (File.Exists(Backup))
                                {
                                    File.Copy(Backup, ModkitPath, true);
                                    continue;
                                }
                                if (Directory.Exists(Backup))
                                {
                                    EnsureAbsoluteDirectoryExists(ModkitPath);
                                    CopyFilesRecursively(new DirectoryInfo(Backup), new DirectoryInfo(ModkitPath));
                                    continue;
                                }
                            }
                            Remove(ModkitPath);
                        }
                        SaveFile.InstalledPlugins.Remove(downloadedPlugin);
                        SavePlugins();
                    }
                }
                GUI.enabled = true;
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(10);
                GUILayout.Label(CurrentFeature.Description, RichStyle);
                GUILayout.Space(10);
                if (CurrentFeature.Links != null)
                {
                    GUILayout.Label("<i>Links:</i>", RichStyle);
                    foreach(var pair in CurrentFeature.Links)
                    {
                        if (LinkButton(pair.Key))
                            Application.OpenURL(pair.Value);
                    }
                    GUILayout.Space(10);
                }
                GUILayout.Label("<i>Files or directories created/modified:</i>\n-" + String.Join("\n-", AffectedFiles), RichStyle);
            }
            else GUILayout.BeginVertical();
            finish:
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
        }
        catch (ArgumentException argEx)
        {
            if(!argEx.Message.StartsWith("Getting control") && !argEx.Message.StartsWith("GUILayout:"))
                Debug.LogException(argEx);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private static void Remove(string path)
    {
        if(File.Exists(path))
            File.Delete(path);
        else if(Directory.Exists(path))
            Directory.Delete(path, true);
    }
    
    private static void EnsureAbsoluteDirectoryExists(string dir)
    {
        if (!String.IsNullOrEmpty(dir) && dir != DataPath && !Directory.Exists(dir))
        {
            EnsureAbsoluteDirectoryExists(Path.GetDirectoryName(dir));
            Directory.CreateDirectory(dir);
        }
    }
    
    private static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
    {
        var Subdirectories = target.GetDirectories();
        foreach (DirectoryInfo dir in source.GetDirectories())
            CopyFilesRecursively(dir,
                Subdirectories.FirstOrDefault(d => d.Name == dir.Name) ?? target.CreateSubdirectory(dir.Name));
        foreach (FileInfo file in source.GetFiles())
            file.CopyTo(Path.Combine(target.FullName, file.Name), true);
    }

    void OnEnable()
    {
        RichStyle.richText = true;
        DataPath = Application.dataPath;
        HelpWindow = null;
        var InfoPath = Path.Combine(DataPath, "../CommunityPlugins");
        BackupPath = Path.Combine(InfoPath, "Backup");
        InfoFilePath = Path.Combine(InfoPath, "plugins.json");
        EnsureAbsoluteDirectoryExists(BackupPath);
        LoadPlugins();
    }
}
