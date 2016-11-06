using UnityEngine;
using System;
using System.Collections.Generic;
using Bolo.DataClasses;

namespace Bolo
{
	[CreateAssetMenu(fileName = "PrefabLibrary", menuName = "Bolo/ScriptableObjects/PrefabLibrary", order = 1)]
	public class PrefabLibrary : LibraryObject<PrefabLibrary>
	{

		[SerializeField]
		private List<GameObject> _prefabList;

		private Dictionary<Type, GameObject> _prefabTypeDict;
		private Dictionary<string, GameObject> _prefabNameDict;


		#region Lifecycle
		void OnEnable()
		{
			_prefabTypeDict = new Dictionary<Type, GameObject>();
			_prefabNameDict = new Dictionary<string, GameObject>();
			foreach (var item in _prefabList)
			{
				_prefabTypeDict.Add(item.GetType(), item); 
				_prefabNameDict.Add(item.name, item); 
			}
		}
		#endregion Lifecycle


		public static GameObject Create(string name)
		{
			GameObject prefab = null;
			if (!sLibObject._prefabNameDict.TryGetValue(name, out prefab))
			{
				Debug.LogError("Prefab of name " + name + " does not exists!");
				return null;
			}
			var instance = GameObject.Instantiate(prefab);
			return instance;
		}

		public static GameObject Create(Type type)
		{
			GameObject prefab = null;
			if (!sLibObject._prefabTypeDict.TryGetValue(type, out prefab))
			{
				Debug.LogError("Prefab of type " + type + " does not exists!");
				return null;
			}
			var instance = GameObject.Instantiate(prefab);
			return instance;
		}
	}
}