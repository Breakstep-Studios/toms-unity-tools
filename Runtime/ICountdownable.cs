namespace StudioName.Runtime {
    /// <summary>
    /// Defines parameters that you might find associated with a countdown timer
    /// </summary>
    public interface ICountdownable {
        /// <summary>
        /// The current amount of time that has passed
        /// </summary>
        float ElapsedTime { get; }
        /// <summary>
        /// When <see cref="ElapsedTime"/> == <see cref="EndTime"/> will be considered completed
        /// </summary>
        float EndTime { get; }
        /// <summary>
        /// True if <see cref="ElapsedTime"/> is being incremented or decremented.
        /// False if <see cref="ElapsedTime"/> >= <see cref="EndTime"/>
        /// </summary>
        bool IsRunning { get; }
    }
}