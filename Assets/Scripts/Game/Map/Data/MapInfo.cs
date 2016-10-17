using UnityEngine;
using System.Collections;
using Bolo.DataClasses;
using System;

namespace Bolo.Map
{
	public class MapInfo
	{
		//public class Tile
		//{
		//	public GroundType ground;
		//	public BlockType block;
		//}

		public int size;
		public int chunkSize;

		public GroundType[,] groundMap;
		public BlockType[,] blockMap;
		//public Tile[,] _tileMap;

		public MapInfo(int size, int chunkSize){
			this.size = size;
			this.chunkSize = chunkSize;
			//_tileMap = new Tile[size, size];
			groundMap = new GroundType[size, size];
			blockMap = new BlockType[size, size];
		}

		public GroundType GetGroundAt(Pos pos)
		{
			return GetGroundAt(pos.x, pos.y);
		}

		public GroundType GetGroundAt(int x, int y)
		{
			if (x < 0 || x >= size || y < 0 || y >= size) return GroundType.GRASS;
			return groundMap[x, y];
			//return _tileMap[x, y].ground;
		}

		public BlockType GetBlockAt(Pos pos)
		{
			return GetBlockAt(pos.x, pos.y);
		}

		public BlockType GetBlockAt(int x, int y)
		{
			if (x < 0 || x >= size || y < 0 || y >= size) return BlockType.EMPTY;
			return blockMap[x, y];
			//return _tileMap[x, y].block;
		}

		public void SetBlock(Pos pos, BlockType block)
		{
			SetBlock(pos.x, pos.y, block);
		}

		public void SetBlock(int x, int y, BlockType block)
		{
			if (x < 0 || x >= size || y < 0 || y >= size) return;
			blockMap[x, y] = block;
		}
	}

	
}