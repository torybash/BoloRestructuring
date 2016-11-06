using System;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool<T> where T : UnityEngine.Object
{

	private T _template;
	private Queue<T> _pool;
	private int _objectCount;

	private int _maxSize;

	public ObjectPool(T template, int maxSize){
		_template = template;
		_pool = new Queue<T>();
		_objectCount = 0;

		_maxSize = maxSize;
	}

	public T Get()
	{
		if (_pool.Count == 0)
		{
			if (_objectCount >= _maxSize) return null;

			T inst = GameObject.Instantiate(_template);
			_pool.Enqueue(inst);
			_objectCount++;
		}
		return _pool.Dequeue();
	}

	public void Return(T obj)
	{
		_pool.Enqueue(obj);
	}
}

