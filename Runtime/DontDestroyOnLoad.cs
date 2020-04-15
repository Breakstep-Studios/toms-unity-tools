using UnityEngine;

namespace StudioName.Runtime {
    /// <summary>
    /// Simple utility script that will prevent a GameObject from being destroyed
    /// </summary>
    public class DontDestroyOnLoad : MonoBehaviour {
        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }
    }
}