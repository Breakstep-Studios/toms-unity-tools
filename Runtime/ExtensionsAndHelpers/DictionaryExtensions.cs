using System.Collections.Generic;

namespace StudioName.Runtime.ExtensionAndHelpers {
    /// <summary>
    /// Extends the <see cref="Dictionary{TKey,TValue}"/> class with additional functionality
    /// </summary>
    public static class DictionaryExtensions {
        /// <summary>
        /// Returns the value if present, otherwise returns the default value specified
        /// </summary>
        /// <param name="dict">Dictionary to operate on</param>
        /// <param name="key">Key to search for</param>
        /// <param name="defaultIfNotFound">The default value to use if key is not found</param>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <returns></returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key,
            TValue defaultIfNotFound = default(TValue)) {
            
            TValue value;
            // value will be the result or the default for TValue
            if (!dict.TryGetValue(key, out value)) {
                value = defaultIfNotFound;
            }
            return value;
        }
        
    }
}