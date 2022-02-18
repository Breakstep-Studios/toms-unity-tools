using System;
using System.Collections;
using UnityEngine;

namespace StudioName.Runtime.ExtensionAndHelpers {
    /// <summary>
    /// Adds helper methods when dealing with <see cref="IEnumerator"/>  
    /// </summary>
    public static class IEnumeratorHelpers {
        /// <summary>
        /// Waits a specified period of time and then executes the given function
        /// </summary>
        /// <param name="func2Execute">The function we will execute after the given time has elapsed.</param>
        /// <param name="funcArg">The argument we will pass in to our executing function.</param>
        /// <param name="waitTimeInSeconds">The time in seconds we will wait until executing the function.</param>
        /// <typeparam name="T1">The type of our function argument.</typeparam>
        /// <typeparam name="T2">The result of our function argument</typeparam>
        /// <returns>IEnumerator representing our time we will wait.</returns>
        public static IEnumerator Wait<T1, T2>(Func<T1, T2> func2Execute, T1 funcArg, float waitTimeInSeconds) {
            yield return new WaitForSeconds(waitTimeInSeconds);
            func2Execute(funcArg);
        }

        /// <summary>
        /// Waits a specified period of time and then executes the given function
        /// </summary>
        /// <param name="func2Execute">The function we will execute after the given time has elapsed.</param>
        /// <param name="funcArg">The argument we will pass in to our executing function.</param>
        /// <param name="waitTimeInSeconds">The time in seconds we will wait until executing the function.</param>
        /// <typeparam name="T">The type of our function argument.</typeparam>
        /// <returns>IEnumerator representing our time we will wait.</returns>
        public static IEnumerator Wait<T>(Action<T> func2Execute, T funcArg, float waitTimeInSeconds) {
            yield return new WaitForSeconds(waitTimeInSeconds);
            func2Execute(funcArg);
        }

        /// <summary>
        /// Waits a specified period of time and then executes the given function
        /// </summary>
        /// <param name="func2Execute">The function we will execute after the given time has elapsed.</param>
        /// <param name="waitTimeInSeconds">The time in seconds we will wait until executing the function.</param>
        /// <returns>IEnumerator representing our time we will wait.</returns>
        public static IEnumerator Wait(Action func2Execute, float waitTimeInSeconds) {
            yield return new WaitForSeconds(waitTimeInSeconds);
            func2Execute();
        }

        /// <summary>
        /// Waits until one  function is true and then executes another function
        /// </summary>
        /// <param name="func2Evaluate">The function that must be true in order for <see cref="func2Execute"/> to execute</param>
        /// <param name="func2Execute">The function we will execute after the given time has elapsed.</param>
        /// <returns>IEnumerator representing our time we will wait.</returns>
        public static IEnumerator WaitUntilTrue(Func<bool> func2Evaluate, Action func2Execute) {
            yield return new WaitUntil(func2Evaluate);
            func2Execute();
        }
        
        /// <summary>
        /// Waits until one  function is true and then executes another function
        /// </summary>
        /// <param name="func2Evaluate">The function that must be true in order for <see cref="func2Execute"/> to execute</param>
        /// <param name="func2Execute">The function we will execute after the given time has elapsed.</param>
        /// <typeparam name="T">The type of our <see cref="func2Execute"/> argument.</typeparam>
        /// <returns>IEnumerator representing our time we will wait.</returns>
        public static IEnumerator WaitUntilTrue<T>(Func<bool> func2Evaluate, Func<T> func2Execute) {
            yield return new WaitUntil(func2Evaluate);
            func2Execute();
        }
    }
}