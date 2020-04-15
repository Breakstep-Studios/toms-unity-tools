namespace StudioName.Runtime {
    /// <summary>
    /// Used to easily define both a left and right bound
    /// </summary>
    public class HorizontalBounds {
        /// <summary>
        /// The left point of the vertical bound
        /// </summary>
        private readonly float leftBound;
        /// <summary>
        /// The right point of the vertical bound
        /// </summary>
        private readonly float rightBound;
        
        public HorizontalBounds(float leftBound = 0, float rightBound = 0) {
            this.leftBound = leftBound;
            this.rightBound = rightBound;
        }

        /// <summary>
        /// Is the current value contained within the horizontal bound
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <returns>True if value is contained, false otherwise</returns>
        public bool Contains(float value) {
            return leftBound >= value && value <= rightBound;
        }
        
        public float LeftBound {
            get { return leftBound; }
        }

        public float RightBound {
            get { return rightBound; }
        }
    }
}