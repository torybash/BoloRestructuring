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
		private MapTiles _tiles;

		[SerializeField]
		private FogOfWar _fog;

		[SerializeField]
		private MapGenerator _mapGen;

		public MapTiles tiles { get { return _tiles; } }
		public MapGenerator mapGen { get { return _mapGen; } }


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


		
		private void OnDrillTileAt(GameEventArgs args)
		{
			var drillArgs = (DrillEventArgs) args;
			Debug.Log("OnDrillTileAt - args: " + drillArgs.pos + ", dmg: "+ drillArgs.damage);

			CmdDrillTileAt(drillArgs.pos.x, drillArgs.pos.y, drillArgs.damage);
		}

		private void OnPlayerMovedToTile(GameEventArgs args)
		{
			var movedArgs = (PlayerMovedToTileArgs) args;

			tiles.UpdateCollision(movedArgs.pos);
			_fog.UpdateFog(movedArgs.pos);
			Debug.Log("OnPlayerMovedToTile - args: " + movedArgs.pos);
		}


		#region RPCs
		[ClientRpc]
		public void RpcCreateMap(int seed)
		{
			//Cleanup map
			if (_tiles) Destroy(_tiles.gameObject);
			//Create new map
			_tiles = _mapGen.GenerateMap(seed, transform);
			_fog.Setup(_tiles.mapInfo);
		}

		#endregion RPCs

		
		#region Commands
		[Command]
		public void CmdDrillTileAt(int x, int y, float damage)
		{
			_tiles.DrillTileAt(x, y, damage);
		}

		#endregion RPCs
	}
}