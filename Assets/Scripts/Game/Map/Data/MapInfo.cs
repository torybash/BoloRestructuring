using UnityEngine;
using System.Collections;
using Bolo.DataClasses;
using System;

namespace Bolo.Map
{
	public class MapInfo
	{
		public int _size;
		public GroundType[,] _groundMap;
		public BlockType[,] _blockMap;

		public MapInfo(int size){
			_size = size;
			_groundMap = new GroundType[size,size];
			_blockMap = new BlockType[size,size];
		}

		public GroundType GetGroundAt(Pos pos)
		{
			return GetGroundAt(pos.x, pos.y);
		}

		public GroundType GetGroundAt(int x, int y)
		{
			if (x < 0 || x >= _size || y < 0 || y >= _size) return GroundType.GRASS;
			return _groundMap[x, y];
		}

		public BlockType GetBlockAt(Pos pos)
		{
			return GetBlockAt(pos.x, pos.y);
		}

		public BlockType GetBlockAt(int x, int y)
		{
			if (x < 0 || x >= _size || y < 0 || y >= _size) return BlockType.EMPTY;
			return _blockMap[x, y];
		}
	}
}