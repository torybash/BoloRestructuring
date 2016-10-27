using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bolo.Util;
using System;
using Random = UnityEngine.Random;
using Bolo.DataClasses;

namespace Bolo.Map
{
	public class MapTiles : MonoBehaviour
	{
		private MapInfo _mapInfo;
		private MapChunk[,] _mapChunks;

		private MapGenerator mapGen = new MapGenerator();

		private Dictionary<Pos, float> tileDrillDict;

		public MapInfo mapInfo
		{
			get
			{
				return _mapInfo;
			}
		}
		public MapGenerationParameters genParams { get; private set; }


		public void InitMap(MapGenerationParameters genParams, Transform transform)
		{
			this.genParams = genParams;

			//Generate map info
			_mapInfo = mapGen.GetRandomMapInfo(genParams.seed, genParams.size);
			
			//Setup tile-drilling
			tileDrillDict = new Dictionary<Pos, float>();

			//Generate chunk parameters
			int chunkAmount = mapInfo.size / mapInfo.chunkSize; 
			_mapChunks = new MapChunk[chunkAmount, chunkAmount];

			//Initialize chunk graphics-tile maps
			var graphicsGroundMap = MapGrahicsHelper.GetGraphicsGroundMap(mapInfo.groundMap);
			var graphicsBlockMap = MapGrahicsHelper.GetGraphicsBlockMap(mapInfo.blockMap);
			var graphicsGroundChunks = MapGrahicsHelper.SplitArray(graphicsGroundMap, chunkAmount);
			var graphicsBlockChunks = MapGrahicsHelper.SplitArray(graphicsBlockMap, chunkAmount);

			//Create chunks
			for (int x = 0; x < chunkAmount; x++)
			{
				for (int y = 0; y < chunkAmount; y++)
				{
					var chunkObj = Game.prefabsLib.Create("MapChunk");
					chunkObj.transform.SetParent(transform, false);
					chunkObj.GetComponent<MapChunk>().GenerateChunk(graphicsGroundChunks[x, y], graphicsBlockChunks[x, y], x * mapInfo.chunkSize, y * mapInfo.chunkSize, mapInfo.chunkSize);
					_mapChunks[x, y] = chunkObj.GetComponent<MapChunk>();
				}
			}
		}



		public Vector2 GetNewSpawnPosition(List<Vector2> playerPositions)
		{
			//TODO use playerPositions!
			//TODO prevent spawn on block (or remove block on spawn)
			var rndPos = new Vector2(Random.Range(1, mapInfo.size-1), Random.Range(1, mapInfo.size-1));
			return rndPos;
		}

		public void UpdateChunks()
		{

		}



		public DrillResult DrillTileAt(Pos pos, float damage)
		{
			var result = new DrillResult { pos = pos };
			var blockType = mapInfo.GetBlockAt(pos);

			//Debug.Log("DrillTileAt - pos: " + pos + ", dmg: "+ damage + ", blockType: "+ blockType);

			if (blockType != BlockType.EMPTY)
			{
				if (!tileDrillDict.ContainsKey(pos))
				{
					tileDrillDict.Add(pos, 3f); //TODO!!! Make tile-class/struct? Make data for start HP!
				}
				tileDrillDict[pos] -= damage;

				if (tileDrillDict[pos] <= 0)
				{
					//TODO, put somewhere that makes sense!
					var pickups = new PickupData[Random.Range(2, 5)];
					for (int i = 0; i < pickups.Length; i++)
					{
						pickups[i].resourceCount = Random.Range(4, 10);
					}
					result.removeTile = true;
					result.pickups = pickups;
				}
			}

			return result;
		}


		public void ChangeTileAt(ChangeBlockParameters changeParams)
		{
			int x = changeParams.pos.x;
			int y = changeParams.pos.y;
			var block = changeParams.block;

			mapInfo.SetBlock(x, y, block);

			UpdateGraphicsTileAt(x, y);
			UpdateGraphicsTileAt(x+1, y);
			UpdateGraphicsTileAt(x-1, y);
			UpdateGraphicsTileAt(x, y+1);
			UpdateGraphicsTileAt(x, y-1);
			//TODO Update surrounding tiles!
		}


		private void UpdateGraphicsTileAt(int x, int y)
		{
			int chunkSize = mapInfo.chunkSize;
			var block = mapInfo.GetBlockAt(x, y);
			var blockPos = new Pos(x/chunkSize, y/chunkSize);
			var tilePos = new Pos(x%chunkSize, y%chunkSize);

			var gfxBlock = MapGrahicsHelper.GetGraphicsBlockTile(mapInfo.blockMap, x, y, block);
			_mapChunks[blockPos.x, blockPos.y].ChangeBlockTile(tilePos.x, tilePos.y, gfxBlock);
		}
		
	}
}