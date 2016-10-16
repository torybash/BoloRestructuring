using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace Bolo.Map
{
	[Serializable]
	public class MapGenerator
	{

		 //TODO Make configuration object instead!
		[Header("Generation parameters")]
		[SerializeField]
		private int _mapSize;
		[SerializeField]
		private int _chunkSize;



		public MapTiles GenerateMap(int seed, Transform parent)
		{
			if (_mapSize % 2 != 0 || _chunkSize % 2 != 0 || _chunkSize > _mapSize)
			{
				Debug.Assert(_mapSize % 2 == 0, "Map size size must of power-of-two! (map size: " + _mapSize + ")");
				Debug.Assert(_chunkSize % 2 == 0, "Chunk size size must of power-of-two! (chunk size: " + _chunkSize + ")");
				Debug.Assert(_chunkSize <= _mapSize, "Map size must be larger than chunk size! (chunk size: " + _chunkSize + ", map size: " + _mapSize + ")");
				return null;
			}

			//Create mapInfo
			var mapInfo = MapInfoHelper.GetRandomMapInfo(seed, _mapSize);

			//Create new map
			var _map = new GameObject("MapContainer").AddComponent<MapTiles>();
			_map.transform.SetParent(parent, false);
			_map.Init(mapInfo, _chunkSize);
			return _map;
		}

		public int CreateMapSeed() //TODO Create map-configuration instead (size, gen-parameters)
		{
			return Random.Range(0, int.MaxValue);
		}
	}
}
