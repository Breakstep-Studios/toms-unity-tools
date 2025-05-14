using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StudioName.Runtime.ExtensionAndHelpers
{
    /// <summary>
    /// Adds helper methods for dealing with Unity <see cref="GameObject"/>
    /// </summary>
    public static class GameObjectHelper
    {
        /// <summary>
        /// Returns all GameObjects in the scene with the given tag in a deterministic order.
        /// This differs from <see cref="GameObject.FindGameObjectsWithTag"/> in that, that method will return tagged objects in arbitrary order.
        /// <remarks>This method is useful when loading a scene twice and wanting to compare objects against one another.</remarks>
        /// <example>
        /// Load scene and call <see cref="FindGameObjectsWithTagDeterministicOrder"/>[0] reload scene and call <see cref="FindGameObjectsWithTagDeterministicOrder"/>[0]
        /// Given the above example [0].transform.position == [0].transform.position (same object different instance)
        /// </example>
        /// </summary>
        /// <param name="tag">The tag we will filter for</param>
        /// <returns>A deterministically ordered list of the gameobjects in the scene</returns>
        /// TODO not sure I like the way this method looks.
        /// TODO GetRootGameObjects doesn't specifically state that the list is deterministically ordered, this may be a problem later!
        public static GameObject[] FindGameObjectsWithTagDeterministicOrder(string tag)
        {
            // Get a list of all root GameObjects in the scene
            var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects().ToList();

            // Create a list to hold the found GameObjects
            var foundGameObjects = new List<GameObject>();

            // Loop through each root GameObject
            foreach (var rootGameObject in rootGameObjects)
            {
                // Call recursive function to traverse all children
                TraverseChildren(rootGameObject.transform, tag, foundGameObjects);
            }

            return foundGameObjects.ToArray();
        }
        
        /// <summary>
        /// Returns all GameObjects in the scene with the given layer in a deterministic order.
        /// <remarks>This method is useful when loading a scene twice and wanting to compare objects against one another.</remarks>
        /// <example>
        /// Load scene and call <see cref="FindGameObjectsWithLayerDeterministicOrder"/>[0] reload scene and call <see cref="FindGameObjectsWithLayerDeterministicOrder"/>[0]
        /// Given the above example [0].transform.position == [0].transform.position (same object different instance)
        /// </example>
        /// </summary>
        /// <param name="layer">The layer we will filter for</param>
        /// <returns>A deterministically ordered list of the gameobjects in the scene</returns>
        /// TODO not sure I like the way this method looks.
        /// TODO GetRootGameObjects doesn't specifically state that the list is deterministically ordered, this may be a problem later!
        public static GameObject[] FindGameObjectsWithLayerDeterministicOrder(string layer)
        {
            var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects().ToList();
            var foundGameObjects = new List<GameObject>();

            foreach (var rootGameObject in rootGameObjects)
            {
                TraverseChildrenByLayer(rootGameObject.transform, layer, foundGameObjects);
            }

            return foundGameObjects.ToArray();
        }

        /// <summary>
        /// Allows us to recursively traverse all children of a given <see cref="Transform"/> and add any GameObjects with the given tag to a list
        /// </summary>
        /// <param name="parent">the transform parent to recurse</param>
        /// <param name="tag">the tag we will add gameobjects for</param>
        /// <param name="foundGameObjects">the list of found gameobjects</param>
        private static void TraverseChildren(Transform parent, string tag, List<GameObject> foundGameObjects)
        {
            // Check parent GameObject
            if (parent.CompareTag(tag))
            {
                foundGameObjects.Add(parent.gameObject);
            }

            // Loop through and recursively check all children
            foreach (Transform child in parent)
            {
                TraverseChildren(child, tag, foundGameObjects);
            }
        }
        
        /// <summary>
        /// Allows us to recursively traverse all children of a given <see cref="Transform"/> and add any GameObjects with the given layer to a list
        /// </summary>
        /// <param name="parent">the transform parent to recurse</param>
        /// <param name="layer">the layer we will add gameobjects for</param>
        /// <param name="foundGameObjects">the list of found gameobjects</param>
        private static void TraverseChildrenByLayer(Transform parent, string layer, List<GameObject> foundGameObjects)
        {
            if (parent.gameObject.layer == LayerMask.NameToLayer(layer))
            {
                foundGameObjects.Add(parent.gameObject);
            }

            foreach (Transform child in parent)
            {
                TraverseChildrenByLayer(child, layer, foundGameObjects);
            }
        }
    }
}