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
        /// Returns the camera bounds at the given <see cref="distanceToDesiredBounds"/> and a field of view.
        /// <para>This method will also account for physical camera properties including <see cref="Camera.GateFitMode"/> and sensor size.</para>
        /// </summary>
        /// <param name="camera">Camera to calculate from</param>
        /// <param name="distanceToDesiredBounds">The distance to the desired orthographic size that we will fetch</param>
        /// <param name="fieldOfView">if input will utilize this field of view for bounds calculation rather then current camera fov</param>
        /// <param name="boundsCenter">The point that will be considered the center of the returned bounds in world space otherwise the current camera position</param>
        /// <returns>The camera bounds at the desired position of our frustum given the gate fit setting</returns>
        public static Bounds GetPerspectiveCameraBounds(this Camera camera, float distanceToDesiredBounds,
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

            // no physical camera properties so just return the gameViewAspectRatio bounds at the given distance
            // (this is what screenBoundsConstrainedHeightSize is)
            if (!camera.usePhysicalProperties)
            {
                return new Bounds(boundsCenter ?? camera.transform.position ,screenBoundsConstrainedHeightSize);;
            }
            
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
        /// Returns the field of view necessary to have <see cref="unitsOfHeight"/> fill the screen height completely
        /// at the provided <see cref="distance"/>.
        /// </summary>
        /// <param name="camera">The camera we will get necessary calculation info from</param>
        /// <param name="unitsOfHeight">The target units of height we want to fill the screen</param>
        /// <param name="distance">The distance to the units of height.</param>
        /// <returns>
        /// The field of view necessary for <see cref="unitsOfHeight"/> to fill the screen provided the units are
        /// at the specified <see cref="distance"/>
        /// </returns>
        public static float GetFOVForUnitsOfHeightAtDistance(this Camera camera, float unitsOfHeight, float distance)
        {
            // If physical properties are not enabled, use the default calculation without any adjustments
            if (!camera.usePhysicalProperties)
            {
                return CalculateVerticalFOVForHeight(unitsOfHeight, distance);
            }
            
            // Use the physical sensor's aspect ratio if physical properties are enabled
            var sensorAspectRatio = camera.sensorSize.x / camera.sensorSize.y;
            // Adjust to compensate for sensor vs screen aspect ratio differences
            var sensorAdjustment = sensorAspectRatio / camera.aspect; 

            // Adjust unitsOfHeight based on gate fit and computed values
            float adjustedHeight;
            switch (camera.gateFit)
            {
                case Camera.GateFitMode.Horizontal:
                    // Horizontal gate fit adjusts the height to ensure the full width is visible
                    adjustedHeight = unitsOfHeight * sensorAdjustment;
                    break;

                case Camera.GateFitMode.Vertical:
                    // Vertical gate fit means height should not be adjusted
                    adjustedHeight = unitsOfHeight;
                    break;

                case Camera.GateFitMode.Fill:
                    // Fill requires choosing the larger value to make sure the entire screen is filled without gaps
                    adjustedHeight = Mathf.Max(unitsOfHeight, unitsOfHeight * sensorAdjustment);
                    break;

                case Camera.GateFitMode.Overscan:
                    // Overscan chooses the smaller value to make sure everything fits, even if there's padding
                    adjustedHeight = Mathf.Min(unitsOfHeight, unitsOfHeight * sensorAdjustment);
                    break;

                default:
                    // Default to vertical fit if none specified
                    adjustedHeight = unitsOfHeight;
                    break;
            }

            return CalculateVerticalFOVForHeight(adjustedHeight, distance);
            
            // Calculate the field of view required to have heightUnits vertically fill the screen at the given distance
            float CalculateVerticalFOVForHeight(float heightUnits, float distanceToUnitsOfHeight)
            {
                // Calculate the field of view required for this adjusted height at the given distance
                return Mathf.Atan2(heightUnits, distanceToUnitsOfHeight) * Mathf.Rad2Deg * 2;
            }
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