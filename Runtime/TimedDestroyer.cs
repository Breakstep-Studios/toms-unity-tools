using System;
using System.Threading.Tasks;
using UnityEngine;

namespace StudioName.Runtime
{
    /// <summary>
    /// Destroys gameobjects after a given amount of item
    /// </summary>
    public class TimedDestroyer : MonoBehaviour
    {
        /// <summary>
        /// The time from our gameobjects creation till when it will be destroyed.
        /// </summary>
        [Tooltip("The time from our gameobjects creation till when it will be destroyed.")]
        public float timeTillDestroy;
        
        private async void Awake()
        {
            await Task.Delay(TimeSpan.FromSeconds(timeTillDestroy));
            //dirty fix for when our level already destroys the gameobject before the delay is finished
            if (this == null)
            {
                return;
            }
            Destroy(gameObject);
        }
    }
}