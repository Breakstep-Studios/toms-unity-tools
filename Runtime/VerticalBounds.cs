namespace StudioName.Runtime {
    /// <summary>
    /// Used to easily define both a top and bottom bound
    /// </summary>
    public class VerticalBounds {
        private readonly float topBound;
        private readonly float bottomBound;
        
        public VerticalBounds(float topBound = 0, float bottomBound = 0) {
            this.topBound = topBound;
            this.bottomBound = bottomBound;
        }

        /// <summary>
        /// Is the current value contained within the vertical bound
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <returns>True if value is contained, false otherwise</returns>
        public bool Contains(float value) {
            return topBound >= value && value <= bottomBound;
        }
        
        /// <summary>
        /// The top point of the vertical bound
        /// </summary>
        public float TopBound {
            get { return topBound; }
        }
        
        /// <summary>
        /// The bottom point of the vertical bound
        /// </summary>
        public float BottomBound {
            get { return bottomBound; }
        }
    }
}