using UnityEngine;
using System;
using Random = UnityEngine.Random;
using Bolo.DataClasses;

namespace Bolo.Map
{
	[Serializable]
	public class MapGenerator
	{
		private const int cChunkSize = 64;

		#region MapInfo
		public MapInfo GetRandomMapInfo(int seed, int mapSize)
		{
			if (mapSize % 2 != 0 || cChunkSize > mapSize) // || cChunkSize % 2 != 0) 
			{
				Debug.Assert(mapSize % 2 == 0, "Map size size must of power-of-two! (map size: " + mapSize + ")");
				//Debug.Assert(cChunkSize % 2 == 0, "Chunk size size must of power-of-two! (chunk size: " + cChunkSize + ")");
				Debug.Assert(cChunkSize <= mapSize, "Map size must be larger than chunk size! (chunk size: " + cChunkSize + ", map size: " + mapSize + ")");
				return null;
			}

			Random.InitState(seed);
			var mapInfo = new MapInfo(mapSize, cChunkSize);
			for (int x = 0; x < mapSize; x++)
			{
				for (int y = 0; y < mapSize; y++)
				{
					mapInfo.groundMap[x, y] = GroundType.GRASS;
					var blockType = BlockType.EMPTY;
					if (Random.value < 0.5) blockType = BlockType.EMPTY;
					else if (Random.value < 0.95) blockType = BlockType.ROCK;
					else if (Random.value < 1) blockType = BlockType.CRYSTAL;

					mapInfo.blockMap[x, y] = blockType;

					//make map walls
					if (y == 0 || y == mapSize - 1) mapInfo.blockMap[x, y] = BlockType.IMPASSABLE;
					if (x == 0 || x == mapSize - 1) mapInfo.blockMap[x, y] = BlockType.IMPASSABLE;
				}
			}
			return mapInfo;
		}

		#endregion MapInfo
	}
}
