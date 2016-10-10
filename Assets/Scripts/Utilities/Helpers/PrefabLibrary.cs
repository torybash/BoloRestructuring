using UnityEngine;
using System;
using System.Collections.Generic;
using Bolo.DataClasses;

namespace Bolo.Util
{
	[CreateAssetMenu(fileName = "PrefabLibrary", menuName = "Bolo/ScriptableObjects/PrefabLibrary", order = 1)]
	public class PrefabLibrary : LibraryObject<PrefabLibrary>
	{

		[SerializeField]
		private List<GameObject> _prefabList;

		private Dictionary<Type, GameObject> _prefabTypeDict;
		private Dictionary<string, GameObject> _prefabNameDict;


		public void Init()
		{
			_prefabTypeDict = new Dictionary<Type, GameObject>();
			_prefabNameDict = new Dictionary<string, GameObject>();
			foreach (var item in _prefabList)
			{
				_prefabTypeDict.Add(item.GetType(), item);      //TODO, use custom key-prefab list instead
				_prefabNameDict.Add(item.name, item);      //TODO, use custom key-prefab list instead
			}
		}


		public GameObject Create(string name)
		{
			GameObject prefab = null;
			if (!_prefabNameDict.TryGetValue(name, out prefab))
			{
				Debug.LogError("Prefab of name " + name + " does not exists!");
				return null;
			}
			var instance = GameObject.Instantiate(prefab);
			return instance;
		}

		public GameObject Create(Type type)
		{
			GameObject prefab = null;
			if (!_prefabTypeDict.TryGetValue(type, out prefab))
			{
				Debug.LogError("Prefab of type " + type + " does not exists!");
				return null;
			}
			var instance = GameObject.Instantiate(prefab);
			return instance;
		}
	}
}