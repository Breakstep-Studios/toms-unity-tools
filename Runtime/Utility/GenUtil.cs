using System;
using System.Collections;
using UnityEngine;

namespace StudioName.Runtime.Utility {

    /// <summary>
    /// Contains general methods and properties that make our life easier when coding.
    /// </summary>
    /// TODO the methods / properties below should be placed in to more specific categories if one is found
    public static class GenUtil {
        private const float HALF = 2f;
        
        public static float screenHeightInPoints = 2.0f * Camera.main.orthographicSize;
        public static float screenWidthInPoints = screenHeightInPoints * Camera.main.aspect;
        public static float HALF_SCREEN_HEIGHT = screenHeightInPoints / HALF;
        public static float HALF_SCREEN_WIDTH = screenWidthInPoints / HALF;

        public static float GetScreenBottomYPos() {
            return Camera.main.transform.position.y - HALF_SCREEN_HEIGHT;
        }

        public static float GetScreenTopYPos() {
            return Camera.main.transform.position.y + HALF_SCREEN_HEIGHT;
        }

        public static float GetScreenLeftXPos() {
            return Camera.main.transform.position.x - HALF_SCREEN_WIDTH;
        }

        public static float GetScreenRightXPos() {
            return Camera.main.transform.position.x + HALF_SCREEN_WIDTH;
        }
    
        public static void RecalculateScreenHeightAndWidth() {
            screenHeightInPoints = 2.0f * Camera.main.orthographicSize;
            screenWidthInPoints = screenHeightInPoints * Camera.main.aspect;
            HALF_SCREEN_HEIGHT = screenHeightInPoints / HALF;
            HALF_SCREEN_WIDTH = screenWidthInPoints / HALF;
        }

        /// <summary>
        /// Input put a value and it will remap it from the range given to the range given
        /// Example say if I have startRange [50,100] and endRange [200,400] and I input 75 (half of 50 to 100) this function will output 300 because it is half of the new range.
        /// effectively mapping on number to another
        /// </summary>
        /// <param name="value">the value to remap to the toRange</param>
        /// <param name="fromRange">the range the value is coming from</param>
        /// <param name="toRange">the range the value wants to be remapped to</param>
        /// <returns>The value now in <see cref="toRange"/></returns>
        /// TODO maybe this should also be part of the FloatHelper class
        public static float MapRange(float value, Vector2 fromRange, Vector2 toRange) {
            return (value - fromRange.x) * (toRange.x - toRange.y) / (fromRange.x - fromRange.y) + toRange.x;
        }
    
        /// <summary>
        /// Swaps two reference types with one another
        /// </summary>
        /// <param name="itemA">Item one involved in swap</param>
        /// <param name="itemB">Item2 two involved in swap</param>
        /// <typeparam name="T">The type of item we are swapping</typeparam>
        public static void Swap<T>( ref T itemA,  ref T itemB){
            T temp;
            temp = itemA;
            itemA = itemB;
            itemB = temp;
        }

        /// <summary>
        /// Gets the directional vector between 2 points 
        /// </summary>
        /// <param name="startPoint"> point where your directional vector will start</param>
        /// <param name="endPoint"> point where your directional vector will end</param>
        /// <returns>The difference of the two vectors</returns>
        /// TODO is this even necessary? And does the name represent what it does give it's not normalized?
        /// TODO see https://forum.unity.com/threads/getting-the-direction-of-a-vector.2103/
        public static Vector3 GetDirectionalVector(Vector3 startPoint, Vector3 endPoint) {
            //I believe this is the same as return endPoint - startPoint;
            return new Vector3( endPoint.x - startPoint.x , endPoint.y - startPoint.y, endPoint.z - startPoint.z );
        }
        
        /// <summary>
        /// Gets the absolute distance between two floating point numbers
        /// </summary>
        /// <param name="numOne">The first number to compare</param>
        /// <param name="numTwo">The second number to compare</param>
        /// <returns>The total distance between those two values</returns>
        /// TODO potentially move this to a FloatHelper class
        public static float AbsoluteDistance(float numOne, float numTwo) {
            return Mathf.Abs((Mathf.Abs(numOne) - Mathf.Abs(numTwo)));
        }
    }
}
