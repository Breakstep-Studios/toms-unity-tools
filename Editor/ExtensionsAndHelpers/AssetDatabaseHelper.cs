using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace StudioName.Editor.ExtensionAndHelpers
{
    public static class AssetDatabaseHelper
    {
        /// <summary>
        /// Loads all assets at the given path including sub folders
        /// </summary>
        /// <param name="path">The path to search from (including sub folders of this path)</param>
        /// <returns>A list of objects contained within this folder and subfolders</returns>
        public static List<Object> RecursivelyLoadAllAssetsAtPath(string path)
        {
            //return all assets at path (including sub folders)
            var assetsAtPath = AssetDatabase.FindAssets("", new[] { path })
                //convert GUIDs to paths
                .Select(AssetDatabase.GUIDToAssetPath)
                //convert paths to assets
                .Select(AssetDatabase.LoadAssetAtPath<Object>)
                //filter out null objects
                .Where(asset => asset != null)
                .ToList();
            return assetsAtPath;
        }
        
        /// <summary>
        /// Loads all assets at the given path including sub folders of type T
        /// </summary>
        /// <param name="path">The path to search from (including sub folders of this path)</param>
        /// <returns>A list of objects contained within this folder and subfolders of type T</returns>
        public static List<T> RecursivelyLoadAllAssetsAtPath<T>(string path)
        {
            //return all assets at path (including sub folders)
            var assetsAtPath = RecursivelyLoadAllAssetsAtPath(path).OfType<T>().ToList();
            return assetsAtPath;
        }
    }
}