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
        
        /// <summary>
        /// A simple way to ensure our errors are shown in unity
        /// see http://www.stevevermeulen.com/index.php/2017/09/using-async-await-in-unity3d-2017/ (search WrapErrors())
        /// </summary>
        /// <param name="task">The task to wrap errors for</param>
        public static async void WrapErrors(this Task task)
        {
            await task;
        }
    }
}