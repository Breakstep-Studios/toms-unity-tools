using UnityEngine;

namespace StudioName.Runtime.Math.SimulatedPhysics {
    /// <summary>
    /// Simulates a projectile being launched with some initial velocity.
    /// <para>Used to extract information about a projectile movement without actually creating a projectile.</para>
    /// </summary>
    public class SimulatedProjectile {
        private Vector2 initialVelocity;
        private float gravity;
        
        public SimulatedProjectile(Vector2 initialVelocity, float gravity) {
            this.initialVelocity = initialVelocity;
            this.gravity = gravity;
        }

        /// <summary>
        /// The combined total velocity of the projectile derived from it's x and y velocity components
        /// <para>c = Sqrt(a^2 + b^2)</para>
        /// </summary>
        public float Velocity {
            get {
                return  Mathf.Sqrt( Mathf.Pow(initialVelocity.x, 2f) + Mathf.Pow(initialVelocity.y, 2f) );                
            }
        }

        /// <summary>
        /// Returns the Angle of velocity vector (angle originates at floor and moves upward)
        /// </summary>
        public float VectorAngle {
            get {
                return Mathf.Atan2(initialVelocity.y, initialVelocity.y) * Mathf.Rad2Deg;
            }
        }

        /// <summary>
        /// Return the Horizontal Distance of a Projectile from starting x to ending x at the same height it started at.
        /// </summary>
        public float HorizontalRange {
            get {
                return Mathf.Pow( Velocity, 2f ) * Mathf.Sin( VectorAngle * 2f * Mathf.Deg2Rad )  / 
                       Mathf.Abs( gravity ) ; 
            }
        }

        /// <summary>
        /// Gets Maximum Height of a Projectile ( the greatest height that the object will reach )
        /// </summary>
        public float MaximumHeight {
            get {
                return ( Mathf.Pow( Velocity, 2f ) * Mathf.Pow( Mathf.Sin( VectorAngle* Mathf.Deg2Rad ), 2f )) / 
                       (2 * Mathf.Abs(gravity));
            }
        }
    }
}