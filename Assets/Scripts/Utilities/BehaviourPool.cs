using System;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourPool<T> where T : Behaviour
{

	private GameObject _obj;
	private Queue<T> _pool;
	private int _objectCount;

	private int _maxSize;

	public BehaviourPool(GameObject obj, int maxSize){
		_obj = obj;
		_pool = new Queue<T>();
		_objectCount = 0;

		_maxSize = maxSize;
	}

	public T Get()
	{
		if (_pool.Count == 0)
		{
			if (_objectCount >= _maxSize) return null;

			T inst = _obj.AddComponent<T>();
			_pool.Enqueue(inst);
			_objectCount++;
		}
		T bh = _pool.Dequeue();
		bh.enabled = true;
		return bh;
	}

	public void Return(T bh)
	{
		bh.enabled = false;
		_pool.Enqueue(bh);
	}
}

