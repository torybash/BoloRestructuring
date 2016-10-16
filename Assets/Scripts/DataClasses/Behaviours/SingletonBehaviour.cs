using UnityEngine;

namespace Bolo.DataClasses
{
	public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
	{

		private static object _lock = new object();
		private static T s_instance;

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
					if (s_instance == null)
					{
						s_instance = (T)FindObjectOfType(typeof(T));

						if (FindObjectsOfType(typeof(T)).Length > 1)
						{
							Debug.LogError("There should never be more than 1 instance of singleton!");
							return s_instance;
						}

						if (s_instance == null)
						{
							Debug.LogError("Could not find an instance of type " + typeof(T));
						}
						else {
							Debug.Log("Using instance: " + s_instance.gameObject.name);
						}
					}

					return s_instance;
				}
			}
		}

		protected void OnDestroy()
		{
			if (Application.isPlaying && s_instance == this) applicationIsQuitting = true;
		}
	}
}