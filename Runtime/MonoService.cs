using System;
using StudioName.Runtime;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// Base class for creating a service which relies on <see cref="MonoBehaviour"/>.
/// <para>In some ways seen as a better alternative to <see cref="Singleton{T}"/></para>
/// </summary>
/// <typeparam name="T">The Monobehaviour type of this instance</typeparam>
public abstract class MonoService<T> : MonoBehaviour where T : MonoBehaviour {
    
    private void Awake()
    {
        var attribute = Attribute.GetCustomAttribute(typeof(T), typeof(MonoServiceAttribute)) as MonoServiceAttribute;
        if (!attribute?.destroyInstanceOnLevelLoad ?? false) {
            DontDestroyOnLoad(this);
        }      
    }

    /// <summary>
    /// Instantiate the service and get a reference to it's component type
    /// </summary>
    /// <returns>The component on our newly instantiate service gameobject</returns>
    public static T CreateInstance()
    {
        var instance = new GameObject().AddComponent<T>();
        instance.transform.position = Vector3.zero;
        instance.name = typeof(T).Name;
        
        var attribute = Attribute.GetCustomAttribute(typeof(T), typeof(MonoServiceAttribute)) as MonoServiceAttribute;
        //append the object name prefix when instaniation for better organization in project hierarchy
        instance.name = instance.name.Insert(0, attribute?.objectNamePrefix ?? "");
        if (!attribute?.destroyInstanceOnLevelLoad ?? false) {
            SafeAddDontDestroyOnLoad(instance);
        }      

        return instance;
    }

    /// <summary>
    /// Instantiate the service prefab and get a reference to it's component type
    /// <para>Loads the prefab from the path specified in <see cref="MonoServiceAttribute.resourcesLoadPath"/></para>
    /// </summary>
    /// <returns>The component on our newly instantiate service gameobject</returns>
    public static T GetInstanceFromResources() {
        var attribute = Attribute.GetCustomAttribute(typeof(T), typeof(MonoServiceAttribute)) as MonoServiceAttribute ??
                        new MonoServiceAttribute();
        
        var prefab = (GameObject)Resources.Load(attribute.ResourcesLoadPathWithTypeName<T>(), typeof(GameObject));
        if (prefab == null) {
            prefab = (GameObject)Resources.Load(attribute.ResourcesLoadPathWithTypeName<T>(true), typeof(GameObject));						
        }

        GameObject instance;
        if (prefab != null) {
            instance = Instantiate(prefab);
        } else {
            Debug.LogWarning($"GetInstanceFromResources() failed for the paths \n" +
                             $"> {attribute.ResourcesLoadPathWithTypeName<T>()}\n" +
                             $"> {attribute.ResourcesLoadPathWithTypeName<T>(true)} \n" +
                             $"Ensure your GameObject is at either path listed with the given name listed.");
            var tempInstance = new GameObject();
            tempInstance.AddComponent<T>();
            instance = tempInstance;
        }
        instance.name = typeof(T).Name;
        //append the object name prefix when instaniation for better organization in project hierarchy
        instance.name = instance.name.Insert(0, attribute?.objectNamePrefix ?? "");
        if (!attribute.destroyInstanceOnLevelLoad)
        {
            SafeAddDontDestroyOnLoad(instance);
        }
        
        return instance.GetComponent<T>();
    }

    /// <summary>
    /// Adds dont destroy on load as long we are not in the editor no in play mode
    /// </summary>
    /// <param name="instance">The instance to apply DontDestroyOnLoad too</param>
    private static void SafeAddDontDestroyOnLoad(Object instance)
    {
        //can't call DontDestroyOnLoad here (used to avoid exception when editor testing)
        if (Application.isEditor && !Application.isPlaying)
        {
            return;
        }
        DontDestroyOnLoad(instance);
    }
}

/// <summary>
/// Defines resource path to prefab and allows marking DontDestroyOnLoad to our MonoService for convenience
/// </summary>
public class MonoServiceAttribute : SingletonAttribute {

    /// <inheritdoc/>
    /// <summary>
    /// Add additional info to our <see cref="MonoService{T}"/> classes
    /// </summary>
    public MonoServiceAttribute(string resourcesLoadPath = "", bool destroyInstanceOnLevelLoad = false,
        string objectNamePrefix = "") : 
        base(resourcesLoadPath, destroyInstanceOnLevelLoad,objectNamePrefix) {
        
    }
}