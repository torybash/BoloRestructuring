using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NetworkManager))]
public class NetManagerPrefabs : MonoBehaviour
{

	private NetworkManager _manager;

	private Dictionary<string, GameObject> _prefabDict;

	private NetworkManager manager
	{
		get
		{
			if (_manager == null) _manager = GetComponent<NetworkManager>();
			return _manager;
		}
	}

	void Awake()
	{
		_prefabDict = new Dictionary<string, GameObject>();
		foreach (var item in manager.spawnPrefabs)
		{
			_prefabDict.Add(item.name, item);
		}
	}

	public GameObject Create(string key)
	{
		GameObject prefab = null;
		if (!_prefabDict.TryGetValue(key, out prefab))
		{
			Debug.LogError("Spawnable with key " + key + " does not exists!");
			return null;
		}
		var instance = GameObject.Instantiate(prefab);
		return instance;
	}
}
