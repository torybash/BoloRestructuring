using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Bolo
{
	public class SpawnPool<T> where T : NetworkSpawnable
	{
		private int _poolSize;
		private T _prefab;
		private Stack<T> _pool;

		public NetworkHash128 assetId { get; set; }

		public SpawnPool(T prefab, int poolSize)
		{
			_prefab = prefab;
			_poolSize = poolSize;

			assetId = _prefab.GetComponent<NetworkIdentity> ().assetId;
			_pool = new Stack<T>(_poolSize);
			for (int i = 0; i < _poolSize; ++i)
			{
				var instance = (T)GameObject.Instantiate(_prefab, Vector3.zero, Quaternion.identity);
				instance.name = "PoolObject" + i;
				instance.gameObject.SetActive(false);
				_pool.Push(instance);
			}
        
			ClientScene.RegisterSpawnHandler(assetId, Spawn, UnSpawn);
		}


		public T GetInstance() //(Vector3 position)
		{
			var obj = _pool.Pop();
			if (obj != null)
			{
				Debug.Log("Activating object " + obj.name);
				//obj.transform.position = position;
				obj.gameObject.SetActive(true);
				return obj;
			}
			Debug.LogError("Could not grab object from pool, nothing available");
			return null;
		}


	    private GameObject Spawn(Vector3 position, NetworkHash128 assetId)
		{
			var inst = GetInstance();
			return inst ? inst.gameObject : null;
		}

		private void UnSpawn(GameObject spawned)
		{
			Debug.Log ("Re-pooling object " + spawned.name);
			spawned.SetActive (false);
		}
	}
}
