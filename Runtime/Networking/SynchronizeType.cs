namespace StudioName.Runtime.Networking {

    /// <summary>
    /// Defines they synchronization type we will use in our networking model.
    /// </summary>
    public enum SynchronizeType{
        /// <summary>
        /// Nothing will be synchronized.
        /// </summary>
        Disabled = 0,
        /// <summary>
        /// Synchronized only when things change.
        /// </summary>
        Discrete = 1,
        /// <summary>
        /// Synchronized repeatedly.
        /// </summary>
        Continuous = 2
    }
    
}