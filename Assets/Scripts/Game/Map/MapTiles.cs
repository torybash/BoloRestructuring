using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bolo.Util;
using System;
using Random = UnityEngine.Random;

namespace Bolo.Map
{
	public class MapTiles : MonoBehaviour
	{
		
		private MapCollision _mapCollision;
		private MapInfo _mapInfo;
		private MapChunk[,] _mapChunks;


		public MapInfo mapInfo
		{
			get
			{
				return _mapInfo;
			}
		}
		public void Init(MapInfo mapInfo, int chunkSize)
		{
			_mapCollision = new GameObject("MapCollision").AddComponent<MapCollision>();
			_mapCollision.transform.SetParent(transform);
			_mapCollision.mapInfo = mapInfo;
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
					var chunkObj = Game.prefabsLib.Create("MapChunk");
					chunkObj.transform.SetParent(transform, false);
					chunkObj.GetComponent<MapChunk>().GenerateChunk(graphicsGroundChunks[x, y], graphicsBlockChunks[x, y], x * chunkSize, y * chunkSize, chunkSize);
					_mapChunks[x, y] = chunkObj.GetComponent<MapChunk>();
				}
			}
		}



		public Vector2 GetNewSpawnPosition(List<Vector2> playerPositions)
		{
			//TODO use playerPositions!
			//TODO prevent spawn on block (or remove block on spawn)
			var rndPos = new Vector2(Random.Range(1, mapInfo._size-1), Random.Range(1, mapInfo._size-1));
			return rndPos;
		}

		public void UpdateChunks()
		{

		}

		public void UpdateCollision(Pos pos)
		{
			_mapCollision.GenerateCollisionInArea(pos);
		}

		public void DrillTileAt(int x, int y, float damage)
		{
			//int tileBeforeDrilling = mapInfo.blockMap[x,y];
			//if (tileBeforeDrilling == (int)Constants.BlockTiles.ROCK){
			//	if (player.lastDrilledTileX != x || player.lastDrilledTileY != y){
			//		//Refill hp if drilling new tile
			//		player.currentDrilledTileHpLeft = 3;
			//	}

			//	player.lastDrilledTileX = x;
			//	player.lastDrilledTileY = y;
			//	player.currentDrilledTileHpLeft -= drillDamage;
			//	ChangeBlockTileAt(x,y, tileBeforeDrilling);


			//	if (player.currentDrilledTileHpLeft <= 0){
			//		ChangeBlockTileAt(x,y, (int)Constants.BlockTiles.EMPTY);
			//		RemoveCollisionBoxAt(x, y);
			//	}
			//}else if (tileBeforeDrilling == (int)Constants.BlockTiles.CRYSTAL){

			//}
		}
	}
}