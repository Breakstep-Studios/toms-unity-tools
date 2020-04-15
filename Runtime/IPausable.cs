namespace StudioName.Runtime {
    /// <summary>
    /// Defines behavior that allows for starting and stopping
    /// </summary>
    public interface IPausable {
        /// <summary>
        /// Begin the process
        /// </summary>
        void Pause();
        /// <summary>
        /// End the process
        /// </summary>
        void UnPause();
        /// <summary>
        /// Check if the current process is paused
        /// </summary>
        bool IsPaused { get; }
    }
}