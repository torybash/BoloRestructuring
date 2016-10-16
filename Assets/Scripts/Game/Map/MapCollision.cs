using UnityEngine;
using System;
using Bolo.Util;
using System.Collections.Generic;
using Bolo.DataClasses;

namespace Bolo.Map
{
	public class MapCollision : MonoBehaviour {

		[Serializable]
		private class MapCollisionParameters
		{
			public int areaWidth = 5;
			public int areaHeight = 5;
		}

		[SerializeField] MapCollisionParameters _parameters = new MapCollisionParameters();

		private MapInfo _mapInfo;
		private BehaviourPool<BoxCollider2D> _colliderPool;

		private Dictionary<Pos, BoxCollider2D> _colliders = new Dictionary<Pos, BoxCollider2D>();

		private Rect _lastRect;

		public MapInfo mapInfo { get { return _mapInfo; } set { _mapInfo = value; } }

		void Awake()
		{
			_colliderPool = new BehaviourPool<BoxCollider2D>(gameObject, int.MaxValue);
		}

		public void GenerateCollisionInArea(Pos pos)
		{
			//TODO use rectss
			//TODO remove colliders
			Debug.Log("GenerateCollisionInArea: " + pos.x + " , " + pos.y + ", areaWidth: " + _parameters.areaWidth + ", areaHeight:" + _parameters.areaHeight);

			var newRect = new Rect(pos.x - _parameters.areaWidth / 2f, pos.y - _parameters.areaHeight / 2f, _parameters.areaWidth, _parameters.areaHeight);


			//_lastRect.

			for (int x = (int)newRect.xMin; x < (int)newRect.xMax; x++)
			{
				for (int y = (int)newRect.yMin; y < (int)newRect.yMax; y++)
				{
					var blockType = _mapInfo.GetBlockAt(x, y);
					if (blockType == BlockType.EMPTY) continue;

					if (_colliders.ContainsKey(new Pos(x, y))) continue;
					
					var collInst = _colliderPool.Get();
					collInst.offset = new Vector2(x, y) + Vector2.one * 0.5f ; 
					collInst.size = Vector2.one;

					_colliders.Add(new Pos(x, y), collInst);
				}
			}
		}
	}
}

