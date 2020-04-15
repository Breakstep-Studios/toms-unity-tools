namespace StudioName.Runtime.Animation {
    
    /// <summary>
    /// Defines whether an animation should be blended through overriding or through addition
    /// </summary>
    public enum BlendType {
        /// <summary>
        /// The animation will be overriden by another.
        /// </summary>
        Set=1,
        /// <summary>
        /// The animation will be added to another.
        /// </summary>
        Add=2
    }
}