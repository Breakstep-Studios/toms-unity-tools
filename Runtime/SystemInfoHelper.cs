using UnityEngine;

namespace StudioName.Runtime {

    /// <summary>
    /// Class used to easily print various pieces of info about the current system in use
    /// </summary>
    public abstract class SystemInfoHelper{

        /// <summary>
        /// Prints the current system os, os family, device model, and unique device id
        /// </summary>
        public static void PrintSystemInfo(){
            Debug.Log("Operating System: " + SystemInfo.operatingSystem);
            Debug.Log("Operating System Family: " + SystemInfo.operatingSystemFamily);
            Debug.Log("Device Model: " + SystemInfo.deviceModel);
            Debug.Log("Device Unique Identifier: " + SystemInfo.deviceUniqueIdentifier);
        }
    }

}