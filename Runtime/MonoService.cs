using System;
using StudioName.Runtime;
using UnityEngine;

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
        if (!attribute?.destroyInstanceOnLevelLoad ?? false) {
            DontDestroyOnLoad(instance);
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
            var tempInstance = new GameObject();
            tempInstance.AddComponent<T>();
            instance = tempInstance;
        }
        instance.name = typeof(T).Name;
        
        if (!attribute.destroyInstanceOnLevelLoad) {
            DontDestroyOnLoad(instance);
        }
        
        return instance.GetComponent<T>();
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
    public MonoServiceAttribute(string resourcesLoadPath = "", bool destroyInstanceOnLevelLoad = false) : 
        base(resourcesLoadPath, destroyInstanceOnLevelLoad) {
        
    }
}