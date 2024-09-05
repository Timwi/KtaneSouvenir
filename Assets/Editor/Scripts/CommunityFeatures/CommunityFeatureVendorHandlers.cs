using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public partial class CommunityFeaturesDownloader
{
    private enum FileVendor
    { 
        GithubRelease,
        Direct
    }

    private static Dictionary<FileVendor, Type> FeatureHandlerTypes = new Dictionary<FileVendor, Type>
    {
        { FileVendor.GithubRelease, typeof(GithubReleaseHandler) },
        { FileVendor.Direct, typeof(DirectDownloadHandler) }
    };
    
    private abstract class FeatureHandler
    {
        protected readonly FeatureInfo Feature;
        public readonly FeatureDownloadHandler Downloader;

        protected bool _ready;

        public bool Ready
        {
            get
            {
                Update();
                return _ready;
            }
        }

        public virtual DownloadInfo Download()
        {
            if (Feature.Integration)
            {
                foreach (var file in Downloader.AffectedFiles)
                {
                    var dir = Path.GetDirectoryName(file);
                    if(!String.IsNullOrEmpty(dir))
                        EnsureAbsoluteDirectoryExists(Path.Combine(BackupPath, dir));
                    var path = Path.Combine(DataPath, file);
                    var destination = Path.Combine(BackupPath, file);
                    if(File.Exists(path))
                        File.Copy(path, destination, true);
                    else if (Directory.Exists(path))
                    {
                        EnsureAbsoluteDirectoryExists(destination);
                        CopyFilesRecursively(new DirectoryInfo(path), new DirectoryInfo(destination));
                    }
                }
            }
            return new DownloadInfo
            {
                Files = Downloader.AffectedFiles,
                Info = "",
                Name = Feature.Name
            };
        }

        public abstract bool Draw();

        public virtual void Fetch()
        {
            _ready = false;
        }
        public virtual void Update() {}

        protected FeatureHandler(FeatureInfo feature)
        {
            Feature = feature;
            Downloader = (FeatureDownloadHandler)Activator.CreateInstance(DownloadHandlerTypes[Feature.FileType], Feature);
        }
    }

    private class DirectDownloadHandler : FeatureHandler
    {
        private readonly ulong Bytes;
        private readonly string ConvertedBytes;
        
        public override DownloadInfo Download()
        {
            var info = base.Download();
            Downloader.Download(Feature.DownloadData["URL"], Bytes);
            return info;
        }

        public override bool Draw()
        {
            GUILayout.Label(ConvertedBytes, GUILayout.ExpandWidth(false));
            GUILayout.Space(5);
            return true;
        }

        public override void Fetch()
        {
            base.Fetch();
            _ready = true;
        }

        public DirectDownloadHandler(FeatureInfo feature) : base(feature)
        {
            Downloader.Filename = Feature.DownloadData["Name"];
            Bytes = ulong.Parse(Feature.DownloadData["Size"]);
            ConvertedBytes = Convert(Bytes);
        }
    }

    private class GithubReleaseHandler : FeatureHandler
    {
        private const int ResultsPerPage = 100;

        public static Dictionary<string, List<ReleaseInfo>> ReleaseCache = new Dictionary<string, List<ReleaseInfo>>();
        private List<ReleaseInfo> Releases
        {
            get
            {
                return ReleaseCache[Feature.Name];
            }
        }
        private string Repo;
        private bool UseSourceCode;
        private string ContentType;
        private string[] ContentTypes = new string[0];
        private string NamePattern;
        private string TagPattern;
        private int PageNumber = 1;
        private WWW CurrentRequest;
        private int LastSelectedAssetIndex = -1;
        private int LastSelectedVersionIndex = -1;
        private int SelectedVersionIndex;
        private int SelectedAssetIndex;
        private string[] VersionTags;
        private string[] Assets;
        private string DefaultAsset;

        private const int DropdownWidth = 300;

        public override DownloadInfo Download()
        {
            var info = base.Download();
            var release = Releases[SelectedVersionIndex];
            Downloader.IsSourceCode = UseSourceCode;
            if (UseSourceCode)
            {
                Downloader.Filename = release.tag_name + ".zip";
                Downloader.Download(release.zipball_url, 0);
            }
            else
            {
                var asset = release.assets[SelectedAssetIndex];
                Downloader.Download(asset.browser_download_url, asset.size);
            }
            info.Info = release.tag_name;
            return info;
        }

        public override bool Draw()
        {
            if(VersionTags == null || VersionTags.Length == 0)
                return false;
            SelectedVersionIndex = EditorGUILayout.Popup(SelectedVersionIndex, VersionTags, GUILayout.Width(DropdownWidth));
            var update = false;
            if (SelectedVersionIndex != LastSelectedVersionIndex)
            {
                update = true;
                Assets = Releases[SelectedVersionIndex].assets.Select(a => String.Format("{0} ({1})", a.name, a.ConvertedSize)).ToArray();
                var index = Array.IndexOf(Assets, DefaultAsset);
                SelectedAssetIndex = index == -1 ? 0 : index;
                LastSelectedVersionIndex = SelectedVersionIndex;
            }
            if (!UseSourceCode)
            {
                SelectedAssetIndex = EditorGUILayout.Popup(SelectedAssetIndex, Assets, GUILayout.Width(DropdownWidth));
                if (update || SelectedAssetIndex != LastSelectedAssetIndex)
                {
                    Downloader.Filename = Releases[SelectedVersionIndex].assets[SelectedAssetIndex].name;
                    LastSelectedAssetIndex = SelectedAssetIndex;
                }
            }
            return true;
        }

        public override void Fetch()
        {
            base.Fetch();
            if (!ReleaseCache.ContainsKey(Feature.Name))
                ReleaseCache.Add(Feature.Name, new List<ReleaseInfo>());
            else _ready = true;
            Repo = Feature.DownloadData["Repo"];
            string useSourceCode;
            UseSourceCode = Feature.DownloadData.TryGetValue("UseSourceCode", out useSourceCode) &&
                            useSourceCode == "true";
            ContentType = Feature.DownloadData["ContentType"];
            string contentTypes;
            if(Feature.DownloadData.TryGetValue("ContentTypes", out contentTypes))
                ContentTypes = contentTypes.Split(';');
            Feature.DownloadData.TryGetValue("NamePattern", out NamePattern);
            Feature.DownloadData.TryGetValue("TagPattern", out TagPattern);
            Feature.DownloadData.TryGetValue("Default", out DefaultAsset);
        }

        public override void Update()
        {
            base.Update();
            if (!_ready)
            {
                if (CurrentRequest == null)
                {
                    CurrentRequest = new WWW(
                        String.Format("https://qkrisi.hu/ktane/gh_releases/{0}/{1}/{2}", Repo, ResultsPerPage,
                            PageNumber++));
                    return;
                }
                if (!CurrentRequest.isDone)
                    return;
                if(!string.IsNullOrEmpty(CurrentRequest.error))
                {
                    Debug.LogError(String.Format("Error while fetching GitHub releases for {0}: {1}", Repo, CurrentRequest.error));
                    _ready = true;
                    return;
                }
                var page = JsonConvert.DeserializeObject<ReleaseInfo[]>(CurrentRequest.text);
                var count = page.Length;
                if (!UseSourceCode)
                {
                    page = page.Select(r =>
                    {
                        r.assets = r.assets.Where(a =>
                            (a.content_type == ContentType || ContentTypes.Contains(a.content_type)) &&
                            (string.IsNullOrEmpty(NamePattern) ||
                             Regex.IsMatch(a.name, NamePattern, RegexOptions.IgnoreCase))).ToArray();
                        return r;
                    }).Where(r => r.assets.Length > 0).ToArray();
                }
                ReleaseCache[Feature.Name].AddRange(page);
                if (count < ResultsPerPage)
                {
                    VersionTags = Releases.Select(r => r.tag_name).Where(t =>
                            string.IsNullOrEmpty(TagPattern) || Regex.IsMatch(t, TagPattern, RegexOptions.IgnoreCase))
                        .ToArray();
                    _ready = true;
                }
                CurrentRequest = null;
            }
        }

        ~GithubReleaseHandler()
        {
            ((IDisposable)CurrentRequest).Dispose();
        }

        public GithubReleaseHandler(FeatureInfo feature) : base(feature)
        {
        }
    }
}