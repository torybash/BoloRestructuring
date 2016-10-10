using UnityEngine;

namespace Bolo.DataClasses
{
	public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
	{

		private static object _lock = new object();
		private static T _instance;

		private static bool applicationIsQuitting = false;

		protected static T I
		{
			get
			{
				lock (_lock)
				{
					if (Application.isPlaying && applicationIsQuitting)
					{
						Debug.LogWarning("Instance '" + typeof(T) + "' already destroyed on application quit. Won't create again - returning null.");
						return null;
					}
					if (_instance == null)
					{
						_instance = (T)FindObjectOfType(typeof(T));

						if (FindObjectsOfType(typeof(T)).Length > 1)
						{
							Debug.LogError("There should never be more than 1 instance of singleton!");
							return _instance;
						}

						if (_instance == null)
						{
							Debug.LogError("Could not find an instance of type " + typeof(T));
						}
						else {
							Debug.Log("Using instance: " + _instance.gameObject.name);
						}
					}

					return _instance;
				}
			}
		}

		protected void OnDestroy()
		{
			if (Application.isPlaying && _instance == this) applicationIsQuitting = true;
		}
	}
}