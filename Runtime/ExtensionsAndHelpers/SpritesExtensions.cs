using UnityEngine;

namespace StudioName.Runtime.ExtensionAndHelpers {
    /// <summary>
    /// Extends the <see cref="UnityEngine.Sprite"/> class with additional functionality
    /// </summary>
    public static class SpritesExtensions {
        /// <summary>
        /// This method will return the sprites pivot as a decimal of it's whole size
        /// For example a sprite with size 100, 100 and pivot 50, 50 will return (0.5, 0.5)
        /// </summary>
        /// <param name="spriteToCalc">The Sprite to calculate pivot from</param>
        public static Vector2 GetPivotFracAmount(this Sprite spriteToCalc) {
            return new Vector2(spriteToCalc.pivot.x / (spriteToCalc.bounds.size.x * spriteToCalc.pixelsPerUnit), 
                spriteToCalc.pivot.y / (spriteToCalc.bounds.size.y * spriteToCalc.pixelsPerUnit));
        }
    }

}
