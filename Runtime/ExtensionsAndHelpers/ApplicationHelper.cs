using UnityEngine;

namespace StudioName.Runtime.ExtensionAndHelpers
{
    /// <summary>
    /// Extends the <see cref="UnityEngine.Application"/> class with additional functionality
    /// </summary>
    public static class ApplicationHelper
    {
        /// <summary>
        /// Returns the runtime platform resolved by the current build settings
        /// <see href="https://discussions.unity.com/t/check-if-my-platform-is-android-in-the-editor/130027/4"/>
        /// <remarks>IMPORTANT! Only Standalone and Android and IOS platforms are implemented for now!</remarks>
        /// </summary>
        public static RuntimePlatform ResolvedPlatform
        {
            get
            {
#if UNITY_STANDALONE_WIN
                return RuntimePlatform.WindowsPlayer;
#elif UNITY_STANDALONE_OSX
                return RuntimePlatform.OSXPlayer;
#elif UNITY_STANDALONE_LINUX
                return RuntimePlatform.LinuxPlayer;
#elif UNITY_ANDROID
                return RuntimePlatform.Android;
#elif UNITY_IOS
                return RuntimePlatform.IPhonePlayer;
#else
                return Application.platform;
#endif
            }
        }
    }
}