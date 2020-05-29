using UnityEngine;

namespace StudioName.Runtime.ExtensionAndHelpers {
    
    /// <summary>
    /// Adds helper methods for dealing with Unity UI Canvas
    /// </summary>
    public static class UICanvasHelpers
    {
        /// <summary>
        /// Similar to <see cref="WorldToUIPoint"/> but the final position will be relative to the <see cref="parentCanvas"/>
        /// <para>This means we can work with the point using someRectTransform.anchoredPosition</para> 
        /// </summary>
        /// <param name="objectCamera">The camera rendering the object we want to convert.</param>
        /// <param name="targetCanvas">The canvas where the object point should lie on</param>
        /// <param name="objectPosition">The object position we want to convert</param>
        /// <returns>The <see cref="objectPosition"/> in world space converted for use with anchoredPosition</returns>
        public static Vector3 WorldToUIAnchoredPosition(Camera objectCamera, Canvas targetCanvas,
            Vector3 objectPosition)
        {
            var worldPositionOnCanvas = WorldToUIPoint(objectCamera, targetCanvas, objectPosition);
            return WorldToViewportAnchoredPosition(targetCanvas, worldPositionOnCanvas);
        }
        
        /// <summary>
        /// Converts an objects position from world space to UI space.
        /// <para>Note the final point will still be in world space, it will just be world space on the <see cref="targetCanvas"/> provided.</para>
        /// <para>In order to use this function with rect transform you must use rectTransform.transform.position</para>
        /// </summary>
        /// <param name="objectCamera">The camera rendering the object we want to convert.</param>
        /// <param name="targetCanvas">The canvas where the object point should lie on</param>
        /// <param name="objectPosition">The object position we want to convert</param>
        /// <returns>The <see cref="objectPosition"/> in world space now on the <see cref="targetCanvas"/> provided</returns>
        public static Vector3 WorldToUIPoint(Camera objectCamera, Canvas targetCanvas, Vector3 objectPosition)
        {
            //convert object world position to screen position for use with ScreenPointToLocalPointInRectangle function
            var objectScreenPoint = objectCamera.WorldToScreenPoint(objectPosition);

            //convert the object screen position to position on our canvas rectangle (Local Position)
            RectTransformUtility.ScreenPointToLocalPointInRectangle(targetCanvas.transform as RectTransform,
                objectScreenPoint, targetCanvas.worldCamera, out var objectCanvasLocalPosition);

            //convert local position on our rectangle to world position
            var worldPositionOnCanvas = targetCanvas.transform.TransformPoint(objectCanvasLocalPosition);

            return worldPositionOnCanvas;
        }
        
        /// <summary>
        /// Convert a world position to an anchored position on a our given <see cref="targetCanvas"/>
        /// </summary>
        /// <param name="targetCanvas">The canvas where the object point should lie on</param>
        /// <param name="objectPosition">The object position we want to convert</param>
        /// <returns>The <see cref="objectPosition"/> in world space converted for use with anchoredPosition</returns>
        public static Vector3 WorldToViewportAnchoredPosition(Canvas targetCanvas, Vector3 objectPosition)
        {
            var canvasRectTransform = targetCanvas.GetComponent<RectTransform>();
     
            //calculate the position of the UI element
            //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left
            //corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.
            var viewportPosition = targetCanvas.worldCamera.WorldToViewportPoint(objectPosition);
            var sizeDelta = canvasRectTransform.sizeDelta;
            var worldObjectScreenPosition = new Vector2(
                ((viewportPosition.x * sizeDelta.x) - (sizeDelta.x * 0.5f)),
                ((viewportPosition.y * sizeDelta.y) - (sizeDelta.y * 0.5f)));
            
            return worldObjectScreenPosition;
        }
    }
    
}