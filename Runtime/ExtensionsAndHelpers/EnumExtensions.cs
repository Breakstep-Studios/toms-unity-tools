using System;
using System.Collections.Generic;

namespace StudioName.Runtime.ExtensionAndHelpers
{
    /// <summary>
    /// Extends the <see cref="Enum"/> class with additional functionality
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Get all the unique flags from an enum
        /// </summary>
        /// <param name="flags">The enum with flags</param>
        /// <typeparam name="T">THe enums type</typeparam>
        /// <returns>The enum values that have been set</returns>
        public static IEnumerable<T> GetUniqueFlags<T>(this T flags) where T : Enum
        {
            foreach (Enum value in Enum.GetValues(flags.GetType()))
            {
                if (flags.HasFlag(value))
                {
                    yield return (T)value;
                }
            }
        }
    }
}