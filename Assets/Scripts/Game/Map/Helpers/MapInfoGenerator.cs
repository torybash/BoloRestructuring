using Bolo.DataClasses;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bolo.Map
{
	public class MapInfoHelper
	{


		public MapInfoHelper()
		{
		}


		#region MapInfo
		public static MapInfo GetRandomMapInfo(int seed, int mapSize)
		{
			Random.InitState(seed);
			var mapInfo = new MapInfo(mapSize);
			for (int x = 0; x < mapSize; x++)
			{
				for (int y = 0; y < mapSize; y++)
				{
					mapInfo._groundMap[x, y] = GroundType.GRASS;
					var blockType = BlockType.EMPTY;
					if (Random.value < 0.5) blockType = BlockType.EMPTY;
					else if (Random.value < 0.95) blockType = BlockType.ROCK;
					else if (Random.value < 1) blockType = BlockType.CRYSTAL;

					mapInfo._blockMap[x, y] = blockType;

					//make map walls
					if (y == 0 || y == mapSize - 1) mapInfo._blockMap[x, y] = BlockType.IMPASSABLE;
					if (x == 0 || x == mapSize - 1) mapInfo._blockMap[x, y] = BlockType.IMPASSABLE;
				}
			}
			return mapInfo;
		}

		#endregion MapInfo
	}
}