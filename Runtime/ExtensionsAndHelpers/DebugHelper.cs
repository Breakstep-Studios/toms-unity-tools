using UnityEngine;

namespace StudioName.Runtime.ExtensionAndHelpers {
    /// <summary>
    /// Extends the <see cref="UnityEngine.Debug"/> class with additional functionality
    /// </summary>
    public static class DebugHelper{
        /// <summary>
        /// Draws a + at the specified point.
        /// </summary>
        /// <param name="point">The position at which we will draw our plus.</param>
        /// <param name="duration">How long will the plus be displayed.</param>
        /// <param name="color">Color of our plus. Defaults to green.</param>
        /// <param name="size">The size of our plus.</param>
        public static void DrawPoint(Vector3 point, float duration, Color? color = null, float size = 0.05f) {
            color = color ?? Color.green;
            Debug.DrawLine(point,point + new Vector3(size+0.01f,0,0),(Color)color, duration);
            Debug.DrawLine(point,point + new Vector3(-(size+0.01f),0,0),(Color)color, duration);
            Debug.DrawLine(point,point + new Vector3(0,size+0.01f,0),(Color)color, duration);
            Debug.DrawLine(point,point + new Vector3(0,-(size+0.01f),0),(Color)color, duration);
        }
    }
}