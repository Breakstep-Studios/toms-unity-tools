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

        /// <summary>
        /// Converts the current presumed screen space bounds to world space bounds
        /// </summary>
        /// <param name="bounds">The presumed screen space bounds</param>
        /// <param name="camera">The camera used for the transformation</param>
        /// <returns>The bounds now in world space</returns>
        public static Bounds ConvertToWorldBounds(this Bounds bounds, Camera camera)
        {
            var topRightWorldPosition =
                camera.ScreenToWorldPoint(bounds.center + bounds.extents);
            var bottomLeftWorldPosition =
                camera.ScreenToWorldPoint(bounds.center - bounds.extents);
            var worldPositionBoundsSize = topRightWorldPosition - bottomLeftWorldPosition; 
            return new Bounds(bottomLeftWorldPosition + worldPositionBoundsSize / 2,
                worldPositionBoundsSize);
        }
        
        /// <summary>
        /// Converts the current presumed world space bounds to screen space bounds
        /// </summary>
        /// <param name="bounds">The presumed world space bounds</param>
        /// <param name="camera">The camera used for the transformation</param>
        /// <returns>The bounds now in screen space</returns>
        public static Bounds ConvertToScreenBounds(this Bounds bounds, Camera camera)
        {
            var topRightWorldPosition =
                camera.WorldToScreenPoint(bounds.center + bounds.extents);
            var bottomLeftWorldPosition =
                camera.WorldToScreenPoint(bounds.center - bounds.extents);
            var worldPositionBoundsSize = topRightWorldPosition - bottomLeftWorldPosition; 
            return new Bounds(bottomLeftWorldPosition + worldPositionBoundsSize / 2,
                worldPositionBoundsSize);
        }
        
    }
}