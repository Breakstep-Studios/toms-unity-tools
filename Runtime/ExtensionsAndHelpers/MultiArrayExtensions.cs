using System.Collections.Generic;
using System.Linq;

namespace StudioName.Runtime.ExtensionAndHelpers {
    /// <summary>
    /// Extends Multidimensional arrays T[,] with additional functionality
    /// <remarks>This should not confused with jagged arrays of the type T[][]</remarks>
    /// </summary>
    public static class MultiArrayExtensions {
        /// <summary>
        /// Trim the empty left, right, top & bottom rows off the array
        /// </summary>
        /// <param name="arrayToTrim"></param>
        /// <returns>The trimmed array</returns>
        public static T[,] TrimArray<T>(this T[,] arrayToTrim) where T : class {
            var array = arrayToTrim;
            var patternWidth = arrayToTrim.GetLength(0);
            var patternHeight = arrayToTrim.GetLength(1);
            var columnsToRemove = new List<int>();
            var rowsToRemove = new List<int>();
        
            TrimLeft:
            for (var x = 0; x < patternWidth; ++x) {
                for (var y = 0; y < patternHeight; ++y) {
                    var validItem = arrayToTrim[x, y];
                    if (validItem != null) {
                        goto TrimRight;
                    }
                    if (y == patternHeight - 1) {
                        columnsToRemove.Add(x);
                    }
                }
            }
            
            TrimRight:
            for (var x = patternWidth-1; x >= 0; --x) {
                for (var y = 0; y < patternHeight; ++y) {
                    var validItem = arrayToTrim[x, y];
                    if (validItem != null) {
                        goto TrimTop;
                    }
                    if (y == patternHeight - 1) {
                        columnsToRemove.Add(x);
                    }
                }
            }
            
            TrimTop:
            for (var y = 0; y < patternWidth; ++y) {
                for (var x = 0; x < patternWidth; ++x) {
                    var validItem = arrayToTrim[x, y];
                    if (validItem != null) {
                        goto TrimBottom;
                    }
                    if (x == patternWidth - 1) {
                        rowsToRemove.Add(y);
                    }
                }
            }

            TrimBottom:
            for (var y = patternHeight-1; y >= 0; --y) {
                for (var x = 0; x < patternWidth; ++x) {
                    var validItem = arrayToTrim[x, y];
                    if (validItem != null) {
                        goto TrimFinish;
                    }
                    if (x == patternWidth - 1) {
                        rowsToRemove.Add(y);
                    }
                }
            }
            
            TrimFinish:
            columnsToRemove = columnsToRemove.OrderByDescending(i => i).ToList();
            foreach (var column in columnsToRemove) {
                array = TrimColumn(column,array);
            }
            rowsToRemove = rowsToRemove.OrderByDescending(i => i).ToList();
            foreach (var row in rowsToRemove) {
                array = TrimRow(row,array);
            }
            return array;
        }
        
        /// <summary>
        /// Trim a row from an array
        /// </summary>
        /// <param name="rowToRemove">The row index to remove</param>
        /// <param name="originalArray">The array to trim the row from</param>
        /// <typeparam name="T">The type of array we are trimming a row from</typeparam>
        /// <returns>The array with the row removed</returns>
        private static T[,] TrimRow<T>(int rowToRemove, T[,] originalArray)
        {
            var result = new T[originalArray.GetLength(0), originalArray.GetLength(1) - 1];
            for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
            {
                for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
                {
                    if (k == rowToRemove) {
                        continue;
                    }
                    result[j, u] = originalArray[i, k];
                    u++;
                }
                j++;
            }
            return result;
        }
        
        /// <summary>
        /// Trim a column from an array
        /// </summary>
        /// <param name="columnToRemove">The column index to remove</param>
        /// <param name="originalArray">The array to trim the column from</param>
        /// <typeparam name="T">The type of array we are trimming a column from</typeparam>
        /// <returns>The array with the column removed</returns>
        private static T[,] TrimColumn<T>(int columnToRemove, T[,] originalArray)
        {
            var result = new T[originalArray.GetLength(0) - 1, originalArray.GetLength(1)];
            for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
            {
                if (i == columnToRemove) {
                    continue;
                }
                for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
                {
                    result[j, u] = originalArray[i, k];
                    u++;
                }
                j++;
            }
            return result;
        }
    }
}