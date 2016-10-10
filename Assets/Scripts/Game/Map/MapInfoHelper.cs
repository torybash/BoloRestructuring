using Bolo.DataClasses;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bolo.Map
{
	[Serializable]
	public class MapInfoHelper
	{
		[SerializeField]
		private int _xSize;
		[SerializeField]
		private int _ySize;

		public MapInfoHelper()
		{
		}

		public MapInfo GetRandomMapInfo()
		{
			var mapInfo = new MapInfo();
			for (int i = 0; i < _xSize; i++)
			{
				for (int j = 0; j < _ySize; j++)
				{
					mapInfo.groundMap[i, j] = TileType.GRASS;
					var blockType = BlockType.EMPTY;
					if (Random.value < 0.5) blockType = BlockType.EMPTY;
					else if (Random.value < 0.95) blockType = BlockType.ROCK;
					else if (Random.value < 1) blockType = BlockType.CRYSTAL;

					mapInfo.blockMap[i, j] = blockType;

					//make map walls
					if (j == 0 || j == _ySize - 1) mapInfo.blockMap[i, j] = BlockType.IMPASSABLE;
					if (i == 0 || i == _xSize - 1) mapInfo.blockMap[i, j] = BlockType.IMPASSABLE;
				}
			}
			return mapInfo;
		}
	}
}