using UnityEngine;

namespace StudioName.Runtime.ExtensionAndHelpers {
    /// <summary>
    /// Extends the <see cref="UnityEngine.Debug"/> class with additional functionality
    /// </summary>
    public static class DebugHelper {
        /// <summary>
        /// Draws a + at the specified point.
        /// </summary>
        /// <param name="point">The position at which we will draw our plus.</param>
        /// <param name="duration">How long will the plus be displayed.</param>
        /// <param name="color">Color of our plus. Defaults to green.</param>
        /// <param name="size">The size of our plus.</param>
        public static void DrawPoint(Vector3 point, float duration, Color? color = null, float size = 0.05f, bool makeStar = false) {
            color = color ?? Color.green;
            Debug.DrawLine(point,point + new Vector3(size+0.01f,0,0),(Color)color, duration);
            Debug.DrawLine(point,point + new Vector3(-(size+0.01f),0,0),(Color)color, duration);
            Debug.DrawLine(point,point + new Vector3(0,size+0.01f,0),(Color)color, duration);
            Debug.DrawLine(point,point + new Vector3(0,-(size+0.01f),0),(Color)color, duration);
            if (!makeStar) {
                return;
            }
            Debug.DrawLine(point,point + new Vector3(size+0.01f,size+0.01f,0),(Color)color, duration);
            Debug.DrawLine(point,point + new Vector3(-(size+0.01f),-(size+0.01f),0),(Color)color, duration);
            Debug.DrawLine(point,point + new Vector3(-(size+0.01f),size+0.01f,0),(Color)color, duration);
            Debug.DrawLine(point,point + new Vector3(size+0.01f,-(size+0.01f),0),(Color)color, duration);
        }

        /// <summary>
        /// Draws a rectangle around the given bounds
        /// </summary>
        /// <param name="bounds">The bounds to draw a rectangle around.</param>
        /// <param name="duration">How long will the plus be displayed.</param>
        /// <param name="color">Color of our plus. Defaults to green.</param>
        /// <param name="addX">Adds an x to the middle of the rectangle as well</param>
        public static void DrawBound(Bounds bounds, float duration, Color? color = null, bool addX = false) {
            var castedColor = color ?? Color.red;
            var topLeft = new Vector2(bounds.min.x, bounds.max.y);
            var topRight = bounds.max;
            var bottomLeft = bounds.min;
            var bottomRight = new Vector2(bounds.max.x, bounds.min.y);
            Debug.DrawLine(topLeft,topRight,castedColor,100);
            Debug.DrawLine(topRight,bottomRight,castedColor,100);
            Debug.DrawLine(bottomRight,bottomLeft,castedColor,100);
            Debug.DrawLine(bottomLeft,topLeft,castedColor,100);
            if (!addX) {
                return;
            }
            Debug.DrawLine(topLeft,bottomRight,castedColor,100);
            Debug.DrawLine(topRight,bottomLeft,castedColor,100);
        }
    }
}