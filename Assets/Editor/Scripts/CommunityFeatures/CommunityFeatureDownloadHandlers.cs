using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ionic.Zip;
using UnityEditor;
using UnityEngine;

public partial class CommunityFeaturesDownloader
{
    private enum DownloadType
    {
        Text,
        Binary,
        Zip
    }
    
    private static Dictionary<DownloadType, Type> DownloadHandlerTypes = new Dictionary<DownloadType, Type>
    {
        { DownloadType.Text, typeof(TextDownloader) },
        { DownloadType.Binary, typeof(BinaryDownloader) },
        { DownloadType.Zip, typeof(ZipDownloader) }
    };

    private struct DownloadProgress
    {
        public float Progress;
        public string ConvertedProgress;
    }

    private abstract class FeatureDownloadHandler
    {
        protected readonly FeatureInfo Feature;

        protected WWW Query { get; private set; }
        
        public string Filename;

        private ulong Bytes;
        private string ConvertedSize;
        private float LastProgress;
        private DownloadProgress LastConvertedProgress;

        public abstract string[] AffectedFiles { get; }

        public bool Ready
        {
            get
            {
                if (Query == null)
                    return true;
                if (Query.isDone)
                {
                    Finish();
                    Query = null;
                    return true;
                }
                return false;
            }
        }
        
        public DownloadProgress Progress
        {
            get
            {
                var progress = Query.progress;
                if (progress == LastProgress)
                    return LastConvertedProgress;
                DownloadProgress prog;
                prog.Progress = progress;
                prog.ConvertedProgress = String.Format("({0} / {1})", Convert((ulong)(Bytes * LastProgress)), ConvertedSize);
                LastProgress = progress;
                LastConvertedProgress = prog;
                return prog;
            }
        }

        public void Download(string URL, ulong bytes)
        {
            Query = new WWW(URL);
            Bytes = bytes;
            ConvertedSize = Convert(Bytes);
        }
        
        protected void EnsureDirectoryExists(string dir)
        {
            EnsureAbsoluteDirectoryExists(Path.Combine(DataPath, dir));
        }

        protected void Import(string path)
        {
            AssetDatabase.ImportAsset(Path.Combine("Assets", path), ImportAssetOptions.ImportRecursive);
        }
        
        protected abstract void Finish();

        ~FeatureDownloadHandler()
        {
            ((IDisposable)Query).Dispose();
        }
        
        protected FeatureDownloadHandler(FeatureInfo feature)
        {
            Feature = feature;
        }
    }

    private class TextDownloader : FeatureDownloadHandler
    {
        protected string FilePath
        {
            get
            {
                return Path.Combine(Feature.FileData["Location"], Filename);
            }
        }

        public override string[] AffectedFiles
        {
            get
            {
                return new[] { FilePath };
            }
        }

        protected override void Finish()
        {
            EnsureDirectoryExists(Path.GetDirectoryName(FilePath));
            File.WriteAllText(Path.Combine(DataPath, FilePath), Query.text);
            Import(FilePath);
        }

        public TextDownloader(FeatureInfo feature) : base(feature)
        {
        }
    }

    private class BinaryDownloader : TextDownloader
    {
        protected override void Finish()
        {
            EnsureDirectoryExists(Path.GetDirectoryName(FilePath));
            File.WriteAllBytes(Path.Combine(DataPath, FilePath), Query.bytes);
            Import(FilePath);
        }

        public BinaryDownloader(FeatureInfo feature) : base(feature)
        {
        }
    }

    private class ZipDownloader : FeatureDownloadHandler
    {
        private readonly string[] _affectedFiles;

        public override string[] AffectedFiles
        {
            get
            {
                return _affectedFiles;
            }
        }

        protected override void Finish()
        {
            var directory = Path.Combine(DataPath, "../Temp");
            var zip = Path.Combine(directory, Filename);
            NotImplementedException error = null;
            if(!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            File.WriteAllBytes(zip, Query.bytes);
            using (var file = ZipFile.Read(zip))
            {
                foreach(var target in Feature.ZipTargets)
                {
                    var target_location = target.Target;
                    var location = target.Location;
                    var TargetType = target.TargetType;
                    var filename = Path.GetFileName(target_location);
                    var modkitLocation = Path.Combine(DataPath, location);
                    EnsureDirectoryExists(location);
                    if (target_location == "*")
                    {
                        file.ExtractAll(modkitLocation, ExtractExistingFileAction.OverwriteSilently);
                        Import(location);
                    }
                    else switch (TargetType)
                    {
                        case FeatureInfo.ZipTarget.ZipTargetType.Directory:
                            EnsureDirectoryExists(location);
                            foreach (var e in file.Where(e => e.FileName.StartsWith(target_location)))
                                e.Extract(modkitLocation, ExtractExistingFileAction.OverwriteSilently);
                            Import(Path.Combine(location, filename));
                            break;
                        case FeatureInfo.ZipTarget.ZipTargetType.File:
                            var entry = file[target_location];
                            if (entry != null)
                            {
                                entry.FileName = filename;
                                entry.Extract(modkitLocation, ExtractExistingFileAction.OverwriteSilently);
                                Import(Path.Combine(location, filename));
                            }
                            break;
                        default:
                            error = new NotImplementedException("Unsupported ZIP target type: " + TargetType);
                            break;
                    }
                    if (error != null)
                        break;
                }
            } 
            File.Delete(zip);
            if (error != null)
                throw error;
        }

        public ZipDownloader(FeatureInfo feature) : base(feature)
        {
            _affectedFiles = Feature.ZipTargets.Select(t => Path.Combine(t.Location, Path.GetFileName(t.Target))).ToArray();
        }
    }
}