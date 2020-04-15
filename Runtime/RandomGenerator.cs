using System;

namespace StudioName.Runtime {
    
    /// <summary>
    /// Generates a random int based on a seed, or system timer if no seed is supplied.
    /// </summary>
    //TODO template class this way we always just call next and it returns appropriate random type.
    public class RandomGenerator {
        private Random random;
        private int seed = 0;

        /// <summary>
        /// Generates a random number seeded from the system timer.
        /// </summary>
        public RandomGenerator() {
            seed = Environment.TickCount;
            random = new Random(seed);
        }

        /// <summary>
        /// Generates a random number based on the seed given
        /// </summary>
        /// <param name="seed">The int from which to seed from</param>
        public RandomGenerator(int seed) {
            random = new Random(seed);
            this.seed = seed;
        }

        /// <summary>
        /// Generates a random integer based on the seed
        /// </summary>
        /// <returns>Random integer generated</returns>
        public int Next() {
            return random.Next();
        }

        /// <summary>
        /// Generates a random integer between 0 and the max value (exclusive) based on the seed
        /// </summary>
        /// <param name="maxValue">The max value (exclusive)</param>
        /// <returns>Random integer generated</returns>
        public int Next( int maxValue) {
            return random.Next(maxValue);
        }

        /// <summary>
        /// Generates a random double based on the seed
        /// </summary>
        /// <returns>Random double generated</returns>
        public double NextDouble() {
            return random.NextDouble();
        }

        /// <summary>
        /// Generates a random integer between min and max (inclusive, exclusive respectively) based on the seed
        /// </summary>
        /// <param name="min">The min value (inclusive)</param>
        /// <param name="max">The max value (exclusive)</param>
        /// <returns>Random integer generated</returns>
        public int Range(int min, int max) {
            return random.Next(min, max);
        }

        /// <summary>
        /// Generates a random float between min and max (inclusive, exclusive respectively) based on the seed
        /// </summary>
        /// <param name="min">The min value (inclusive)</param>
        /// <param name="max">The max value (exclusive)</param>
        /// <returns></returns>
        public float Range( float min, float max) {
            return (float)random.NextDouble() * (max - min) + min;
        }

        /// <summary>
        /// The seed from which to generate random numbers from. ( Set = new Random(value) )
        /// </summary>
        public int Seed {
            set { seed = value; random = new Random(value); }
            get { return seed; }
        }
    }
}