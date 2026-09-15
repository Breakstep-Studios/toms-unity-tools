using System.IO;
using UnityEngine;
using StudioName.Runtime.Security.IOS;
using System;

namespace StudioName.Runtime.ExtensionAndHelpers {

    /// <summary>
    /// Class used to easily print various pieces of info about the current system in use
    /// </summary>
    public static class SystemInfoHelper {

        /// <summary>
        /// Prints the current system os, os family, device model, and unique device id
        /// </summary>
        public static void PrintSystemInfo() {
            Debug.Log("Operating System: " + SystemInfo.operatingSystem);
            Debug.Log("Operating System Family: " + SystemInfo.operatingSystemFamily);
            Debug.Log("Device Model: " + SystemInfo.deviceModel);
            Debug.Log("Device Unique Identifier: " + SystemInfo.deviceUniqueIdentifier);
        }
        
        /// <summary>
        /// Check if the project is symbolic linked by checking if Assets & ProjectSettings folders have Sym Link data.
        /// <para>When project is not run in Unity Editor will always return false!</para>
        /// </summary>
        /// <param name="checkAssets">
        /// Should we check the Unity Assets folder to determine if the
        /// project is Symbolic Linked?
        /// </param>
        /// <param name="checkProjectSettings">
        /// Should we check the Unity Project Settigns folder to determine if the project is Symbolic Linked?
        /// </param>
        /// <returns>True if testing folders have Sym link data, false otherwise.</returns>
        public static bool IsProjectSymbolicLinked(bool checkAssets = true, bool checkProjectSettings = true) {
#if !UNITY_EDITOR
                return false;
#endif
            
            if (!checkAssets && !checkProjectSettings) {
                checkAssets = true;
                checkProjectSettings = true;
                Debug.LogWarning("CheckAssets & CheckProjectSettings both set to false, defaulting them to true.");
            }
            
            var assetsPathInfo = new FileInfo(Application.dataPath);
            var assetsIsSymbolic = assetsPathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint);
            var projectSettingsPathInfo = new FileInfo(
                Application.dataPath.Substring(0,Application.dataPath.Length-6) + "ProjectSettings");
            var projectSettingsIsSymbolic = projectSettingsPathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint);

            if (!checkAssets)
            {
                return projectSettingsIsSymbolic;
            }

            if (!checkProjectSettings)
            {
                return assetsIsSymbolic;
            }
            
            return assetsIsSymbolic && projectSettingsIsSymbolic;
        }
        
        /// <summary>
        /// Either retrieves a previously set deviceId, or if one isn't present, sets one and returns it
        /// </summary>
        /// <returns>The current deviceID set in the storage KeyChain</returns>
        public static KeyChainUserData GetIosDeviceIDFromKeyChain() {
#if UNITY_IOS && !UNITY_EDITOR
				KeyChainUserData userData = JsonUtility.FromJson<KeyChainUserData>(KeyChain.BindGetKeyChainUser());
				if (userData.uuid == "") {
					Debug.Log("No deviceId found on keychain...creating new one - " + SystemInfo.deviceUniqueIdentifier);
					KeyChain.BindSetKeyChainUser("0", SystemInfo.deviceUniqueIdentifier);
					userData = JsonUtility.FromJson<KeyChainUserData>(KeyChain.BindGetKeyChainUser());
				} else {
					Debug.Log("DeviceId found - " + userData.uuid);
				}
				return userData;
#else
            KeyChainUserData userData = new KeyChainUserData {
                userId = "0", 
                uuid = SystemInfo.deviceUniqueIdentifier
            };
            Debug.Log("DeviceId found - " + userData.uuid + ". (Using SystemInfo.deviceID to allow Editor testing)");
            return userData;
#endif
        }

		/// <summary>
		/// Either retrieves a previously generated deviceId from PlayerPrefs, or if one isn't present, generates one, saves it and returns it.
		/// <para>Use on platforms where <see cref="SystemInfo.deviceUniqueIdentifier"/> is unavailable or unstable (e.g. WebGL).</para>
		/// </summary>
		/// <returns>The deviceId stored in PlayerPrefs</returns>
		public static string GetDeviceIdFromPlayerPrefs() {
		    // Changing this key after release will orphan existing ids.
		    const string deviceIdPlayerPrefsKey = "StudioName.DeviceId";
		
		    var deviceId = PlayerPrefs.GetString(deviceIdPlayerPrefsKey, "");
		    if (string.IsNullOrEmpty(deviceId)) {
		        deviceId = Guid.NewGuid().ToString("N");
		        PlayerPrefs.SetString(deviceIdPlayerPrefsKey, deviceId);
		        PlayerPrefs.Save();
		        Debug.Log("No deviceId found in PlayerPrefs...creating new one - " + deviceId);
		    } else {
		        Debug.Log("DeviceId found - " + deviceId);
		    }
		    return deviceId;
		}
	    
    }

}
