using System;
using UnityEngine;

namespace StudioName.Runtime.ExtensionAndHelpers {
    
    /// <summary>
    /// Extends the <see cref="UnityEngine.Camera"/> class with additional functionality
    /// </summary>
    public static class CameraExtensions {

        /// <summary>
        /// Gets the orthographic size of the perspective camera at a given depth
        /// </summary>
        /// <param name="camera">Camera to calculate from</param>
        /// <param name="distance">The distance from the origin of the camera we will traverse before calculating ortho size</param>
        /// <returns>The orthographic size at point</returns>
        public static float GetOrthographicSizeAtDistance(this Camera camera, float distance)
        {
            return GetOrthographicSizeAtDistance(camera.fieldOfView, distance);
        }

        /// <summary>
        /// Gets the orthographic size of the perspective camera at a given depth
        /// </summary>
        /// <param name="fieldOfView">the field of view to calculate the ortho size from</param>
        /// <param name="distance">The distance from the origin of the camera we will traverse before calculating ortho size</param>
        /// <returns>The orthographic size at point</returns>
        public static float GetOrthographicSizeAtDistance(float fieldOfView, float distance)
        {
            return distance * Mathf.Tan(fieldOfView * 0.5f * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Returns the camera bounds that are being used given the camera <see cref="Camera.GateFitMode"/> setting
        /// </summary>
        /// <param name="camera">Camera to calculate from</param>
        /// <param name="distanceToDesiredBounds">The distance to the desired orthographic size that we will fetch</param>
        /// <param name="fieldOfView">if input will utilize this field of view for bounds calculation rather then current camera fov</param>
        /// <param name="boundsCenter">The point that will be considered the center of the returned bounds in world space otherwise the current camera position</param>
        /// <returns>The camera bounds at the desired position of our frustum given the gate fit setting</returns>
        public static Bounds GetPhysicalCameraBoundsForGateFit(this Camera camera, float distanceToDesiredBounds,
            float? fieldOfView = null, Vector3? boundsCenter = null)
        {
            //works with overscan settings
            var sensorAspectRatio = camera.sensorSize.x / camera.sensorSize.y;
            var gameViewAspectRatio = (float)Screen.width/Screen.height;
            var reciprocalGameViewAspectRatio = 1 / gameViewAspectRatio;
            var orthoSizeAtDesiredDistance =
                GetOrthographicSizeAtDistance(fieldOfView: fieldOfView ?? camera.fieldOfView, Mathf.Abs(distanceToDesiredBounds));
            var boundsDepth = 0.1f;

            // Sensor View Bounds
            var sensorViewBoundsSize =
                new Vector3(orthoSizeAtDesiredDistance * 2 * sensorAspectRatio, orthoSizeAtDesiredDistance * 2, boundsDepth); 
            
            // Screen Bounds Constrained Width
            var screenBoundsConstrainedWidthSize =
                new Vector3(sensorViewBoundsSize.x, sensorViewBoundsSize.x * reciprocalGameViewAspectRatio, boundsDepth); 
            
            // Screen Bounds Constrained Height
            var screenBoundsConstrainedHeightSize =
                new Vector3(sensorViewBoundsSize.y * gameViewAspectRatio, sensorViewBoundsSize.y, boundsDepth); 

            //display correct bounds after gate fit
            Vector3 physicalCameraBoundsSize;
            switch (camera.gateFit)
            {
                case Camera.GateFitMode.Vertical:
                    if (sensorAspectRatio > gameViewAspectRatio)
                    {
                        physicalCameraBoundsSize = FindRequestedBounds(screenBoundsConstrainedWidthSize,
                            screenBoundsConstrainedHeightSize, Measurement.Width, -1);
                    }
                    else
                    {
                        physicalCameraBoundsSize = FindRequestedBounds(screenBoundsConstrainedWidthSize,
                            screenBoundsConstrainedHeightSize, Measurement.Width, 1);
                    }

                    break;
                case Camera.GateFitMode.Horizontal:
                    if (sensorAspectRatio > gameViewAspectRatio)
                    {
                        physicalCameraBoundsSize = FindRequestedBounds(screenBoundsConstrainedWidthSize,
                            screenBoundsConstrainedHeightSize, Measurement.Width, 1);
                    }
                    else
                    {
                        physicalCameraBoundsSize = FindRequestedBounds(screenBoundsConstrainedWidthSize,
                            screenBoundsConstrainedHeightSize, Measurement.Width, -1);
                    }

                    break;
                case Camera.GateFitMode.Fill:
                    physicalCameraBoundsSize = FindRequestedBounds(screenBoundsConstrainedWidthSize,
                        screenBoundsConstrainedHeightSize, Measurement.Height, -1);
                    break;
                case Camera.GateFitMode.Overscan:
                    physicalCameraBoundsSize = FindRequestedBounds(screenBoundsConstrainedWidthSize,
                        screenBoundsConstrainedHeightSize, Measurement.Height, 1);
                    break;
                case Camera.GateFitMode.None:
                    physicalCameraBoundsSize = sensorViewBoundsSize;
                    break;
                default:
                    physicalCameraBoundsSize = sensorViewBoundsSize;
                    break;
            }

            return new Bounds(boundsCenter ?? camera.transform.position ,physicalCameraBoundsSize);;
        }
        
        /// <summary>
        /// Finds the bounds that match the given criteria given two input bounds 
        /// </summary>
        /// <param name="boundsOne">The first of two bounds we will compare against.</param>
        /// <param name="boundsTwo">The second of two bounds we will compare against.</param>
        /// <param name="measurement">If we should measure the width or the height of the bounds</param>
        /// <param name="comparisonCheck">-1 if boundsOne is less then boundsTwo; 0 if boundsOne equals boundsTwo; 1 if boundsOne greater then boundsTwo</param>
        /// <returns>The bounds that meets the given criteria. Example comparisonCheck = -1 measurement = width returns bounds with the smallest width</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static Vector3 FindRequestedBounds(Vector3 boundsOne, Vector3 boundsTwo, Measurement measurement,
            int comparisonCheck)
        {
            comparisonCheck = Mathf.Clamp(comparisonCheck, -1, 1);
            switch (measurement)
            {
                case Measurement.Width:
                    switch (comparisonCheck)
                    {
                        case -1 when boundsOne.x < boundsTwo.x:
                            return boundsOne;
                        case 0 when Mathf.Abs(boundsOne.x - boundsTwo.x) < 0.01f:
                            return boundsOne;
                        case 1 when boundsOne.x > boundsTwo.x:
                            return boundsOne;
                        default:
                            return boundsTwo;
                    }
                case Measurement.Height:
                    switch (comparisonCheck)
                    {
                        case -1 when boundsOne.y < boundsTwo.y:
                            return boundsOne;
                        case 0 when Mathf.Abs(boundsOne.y - boundsTwo.y) < 0.01f:
                            return boundsOne;
                        case 1 when boundsOne.y > boundsTwo.y:
                            return boundsOne;
                        default:
                            return boundsTwo;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(measurement), measurement, null);
            }
        }
        
        /// <summary>
        /// Enum used for calculating which bounds is bigger or smaller in <see cref="CameraExtensions.FindRequestedBounds"/> 
        /// </summary>
        private enum Measurement
        {
            Width,
            Height
        }
    }
}