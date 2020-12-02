using System.Collections;
using System.Threading.Tasks;

namespace StudioName.Runtime.ExtensionAndHelpers
{
    /// <summary>
    /// Extends the <see cref="Task"/> class with additional functionality
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Converts a <see cref="System.Threading.Tasks.Task"/> to <see cref="IEnumerator"/> (helps with unity coroutines)
        /// </summary>
        /// <param name="task">The task to convert</param>
        /// <returns>An IEnumerator which waits till the task has been completed</returns>
        public static IEnumerator AsIEnumerator(this Task task)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (task.IsFaulted)
            {
                throw task.Exception;
            }
        }
        
        /// <inheritdoc cref="AsIEnumerator"/>
        public static IEnumerator AsIEnumerator<T>(this Task<T> task)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (task.IsFaulted)
            {
                throw task.Exception;
            }
        }
    }
}