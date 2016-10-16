using UnityEngine;
using System.Collections;
using Bolo.DataClasses;

namespace Bolo.Map
{
	public class MapGrahicsHelper
	{

		public static int[,] GetGraphicsGroundMap(GroundType[,] groundMap)
		{
			int width = groundMap.GetLength(0);
			int height = groundMap.GetLength(1);
			var gfxMap = new int[width, height];

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					gfxMap[x, y] = (int) groundMap[x, y]; //TEMP
				}
			}
			return gfxMap;
		}

		public static int[,] GetGraphicsBlockMap(BlockType[,] blockMap)
		{
			int width = blockMap.GetLength(0);
			int height = blockMap.GetLength(1);
			var gfxMap = new int[width, height];

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					var blockType = blockMap[x, y];
					switch (blockType) {
					case BlockType.ROCK:

						gfxMap[x,y] = (int) GetGraphicsBlockTile(blockMap, x, y, BlockType.ROCK);

						break;
					case BlockType.EMPTY:
					
						gfxMap[x,y] = (int)GraphicsBlockType.EMPTY;
						break;
					case BlockType.IMPASSABLE:
					
						gfxMap[x,y] = (int)GraphicsBlockType.IMPASSABLE;
						break;

					case BlockType.CRYSTAL:
					
						gfxMap[x,y] = (int)GraphicsBlockType.CRYSTAL;
						break;

					default:
						gfxMap[x,y] = (int)GraphicsBlockType.EMPTY;
						break;
					}
					//var blockType = blockMap[x, y];
					//if (blockType == BlockType.ROCK)
					//{
					//	gfxMap[x, y] = (int) GetGraphicsBlockTile(blockMap, x, y, BlockType.ROCK);
					//}
					//else
					//{
					//	gfxMap[x, y] = (int) blockType;
					//}
				}
			}

			return gfxMap;
		}



		private static GraphicsBlockType GetGraphicsBlockTile(BlockType[,] tileMap, int i, int j, BlockType type)
		{
			var neighbors = new BitArray(4); //RIGHT, UP, LEFT, DOWN
			neighbors[0] = tileMap[i + 1, j] == type;
			neighbors[1] = tileMap[i, j + 1] == type;
			neighbors[2] = tileMap[i - 1, j] == type;
			neighbors[3] = tileMap[i, j - 1] == type;

			var intArray = new int[1];
			neighbors.CopyTo(intArray, 0);

			return (GraphicsBlockType)(1 + intArray[0]);
		}



		//TODO!!!! Make generic solution work!
		public static int[,][,] SplitArray(int[,] array, int splitCount){
			int[,][,] blockArray = new int[splitCount,splitCount][,];

			for (int i = 0; i < splitCount; i++) {
				for (int j = 0; j < splitCount; j++) {

					int[,] newArray = GetNewArray(array, splitCount, i*array.GetLength(0)/splitCount, j*array.GetLength(1)/splitCount);
			
					blockArray[i,j] = newArray;
				}
			}

			return blockArray;
		}


		public static int[,] GetNewArray(int[,] array, int splitCount, int x, int y){
			int[,] newArray = new int[array.GetLength(0) / splitCount, array.GetLength(1) / splitCount];

			for (int i = x; i < x + array.GetLength(0)/splitCount; i++) {
				for (int j = y; j < y + array.GetLength(1)/splitCount; j++) {
					newArray[i-x, j-y] = array[i,j];
				}
			}

			return newArray;
		}


		//public static GraphicsBlockType[,][,] SplitBlockArray(GraphicsBlockType[,] array, int splitCount)
		//{
		//	if (splitCount == 0 || splitCount > array.GetLength(0) || splitCount > array.GetLength(1))
		//	{
		//		Debug.LogError("Split count must be higher than 0, and less than map size! (was " + splitCount + ")");
		//		return null;
		//	}
		//	int splitWidth = array.GetLength(0) / splitCount;
		//	int splitHeight = array.GetLength(1) / splitCount;
		//	GraphicsBlockType[,][,] blockArray = new GraphicsBlockType[splitCount, splitCount][,];

		//	for (int i = 0; i < splitCount; i++)
		//	{
		//		for (int j = 0; j < splitCount; j++)
		//		{

		//			GraphicsBlockType[,] newArray = GetNewBlockArray(array, splitCount, i * splitWidth, j * splitHeight);

		//			blockArray[i, j] = newArray;
		//		}
		//	}

		//	return blockArray;
		//}


		//private static GraphicsBlockType[,] GetNewBlockArray(GraphicsBlockType[,] array, int splitCount, int x, int y)
		//{
		//	int splitWidth = array.GetLength(0) / splitCount;
		//	int splitHeight = array.GetLength(1) / splitCount;
		//	GraphicsBlockType[,] newArray = new GraphicsBlockType[splitWidth, splitHeight];

		//	for (int i = x; i < x + splitWidth; i++)
		//	{
		//		for (int j = y; j < y + splitHeight; j++)
		//		{
		//			newArray[i - x, j - y] = array[i, j];
		//		}
		//	}

		//	return newArray;
		//}


		//public static GraphicsGroundType[,][,] SplitGroundArray<GraphicsGroundType>(GraphicsGroundType[,] array, int splitCount)
		//{
		//	if (splitCount == 0 || splitCount > array.GetLength(0) || splitCount > array.GetLength(1))
		//	{
		//		Debug.LogError("Split count must be higher than 0, and less than map size! (was " + splitCount + ")");
		//		return null;
		//	}
		//	int splitWidth = array.GetLength(0) / splitCount;
		//	int splitHeight = array.GetLength(1) / splitCount;
		//	GraphicsGroundType[,][,] blockArray = new GraphicsGroundType[splitCount, splitCount][,];

		//	for (int i = 0; i < splitCount; i++)
		//	{
		//		for (int j = 0; j < splitCount; j++)
		//		{

		//			GraphicsGroundType[,] newArray = GetNewGroundArray(array, splitCount, i * splitWidth, j * splitHeight);

		//			blockArray[i, j] = newArray;
		//		}
		//	}

		//	return blockArray;
		//}


		//private static GraphicsGroundType[,] GetNewGroundArray<GraphicsGroundType>(GraphicsGroundType[,] array, int splitCount, int x, int y)
		//{
		//	int splitWidth = array.GetLength(0) / splitCount;
		//	int splitHeight = array.GetLength(1) / splitCount;
		//	GraphicsGroundType[,] newArray = new GraphicsGroundType[splitWidth, splitHeight];

		//	for (int i = x; i < x + splitWidth; i++)
		//	{
		//		for (int j = y; j < y + splitHeight; j++)
		//		{
		//			newArray[i - x, j - y] = array[i, j];
		//		}
		//	}

		//	return newArray;
		//}
	}
}