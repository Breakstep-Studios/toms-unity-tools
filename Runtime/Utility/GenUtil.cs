using System;
using System.Collections;
using UnityEngine;

namespace StudioName.Runtime.Utility {

    //TODO Update GenUtil so that comments are in xml format
    public static class GenUtil {

        public static float HALF = 2f;
        public static float screenHeightInPoints = 2.0f * Camera.main.orthographicSize;
        public static float screenWidthInPoints = screenHeightInPoints * Camera.main.aspect;
        public static float HALF_SCREEN_HEIGHT = screenHeightInPoints / HALF;
        public static float HALF_SCREEN_WIDTH = screenWidthInPoints / HALF;

        public static float GetScreenBottomYPos() { return Camera.main.transform.position.y - HALF_SCREEN_HEIGHT; }
        public static float GetScreenTopYPos() { return Camera.main.transform.position.y + HALF_SCREEN_HEIGHT; }
        public static float GetScreenLeftXPos() { return Camera.main.transform.position.x - HALF_SCREEN_WIDTH; }
        public static float GetScreenRightXPos() { return Camera.main.transform.position.x + HALF_SCREEN_WIDTH; }
    
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
        /// <returns></returns>
        public static float MapRange(float value, Vector2 fromRange, Vector2 toRange) {
            return (value - fromRange.x) * (toRange.x - toRange.y) / (fromRange.x - fromRange.y) + toRange.x;
        }
    
        //well swap any types with each other ( numbers, collider, something usermade)
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
        /// <returns></returns>
        public static Vector3 GetDirectionalVector(Vector3 startPoint, Vector3 endPoint) {
            return new Vector3( endPoint.x - startPoint.x , endPoint.y - startPoint.y, endPoint.z - startPoint.z );
        }
        

        //Gets the absolute distance between two floating point numbers
        public static float AbsoluteDistance(float numOne, float numTwo) {
            return Mathf.Abs((Mathf.Abs(numOne) - Mathf.Abs(numTwo)));
        }

        //this function will allow us to execute a specific funciton in a the desired amount of time with one argument T and return type A
        public static IEnumerator Wait<T, A>(Func<T, A> func2Execute, T funcArg, float waitTimeInSeconds) {

            yield return new WaitForSeconds(waitTimeInSeconds);
            func2Execute(funcArg);

        }

        //this function will allow us to execute a specific funciton in a the desired amount of time with one argument T and no return type
        public static IEnumerator Wait<T>(Action<T> func2Execute, T funcArg, float waitTimeInSeconds) {

            yield return new WaitForSeconds(waitTimeInSeconds);
            func2Execute(funcArg);

        }

        //this function will allow us to execute a specific funciton in a the desired amount of time with no arguments and no return type
        public static IEnumerator Wait(Action func2Execute, float waitTimeInSeconds) {

            yield return new WaitForSeconds(waitTimeInSeconds);
            func2Execute();
        
        }

        /// <summary>
        /// Ensures the func2Evaluate is true before executing the func2Execute
        /// </summary>
        /// <param name="func2Evaluate">The function that must be true to continue</param>
        /// <param name="func2Execute">The function to execute after the func2Evaluate is true</param>
        /// <returns>An enumerator to call StartCoroutine() with</returns>
        public static IEnumerator WaitUntilTrue(Func<bool> func2Evaluate, Action func2Execute) {
            yield return new WaitUntil(func2Evaluate);
            func2Execute();
        }
        

        
        /// <summary>
        /// Ensures the func2Evaluate is true before executing the func2Execute
        /// </summary>
        /// <param name="func2Evaluate">The function that must be true to continue</param>
        /// <param name="func2Execute">The function to execute after the func2Evaluate is true</param>
        /// <typeparam name="T">The return type of our func2Execute</typeparam>
        /// <returns>An enumerator to call StartCoroutine() with</returns>
        public static IEnumerator WaitUntilTrue<T>(Func<bool> func2Evaluate, Func<T> func2Execute) {
            yield return new WaitUntil(func2Evaluate);
            func2Execute();
        }
    }
}
