namespace StudioName.Runtime.IO {
    /// <summary>
    /// Used for easy handling of on & offline data with various DataService classes.
    /// </summary>
    /// <typeparam name="T">The type of offline data that we will read/write to.</typeparam>
    public abstract class DataService<T> : IOfflineDataHandleable<T> {
        /// <summary>
        /// The contiainer that will be used to store our offline data
        /// </summary>
        protected T offlineDataContainer;
        /// <summary>
        /// A simple flag set when @see InitOFflineHandling has been called;
        /// </summary>
        private bool isOfflineHandlingInit = false;
        
        public virtual void InitOfflineHandling(T offlineDataContainer) {
            this.offlineDataContainer = offlineDataContainer;
            isOfflineHandlingInit = true;
        }

        public bool ShouldProccessOfflineData {
            get { return isOfflineHandlingInit; }
        }
    }
}