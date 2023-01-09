using UnityEngine;

namespace StudioName.Runtime.ExtensionAndHelpers
{
    /// <summary>
    /// Retrieves a cached version of our Cloud Build manifest data rather then loading the manifest file over and over again.
    /// </summary>
    public static class CloudBuildManifestDataHelper 
    {
        private static CloudBuildManifestData cachedManifestData;
        
        /// <summary>
        /// A cached version of our cloud build manifest data. Ensures we don't keep reloading the manifest data from file
        /// </summary>
        public static CloudBuildManifestData CachedManifestData 
        {
            get 
            {
                if (cachedManifestData != null) {
                    return cachedManifestData;
                }
                
                var manifest = (TextAsset) Resources.Load("UnityCloudBuildManifest.json");
                if (manifest != null) {
                    cachedManifestData = JsonUtility.FromJson<CloudBuildManifestData>(manifest.text);
                    return cachedManifestData;
                }

                cachedManifestData = new CloudBuildManifestData();
                Debug.LogWarning("Unable to find / load Cloud Build manifest data...");
                return cachedManifestData;
            }
        }

        /// <summary>
        /// Data relating to the finished Unity Cloud Build 
        /// </summary>
        public class CloudBuildManifestData 
        {
            public string cloudBuildTargetName;
            public string buildNumber;
            public string scmCommitId;
            public string scmBranch;
            public string buildStartTime;
            public string projectId;
            public string bundleId;
            public string xcodeVersion;
            public string unityVersion;
            
            public const string DEFAULT_VALUE_NAME = "unknown";
            
            public CloudBuildManifestData() 
            {
                cloudBuildTargetName = DEFAULT_VALUE_NAME;
                buildNumber = DEFAULT_VALUE_NAME;
                scmCommitId = DEFAULT_VALUE_NAME;
                scmBranch = DEFAULT_VALUE_NAME;
                buildStartTime = DEFAULT_VALUE_NAME;
                projectId = DEFAULT_VALUE_NAME;
                bundleId = DEFAULT_VALUE_NAME;
                xcodeVersion = DEFAULT_VALUE_NAME;
                unityVersion = DEFAULT_VALUE_NAME;
            }
        }
    }
}