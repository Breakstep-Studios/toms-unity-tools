using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StudioName.Runtime.ExtensionAndHelpers
{
    /// <summary>
    /// Extends the <see cref="Scene"/> class with additional functionality
    /// </summary>
    public static class SceneExtensions
    {
        /// <summary>
        /// Get all the components of the type specified in the scene
        /// </summary>
        /// <param name="scene">The scene we are acting on</param>
        /// <typeparam name="T">The component type we want to retrieve</typeparam>
        /// <returns>The components of the given type in the scene</returns>
        public static T[] GetAllComponentsOfType<T>(this Scene scene) where T : Component
        {
            
            var rootGameObjects = scene.GetRootGameObjects();
            var components = new List<T>();
            foreach (var rootGameObject in rootGameObjects)
            {
                components.AddRange(rootGameObject.GetComponentsInChildren<T>(true));
            }
            return components.ToArray();
        }
        
        /// <summary>
        /// Moves all the root gameobjects from the current scene to the destination scene
        /// </summary>
        /// <param name="scene">The scene to move the root gameobjects FROM</param>
        /// <param name="destinationScene">The scene to move the root gameobjects TO</param>
        public static void MoveRootGameObjectsToScene(this Scene scene, Scene destinationScene)
        {
            var rootGameObjects = scene.GetRootGameObjects();
            foreach (var rootGameObject in rootGameObjects)
            {
                SceneManager.MoveGameObjectToScene(rootGameObject, destinationScene);
            }
        }
        
    }
}