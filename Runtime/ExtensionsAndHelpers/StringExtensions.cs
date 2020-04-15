using System;
using System.Linq;

namespace StudioName.Runtime.ExtensionAndHelpers {
    /// <summary>
    /// Extends c# <see cref="string"/> class with additional functionality
    /// </summary>
    public static class StringExtensions {
        /// <summary>
        /// Extension for <see cref="string.IsNullOrEmpty"/>. Returns true if the string is null or empty, false otherwise.
        /// </summary>
        public static bool IsNullOrEmpty(this string str) {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Converts a camelCase/CamelCase string to a string separated by spaces (camel Case/Camel Case)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RevertCamelCase(this string str) {
            return string.Concat(str.Select(
                x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }
    }
}