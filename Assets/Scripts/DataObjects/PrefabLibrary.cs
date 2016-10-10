using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PrefabLibrary", menuName = "Bolo/ScriptableObjects/PrefabLibrary", order = 1)]
public class PrefabLibrary : LibraryObject<PrefabLibrary>
{

	[SerializeField]
	private List<GameObject> _prefabList;

	private Dictionary<Type, GameObject> _prefabDict;


	public void Init()
	{
		_prefabDict = new Dictionary<Type, GameObject>();
		foreach (var item in _prefabList)
		{
			_prefabDict.Add(item.GetType(), item);      //TODO, use custom key-prefab list instead
		}
	}


	public GameObject Create(Type type)
	{
		GameObject prefab = null;
		if (!_prefabDict.TryGetValue(type, out prefab))
		{
			Debug.LogError("Spawnable with type " + type + " does not exists!");
			return null;
		}
		var instance = GameObject.Instantiate(prefab);
		return instance;
	}
}
