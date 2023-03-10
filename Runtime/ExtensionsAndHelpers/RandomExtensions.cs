using System;

namespace StudioName.Runtime.ExtensionAndHelpers
{
    /// <summary>
    /// Extends the <see cref="Random"/> class with additional functionality
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Returns a random double between min and max inclusive
        /// </summary>
        /// <param name="random">the random number generator to use</param>
        /// <param name="min">The minimum number to generate from</param>
        /// <param name="max">The maximum number to generate to</param>
        /// <returns>A random number between <see cref="min"/> and <see cref="max"/> inclusive</returns>
        public static double NextDouble(this Random random, double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
        
        /// <summary>
        /// Returns a random float between min and max inclusive
        /// </summary>
        /// <param name="random">the random number generator to use</param>
        /// <param name="min">The minimum number to generate from</param>
        /// <param name="max">The maximum number to generate to</param>
        /// <returns>A random number between <see cref="min"/> and <see cref="max"/> inclusive</returns>
        public static float NextFloat(this Random random, float min, float max)
        {
            return (float)random.NextDouble(min, max);
        }
    }
}