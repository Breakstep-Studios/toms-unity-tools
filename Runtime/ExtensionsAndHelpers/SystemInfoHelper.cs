using UnityEngine;
using StudioName.Runtime.Security.IOS;

namespace StudioName.Runtime.ExtensionAndHelpers {

    /// <summary>
    /// Class used to easily print various pieces of info about the current system in use
    /// </summary>
    public static class SystemInfoHelper {

        /// <summary>
        /// Prints the current system os, os family, device model, and unique device id
        /// </summary>
        public static void PrintSystemInfo(){
            Debug.Log("Operating System: " + SystemInfo.operatingSystem);
            Debug.Log("Operating System Family: " + SystemInfo.operatingSystemFamily);
            Debug.Log("Device Model: " + SystemInfo.deviceModel);
            Debug.Log("Device Unique Identifier: " + SystemInfo.deviceUniqueIdentifier);
        }
        
        /// <summary>
        /// Either retrieves a previously set deviceId, or if one isn't present, sets one and returns it
        /// </summary>
        /// <returns>The current deviceID set in the storage KeyChain</returns>
        public static KeyChainUserData GetIosDeviceIDFromKeyChain() {
#if UNITY_IOS
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
	    
    }

}