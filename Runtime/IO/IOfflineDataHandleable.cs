namespace StudioName.Runtime.IO {
    /// <summary>
    /// A contract that a class will be able to handle offline data 
    /// using a specfic container to write and read from.
    /// </summary>
    /// <typeparam name="T">The type of data continer we will be writing and reading from.</typeparam>
    public interface IOfflineDataHandleable<T> {
        /// <summary>
        /// Allows us to easily check if we are in a state to process offline data.
        /// </summary>
        bool ShouldProccessOfflineData { get; }

        /// <summary>
        /// Initializes offline data with the data container to read and write from.
        /// </summary>
        /// <param name="offlineDataContainer">The offlline container to read/write our data to.</param>
        void InitOfflineHandling(T offlineDataContainer);

    }
}