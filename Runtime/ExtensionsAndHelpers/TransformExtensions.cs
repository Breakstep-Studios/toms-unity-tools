using UnityEngine;

namespace StudioName.Runtime.ExtensionAndHelpers
{
    /// <summary>
    /// Extends the <see cref="UnityEngine.Transform"/> class with additional functionality
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Increases the scale uniformly across the x & the y axis (Leave z scale untouched)
        /// </summary>
        /// <param name="transform">The Transform to act on</param>
        /// <param name="scalar">The value we will multiply our current x & y scales by</param>
        public static void UniformScale2D(this Transform transform, float scalar)
        {
            var localScale = transform.localScale;
            transform.localScale = new Vector3(localScale.x * scalar, localScale.y * scalar, localScale.z);
        }

        /// <summary>
        /// Sets the <see cref="scale"/> value to both the x & y localScale params leaving z intact
        /// </summary>
        /// <param name="transform">The Transform to act on</param>
        /// <param name="scale">The value that we will set our x & y scales to</param>
        public static void SetUniformScale2D(this Transform transform, float scale)
        {
            transform.localScale = new Vector3(scale, scale, transform.localScale.z);
        }
    }
}