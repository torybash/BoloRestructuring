using UnityEngine;
using System.Collections;

namespace Bolo.Map
{
	public class MapGenerator : MonoBehaviour
	{
		[SerializeField]
		private Map _map;

		[Header("Generation parameters")]

		[SerializeField]
		private int _mapSize;
		[SerializeField]
		private int _chunkSize;



		public void GenerateMap(int seed)
		{
			if (_mapSize % 2 != 0 || _chunkSize % 2 != 0 || _chunkSize > _mapSize)
			{
				Debug.Assert(_mapSize % 2 == 0, "Map size size must of power-of-two! (map size: " + _mapSize + ")");
				Debug.Assert(_chunkSize % 2 == 0, "Chunk size size must of power-of-two! (chunk size: " + _chunkSize + ")");
				Debug.Assert(_chunkSize <= _mapSize, "Map size must be larger than chunk size! (chunk size: " + _chunkSize + ", map size: " + _mapSize + ")");
				return;
			}

			//Create mapInfo
			var mapInfo = MapInfoHelper.GetRandomMapInfo(seed, _mapSize);

			//Cleanup map
			if (_map) Destroy(_map.gameObject);

			//Create new map
			_map = new GameObject("MapContainer").AddComponent<Map>();
			_map.transform.SetParent(transform, false);
			_map.GenerateChunks(mapInfo, _chunkSize);
		}
	}
}
