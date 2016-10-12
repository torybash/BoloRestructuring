using UnityEngine;
using System.Collections;
using Bolo.Util;

namespace Bolo.Map
{
	public class Map : MonoBehaviour
	{
		private MapInfo _mapInfo;
		private MapChunk[,] _mapChunks;


		public void GenerateChunks(MapInfo mapInfo, int chunkSize)
		{	
			_mapInfo = mapInfo;

			//Generate chunks
			int chunkAmount = mapInfo._size / chunkSize; 
			_mapChunks = new MapChunk[chunkAmount, chunkAmount];

			//Initialize graphics-tile maps
			var graphicsGroundMap = MapGrahicsHelper.GetGraphicsGroundMap(mapInfo._groundMap);
			var graphicsBlockMap = MapGrahicsHelper.GetGraphicsBlockMap(mapInfo._blockMap);

			var graphicsGroundChunks = MapGrahicsHelper.SplitArray(graphicsGroundMap, chunkAmount);
			var graphicsBlockChunks = MapGrahicsHelper.SplitArray(graphicsBlockMap, chunkAmount);

			for (int x = 0; x < chunkAmount; x++)
			{
				for (int y = 0; y < chunkAmount; y++)
				{
					//Debug.Log("i, j: "+ x + ", " +y);
					var chunkObj = Game.prefabsLib.Create("MapChunk");
					chunkObj.transform.SetParent(transform, false);
					chunkObj.GetComponent<MapChunk>().GenerateChunk(graphicsGroundChunks[x, y], graphicsBlockChunks[x, y], x * chunkSize, y * chunkSize, chunkSize);
					_mapChunks[x, y] = chunkObj.GetComponent<MapChunk>();
				}
			}
		}
	}
}