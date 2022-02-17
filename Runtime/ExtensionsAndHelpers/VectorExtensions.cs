using UnityEngine;

namespace StudioName.Runtime.ExtensionAndHelpers {
    /// <summary>
    /// Extends the <see cref="UnityEngine.Vector2"/>, <see cref="UnityEngine.Vector3"/> & <see cref="UnityEngine.Vector4"/>
    /// classes with additional functionality
    /// </summary>
    public static class VectorExtensions {
        
        #region Vector3

        /// <summary>
        /// Sets a new X value to the vector and returns it.
        /// </summary>
        /// <param name="v3">The Vector3.</param>
        /// <param name="x">New X value.</param>
        /// <returns></returns>
        public static Vector3 SetX(this Vector3 v3, float x) {
            return new Vector3(x, v3.y, v3.z);
        }

        /// <summary>
        /// Sets a new Y value to the vector and returns it.
        /// </summary>
        /// <param name="v3">The Vector3.</param>
        /// <param name="y">New Y value.</param>
        /// <returns></returns>
        public static Vector3 SetY(this Vector3 v3, float y) {
            return new Vector3(v3.x, y, v3.z);
        }

        /// <summary>
        /// Sets a new Z value to the vector and returns it.
        /// </summary>
        /// <param name="v3">The Vector3.</param>
        /// <param name="z">New Z value.</param>
        /// <returns></returns>
        public static Vector3 SetZ(this Vector3 v3, float z) {
            return new Vector3(v3.x, v3.y, z);
        }
        
        /// <summary>
        /// Adds X to vector.x, adds Y value to vector.y, adds Z to vector.z and returns the new Vector3.
        /// </summary>
        /// <param name="v3">The Vector3.</param>
        /// <param name="x">Value to be added to X.</param>
        /// <param name="y">Value to be added to Y.</param>
        /// <param name="z">Value to be added to Z.</param>
        /// <returns></returns>
        public static Vector3 AddXYZ(this Vector3 v3, float x, float y, float z) {
            return new Vector3(v3.x + x, v3.y + y, v3.z + z);
        }
        
        #endregion
        
        #region Vector2

        /// <summary>
        /// Sets a new X value to the vector and returns it.
        /// </summary>
        /// <param name="v2">The Vector2.</param>
        /// <param name="x">New X value.</param>
        /// <returns></returns>
        public static Vector2 SetX(this Vector2 v2, float x) {
            return new Vector2(x, v2.y);
        }

        /// <summary>
        /// Sets a new Y value to the vector and returns it.
        /// </summary>
        /// <param name="v2">The Vector2.</param>
        /// <param name="y">New Y value.</param>
        /// <returns></returns>
        public static Vector2 SetY(this Vector2 v2, float y) {
            return new Vector2(v2.x, y);
        }

        /// <summary>
        /// Adds X to vector.x, adds Y value to vector.y and returns the new Vector2.
        /// </summary>
        /// <param name="v2">The Vector2.</param>
        /// <param name="x">Value to be added to X.</param>
        /// <param name="y">Value to be added to Y.</param>
        /// <returns></returns>
        public static Vector2 AddXY(this Vector2 v2, float x, float y) {
            return new Vector2(v2.x + x, v2.y + y);
        }

        #endregion
    }
}