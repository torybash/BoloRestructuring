using UnityEngine;
using UnityEngine.Networking;
using Bolo.DataClasses;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System;
using Bolo.Map;
using Bolo.Util;
using Bolo.Events;

namespace Bolo
{
	public class MapCommander : CommanderBehaviour
	{
		[SerializeField]
		private MapTiles _tileMap;

		[SerializeField]
		private FogOfWar _fog;

		[SerializeField]
		private MapCollision _mapCollision;



		#region Setup
		protected override void Listen()
		{
			EventManager.AddListener("DrillTileAt", OnDrillTileAt);
			EventManager.AddListener("PlayerMovedToTile", OnPlayerMovedToTile);
		}

		protected override void UnListen()
		{
			EventManager.RemoveListener("DrillTileAt", OnDrillTileAt);
			EventManager.RemoveListener("PlayerMovedToTile", OnPlayerMovedToTile);
		}
		#endregion


		#region Server
		[Server]
		public void CreateMapSeedAndShare()
		{
			var mapParams = new MapGenerationParameters { seed = Random.Range(0, int.MaxValue), size = 256 }; //TODO make size customizable!
			Game.map.RpcCreateMap(mapParams);
		}

		[Server]
		public Vector2 GetNewSpawnPosition(List<Vector2> playerPositions)
		{
			return _tileMap.GetNewSpawnPosition(playerPositions);
		}
		#endregion Server




		#region Event callbacks
		private void OnDrillTileAt(GameEventArgs args)
		{
			var drillArgs = (DrillEventArgs) args;

			CmdDrillTileAt(drillArgs.pos, drillArgs.damage);
		}

		private void OnPlayerMovedToTile(GameEventArgs args)
		{
			var movedArgs = (PlayerMovedToTileArgs) args;

			_mapCollision.UpdateCollisionInArea(movedArgs.pos);
			_fog.UpdateFog(movedArgs.pos);
			//Debug.Log("OnPlayerMovedToTile - args: " + movedArgs.pos);
		}
		#endregion Event callbacks

		#region RPCs
		[ClientRpc]
		public void RpcCreateMap(MapGenerationParameters genParams)
		{
			//Create new map
			_tileMap.InitMap(genParams, transform); // = mapGen.GenerateMap(genParams, transform);

			//Init stuff for map
			var mapInfo = _tileMap.mapInfo;
			_fog.Setup(mapInfo);
			_mapCollision.Setup(mapInfo);
		}

		[ClientRpc]
		public void RpcDrillEffectAt(Pos pos)
		{
			//TODO
		}

		[ClientRpc]
		public void RpcChangeTileAt(ChangeBlockParameters changeParams)
		{
			_tileMap.ChangeTileAt(changeParams);
			var playerPos = new Pos((int)Game.player.vehicle.transform.position.x, (int)Game.player.vehicle.transform.position.y); //TODO! convert pos, nullchecks!!!
			_fog.UpdateFog(playerPos );
			_mapCollision.UpdateCollisionInArea(playerPos);
		}
		#endregion RPCs

		
		#region Commands
		[Command]
		public void CmdDrillTileAt(Pos pos, float damage)
		{
			var result = _tileMap.DrillTileAt(pos, damage);	
			if (result.removeTile)
			{
				var changeParams = new ChangeBlockParameters { pos = pos, block = BlockType.EMPTY };
				RpcChangeTileAt(changeParams);

				//TODO Spawn pickups!
			}
			RpcDrillEffectAt(pos);


		}

		#endregion Commands
	}
}