using UnityEngine;
using Bolo.DataClasses;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Bolo.Map
{
	public class MapController : ControllerBehaviour<MapController>
	{
		[SerializeField]
		private Map _map;

		[SerializeField]
		private MapGenerator _mapGen;


		#region Client

		public void CreateMapFromSeed(int seed)
		{
			//Cleanup map
			if (_map) Destroy(_map.gameObject);

			//Create new map
			_map = _mapGen.GenerateMap(seed);
		}
		#endregion Client

		#region Server
		public int CreateMapSeed()
		{
			return Random.Range(0, int.MaxValue);
		}

		public Vector2 GetNewSpawnPosition(List<Vector2> playerPositions)
		{
			//TODO use playerPositions!
			//TODO prevent spawn on block (or remove block on spawn)
			var rndPos = new Vector2(Random.Range(1, _map.mapInfo._size-1), Random.Range(1, _map.mapInfo._size-1));
			return rndPos;
		}


		#endregion Server
	}
}