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
        /// Extension for <see cref="string.IsNullOrWhiteSpace"/>. Returns true if the string is null or contains whitespace, false otherwise.
        /// <remarks>See https://stackoverflow.com/q/18710644/1480728</remarks>
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str) {
            return string.IsNullOrWhiteSpace(str);
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
        
        /// <summary>
        /// Capitalizes the first letter of a string
        /// </summary>
        /// <param name="input">The string to capitalize.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if our string is null</exception>
        /// <exception cref="ArgumentException">Throw if our string is empty</exception>
        public static string FirstCharToUpper(this string input)
        {
            switch (input) {
                case null:
                    throw new ArgumentNullException(nameof(input));
                case "":
                    throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default:
                    return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }

    }
}