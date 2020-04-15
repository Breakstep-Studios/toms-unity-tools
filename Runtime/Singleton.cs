using System;
using System.Linq;
using UnityEngine;

namespace StudioName.Runtime {

	/// <summary>
	/// Creates a single instance of a component / GameObject that will last for the life of the scene / application. 
	/// [Note] Destroys any GO after first of type is created. Use <see cref="SingletonAwake"/> in place of awake!
	/// </summary>
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T instance;
		private static object instanceLock = new object();
		private static bool available = true;

		public static T Instance
		{
			get
			{
				//prevents not getting a singleton if we are in edit mode
#if UNITY_EDITOR
				if (!UnityEditor.EditorApplication.isPlaying)
				{
					available = true;
				}
#endif

				var typeName = typeof(T).Name;

				//below error still possible with DestroyOnLoad but skip otherwise we will have issues
				var attribute =
					Attribute.GetCustomAttribute(typeof(T), typeof(SingletonAttribute)) as SingletonAttribute
					?? new SingletonAttribute();

				if (!available && !attribute.destroyInstanceOnLevelLoad)
				{
					Debug.LogWarning("Singleton Instance \"" + typeName + "\" already destroyed - returning null." +
					                 "If accessing Singleton in OnDestroy() / OnApplicationQuit() first check if it exists with" +
					                 "InstanceAvailable property...For more info see Singleton OnDestroy() Summary.");
					return null;
				}

				lock (instanceLock)
				{
					if (InstanceExists)
					{
						return instance;
					}

					//find in scene
					var singletonInstances = FindObjectsOfType<T>();
					if (singletonInstances.Length > 0)
					{
						instance = singletonInstances[0];
						if (singletonInstances.Length > 1)
						{
							for (var i = 1; i < singletonInstances.Length; i++)
							{
								DestroyImmediate(singletonInstances[i].gameObject);
							}

							Debug.LogWarning("There is more than one Singleton of type \"" + typeName +
							                 "\" in scene. " +
							                 "Keeping the first destroying the others.");
						}
					}
					else
					{
						//find in resources folder
						GameObject singleton = null;
						var singletonPrefab = (GameObject) Resources.Load(attribute.ResourcesLoadPathWithTypeName<T>(),
							typeof(GameObject));
						if (singletonPrefab == null)
						{
							singletonPrefab = (GameObject) Resources.Load(
								attribute.ResourcesLoadPathWithTypeName<T>(true), typeof(GameObject));
						}

						if (singletonPrefab != null)
						{
							singleton = Instantiate(singletonPrefab);
						}

						//can't find in resources, so create
						if (singleton == null)
						{
							Debug.LogWarningFormat("Singleton {0} was not found in resources folder...Creating one.",
								typeName);
							var tempSingleton = new GameObject();
							tempSingleton.AddComponent<T>();
							singleton = tempSingleton;
						}

						singleton.name = typeName;
#if UNITY_EDITOR
						if (!UnityEditor.EditorApplication.isPlaying)
						{
							singleton.name = "[CREATED IN EDIT MODE] " + singleton.name;
						}
#endif
						instance = singleton.GetComponent<T>();
					}

					available = true;
					if (!attribute.destroyInstanceOnLevelLoad)
					{
#if UNITY_EDITOR
						if (UnityEditor.EditorApplication.isPlaying)
						{
							DontDestroyOnLoad(instance);
						}
#else
						DontDestroyOnLoad(instance);
#endif
					}

					return instance;
				}
			}
		}

		/// <summary>
		/// Allows us to initialize our singleton on Awake. Alternate to Awake { base.Awake; //otherstuff }
		/// </summary>
		protected virtual void SingletonAwake()
		{
		}

		/// <summary>
		/// Allows us to execute destroy logic on OnDestroy. Alternate to OnDestroy { base.OnDestroy; //otherstuff }
		/// </summary>
		protected virtual void SingletonOnDestroy()
		{
		}

		/// <summary>
		/// Destroy the current static instance of this singleton
		/// </summary>
		/// <param name="destroyGameObject">Should we destroy the gameobject of the instance too?</param>
		public static void DestroyInstance(bool destroyGameObject = true)
		{
			if (InstanceExists)
			{
				if (destroyGameObject)
				{
					Destroy(instance.gameObject);
				}
				else
				{
					Destroy(instance);
				}

				instance = null;
			}
		}

		/// <summary>
		/// Ensure Instance != null && Instance is not destroyed in OnDestroy() before another methods OnDestroy() wants to access it.
		/// </summary>
		public static bool InstanceAvailable
		{
			get { return instance != null && available; }
		}

		/// <summary>
		/// Convenience method to ensure we have a singleton instance
		/// </summary>
		private static bool InstanceExists
		{
			get { return instance != null; }
		}

		/// <summary>
		/// If a singleton is created outside of Instance.SomeMethod, ensure we destroy it if we already have an instance
		/// To use Awake in Singleton <see cref="SingletonAwake"/>
		/// </summary>
		/// TODO maybe should rethink this bit
		private void Awake()
		{
			if (InstanceExists && instance != this)
			{
				Debug.Log("A new singleton of type  \"" + typeof(T).Name + "\"  has been created, " +
				          "but we already have one available...Destroying it.");
				Destroy(gameObject);
				return;
			}

			var initInstance = Instance;
			SingletonAwake();
		}

		/// <summary>
		/// Prevents Unity from leaking Singleton GameObjects in the below scenario.
		/// <para>
		/// When Unity quits it destroys objects in a random order. Because of this Singleton could be destroyed first, then
		/// in an OnDestroy() another script could want it Example:( OnDestroy() { Singleton.Instance.SomeMethod } ).
		/// At this point unity creates a new Singleton. The new Singleton doesn't get an OnDestroy callback, because
		/// the scene / app has already called it. This is where the leak comes in.
		/// </para>
		/// </summary>
		private void OnDestroy()
		{
			available = false;
			SingletonOnDestroy();
		}

		/// <inheritdoc cref="OnDestroy"/>
		protected void OnApplicationQuit()
		{
			available = false;
		}
	}

	/// <summary>
	/// Defines a custom resource path to our singleton if necessary
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class SingletonAttribute : Attribute
	{
		public readonly string resourcesLoadPath;
		public readonly bool destroyInstanceOnLevelLoad;

		/// <summary>
		/// Add additional info to our singleton classes
		/// </summary>
		/// <param name="resourcesLoadPath">
		/// <para>Set the resources path to load from</para>
		/// <para>Remember relative to resources folder in Assets path!</para>
		/// </param>
		/// <param name="destroyInstanceOnLevelLoad">Destroy the instance when a new level loads</param>
		public SingletonAttribute(string resourcesLoadPath = "", bool destroyInstanceOnLevelLoad = false)
		{
			this.resourcesLoadPath = resourcesLoadPath;
			this.destroyInstanceOnLevelLoad = destroyInstanceOnLevelLoad;
		}

		public string ResourcesLoadPathWithTypeName<T>(bool includeSpaces = false)
		{
			var typeName = typeof(T).Name;
			var typeNameWithSpaces = string.Concat(typeName.Select(
				x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
			return (includeSpaces) ? resourcesLoadPath + typeNameWithSpaces : resourcesLoadPath + typeName;
		}
	}
}