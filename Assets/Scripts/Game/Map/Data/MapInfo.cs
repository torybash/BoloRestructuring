using UnityEngine;
using System.Collections;
using Bolo.DataClasses;

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


	}
}