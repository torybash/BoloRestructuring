using UnityEngine;
using System.Collections;
using Bolo.DataClasses;

namespace Bolo.Map
{
	public class MapInfo
	{
		public MapInfo(int x, int y){
			groundMap = new TileType[x,y];
			blockMap = new BlockType[x,y];
		}

		public TileType[,] groundMap;
		public BlockType[,] blockMap;
	}
}