using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

public partial class CommunityFeaturesDownloader
{
    #pragma warning disable 649
    
        [Serializable]
        private class FeatureInfo
        {
            [Serializable]
            public class ZipTarget
            {
                public enum ZipTargetType
                {
                    Directory,
                    File
                }
                
                [JsonProperty(Required = Required.Always)] public string Location { get; set; }
                [JsonProperty(Required = Required.Always)] public string Target { get; set; }
                [JsonProperty(Required = Required.Always)] public ZipTargetType TargetType { get; set; }
            }
            
            [JsonProperty(Required = Required.Always)] public string Name { get; set; }
            [JsonProperty(Required = Required.Always)] public string Author { get; set; }
            [JsonProperty(Required = Required.Always)] public string Description { get; set; }
            public Dictionary<string, string> Links { get; set; }
            [JsonProperty(Required = Required.Always)] public bool Integration { get; set; }
            [JsonProperty(Required = Required.Always)] public FileVendor Vendor { get; set; }
            [JsonProperty(Required = Required.Always)] public Dictionary<string, string> DownloadData { get; set; }
            [JsonProperty(Required = Required.Always)] public DownloadType FileType { get; set; }
            public Dictionary<string, string> FileData { get; set; }
            public ZipTarget[] ZipTargets { get; set; }
            
            
            public FeatureHandler Handler { get; private set; }

            [OnDeserialized]
            private void Init(StreamingContext _)
            {
                Handler = (FeatureHandler)Activator.CreateInstance(FeatureHandlerTypes[Vendor], this);
                Handler.Fetch();
            }
        }
        
        [Serializable]
        private class ReleaseInfo
        {
            [Serializable]
            public class AssetInfo
            {
                [JsonProperty(Required = Required.Always)] public string name { get; set; }
                [JsonProperty(Required = Required.Always)] public string browser_download_url { get; set; }
                [JsonProperty(Required = Required.Always)] public string content_type { get; set; }
                [JsonProperty(Required = Required.Always)] public ulong size { get; set; }

                public string ConvertedSize { get; private set; }

                [OnDeserialized]
                private void ConvertSize(StreamingContext _)
                {
                    ConvertedSize = Convert(size);
                }
            }
            
            [JsonProperty(Required = Required.Always)] public string tag_name { get; set; }
            [JsonProperty(Required = Required.Always)] public AssetInfo[] assets { get; set; }
        }

        [Serializable]
        private class DownloadInfo
        {
            [JsonProperty(Required = Required.Always)] public string Name { get; set; }
            [JsonProperty(Required = Required.Always)] public string Info { get; set; }
            [JsonProperty(Required = Required.Always)] public string[] Files { get; set; }
        }
    #pragma warning restore 649
}
