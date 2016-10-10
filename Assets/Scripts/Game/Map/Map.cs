using UnityEngine;
using System.Collections;

namespace Bolo.Map
{
	public class Map : MonoBehaviour
	{
		private MapInfo _mapInfo;
		private MapChunk[,] _mapChunks;


		public void GenerateChunks(MapInfo mapInfo)
		{
			_mapInfo = mapInfo;

			//Initialize graphics-tile maps
			//int[,] graphicsGroundMap = GetGraphicsGroundMap(mapInfo.groundMap);
			//int[,] graphicsBlockMap = GetGraphicsBlockMap(mapInfo.blockMap);

			//Generate chunks
			int chunkAmount = mapInfo.groundMap.Length / 8; //TODO!!
			//int chunkAmount = mapSize / chunkSize;
			//mapChunks = new MapChunk[chunkAmount, chunkAmount];
			//int[,][,] graphicsGroundChunks = SplitArray(graphicsGroundMap);
			//int[,][,] graphicsBlockChunks = SplitArray(graphicsBlockMap);

			for (int i = 0; i < chunkAmount; i++)
			{
				for (int j = 0; j < chunkAmount; j++)
				{
					var mapChunkInst = Game.prefabsLib.Create("MapChunk");
					//mapChunkInst.GetComponent<MapChunk>().GenerateChunk(graphicsGroundChunks[i, j], graphicsBlockChunks[i, j], i * chunkSize, j * chunkSize, chunkSize);
					//mapChunks[i, j] = mapChunk.GetComponent<MapChunk>();
				}
			}
		}
	}
}