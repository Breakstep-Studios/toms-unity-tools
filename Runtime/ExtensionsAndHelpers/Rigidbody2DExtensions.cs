using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudioName.Runtime.ExtensionAndHelpers
{
    /// <summary>
    /// Extends the <see cref="UnityEngine.Rigidbody2D"/> class with additional functionality
    /// </summary>
    public static class Rigidbody2DExtensions
    {
        /// <summary>
        /// Sets the isKinematic property to true and our velocity to zero
        /// </summary>
        /// <param name="rigidbody2D">Rigidbody2D to calculate from</param>
        public static void EnableKinematicAndStop(this Rigidbody2D rigidbody2D)
        {
            rigidbody2D.isKinematic = true;
            rigidbody2D.velocity = Vector2.zero;
        }
    }
}