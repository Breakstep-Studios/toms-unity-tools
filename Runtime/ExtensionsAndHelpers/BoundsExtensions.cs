using UnityEngine;

namespace StudioName.Runtime.ExtensionAndHelpers {
    /// <summary>
    /// Extends the <see cref="UnityEngine.Bounds"/> class with additional functionality
    /// </summary>
    public static class BoundsExtensions {
        /// <summary>
        /// Check if a point is contained within the 2 dimensional space of the bounding box. Ignores z size.
        /// </summary>
        /// <param name="bounds">Bounds to calculate from</param>
        /// <param name="point">Point to test.</param>
        /// <returns>True if contained false otherwise</returns>
        public static bool Contains2D(this Bounds bounds, Vector2 point) {
            if (bounds.size.z <= 0.1f) {
                bounds.size += new Vector3(0,0,1);
            }
            return bounds.Contains(point);
        }
        
    }
}