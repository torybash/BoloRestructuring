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

		private BehaviourPool<BoxCollider2D> _colliderPool;

		private Dictionary<Pos, BoxCollider2D> _colliders = new Dictionary<Pos, BoxCollider2D>();

		private Rect _lastRect;

		private MapInfo _mapInfo;

		void Awake()
		{
			_colliderPool = new BehaviourPool<BoxCollider2D>(gameObject, int.MaxValue);
		}


		public void Setup(MapInfo mapInfo)
		{
			_mapInfo = mapInfo;
		}


		public void UpdateCollisionInArea(Pos pos)
		{
			//TODO use rects

			//Debug.Log("GenerateCollisionInArea: " + pos.x + " , " + pos.y + ", areaWidth: " + _parameters.areaWidth + ", areaHeight:" + _parameters.areaHeight);
			var rect = new Rect(pos.x - _parameters.areaWidth / 2f, pos.y - _parameters.areaHeight / 2f, _parameters.areaWidth, _parameters.areaHeight);

			//_lastRect

			for (int x = (int)rect.xMin; x < (int)rect.xMax; x++)
			{
				for (int y = (int)rect.yMin; y < (int)rect.yMax; y++)
				{
					var tilePos = new Pos(x, y);
					var blockType = _mapInfo.GetBlockAt(tilePos);
					bool hasCollider = _colliders.ContainsKey(tilePos);
					if (blockType == BlockType.EMPTY)
					{
						if (hasCollider)
						{
							var coll = _colliders[tilePos];
							_colliderPool.Return(coll);
							_colliders.Remove(tilePos);
						}
						continue;
					}
					if (hasCollider) continue;

					var collInst = _colliderPool.Get();
					collInst.offset = new Vector2(x, y) + Vector2.one * 0.5f ; 
					collInst.size = Vector2.one;

					_colliders.Add(new Pos(x, y), collInst);
				}
			}

			_lastRect = rect;
		}


	}
}

