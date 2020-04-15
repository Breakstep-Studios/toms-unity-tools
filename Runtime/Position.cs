namespace StudioName.Runtime {
    /// <summary>
    /// Represents a simple position on a 2D plane
    /// </summary>
    //TODO Might want to refactor into SimplePosition later
    public enum Position {
        /// <summary>
        /// The position is above the middle
        /// </summary>
        Top,
        /// <summary>
        /// The position is below the middle
        /// </summary>
        Bottom,
        /// <summary>
        /// The position is to the left of the middle
        /// </summary>
        Left,
        /// <summary>
        /// The position is to the right of the middle
        /// </summary>
        Right
    }
}