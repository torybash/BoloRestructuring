﻿using UnityEngine;
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

		public MapTiles tiles { get { return _tileMap; } }


		



		#region Setup
		//protected override void Listen()
		//{
		//	EventManager.AddListener<DrillEventArgs>("DrillTileAt", OnDrillTileAt);
		//	EventManager.AddListener<PlayerMovedToTileArgs>("PlayerMovedToTile", OnPlayerMovedToTile);

		//}

		//protected override void UnListen()
		//{
		//	EventManager.RemoveListener<DrillEventArgs>("DrillTileAt", OnDrillTileAt);
		//	EventManager.RemoveListener<PlayerMovedToTileArgs>("PlayerMovedToTile", OnPlayerMovedToTile);
		//}

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

			tiles.UpdateCollision(movedArgs.pos);
			_fog.UpdateFog(movedArgs.pos);
			Debug.Log("OnPlayerMovedToTile - args: " + movedArgs.pos);
		}
		#endregion Event callbacks

		#region RPCs
		[ClientRpc]
		public void RpcCreateMap(MapGenerationParameters genParams)
		{
			//Create new map
			_tileMap.InitMap(genParams, transform); // = mapGen.GenerateMap(genParams, transform);

			//Init stuff for map
			_fog.Setup(_tileMap);
		}

		[ClientRpc]
		public void RpcDrillEffectAt(Pos pos)
		{
			
		}

		[ClientRpc]
		public void RpcChangeTileAt(ChangeBlockParameters changeParams)
		{
			_tileMap.ChangeTileAt(changeParams);
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