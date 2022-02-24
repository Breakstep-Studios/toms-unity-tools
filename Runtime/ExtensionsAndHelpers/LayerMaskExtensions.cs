using UnityEngine;

namespace StudioName.Runtime.ExtensionAndHelpers
{
    /// <summary>
    /// Extends the <see cref="UnityEngine.LayerMask"/> class with additional functionality
    /// </summary>
    public static class LayerMaskExtensions
    {
        /// <summary>
        /// Is the layer contained in the layer mask?
        /// </summary>
        /// <param name="layerMask">The layer mask to test against</param>
        /// <param name="layer">The layer we will check</param>
        /// <returns>True if layer is contained, false otherwise</returns>
        public static bool Contains(this LayerMask layerMask, int layer)
        {
            return (layerMask.value & 1 << layer) > 0;
        }
    }
}