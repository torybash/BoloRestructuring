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
		//[SerializeField]
		//private MapTiles _tileMap;

		//[SerializeField]
		//private FogOfWar _fog;

		//[SerializeField]
		//private MapCollision _mapCollision;



		#region Lifecycle
		public override void OnStartAuthority()
		{
						//Debug.LogError("OnStartAuthority - isServer: " + isServer + ", isLocalPlayer: "+ isLocalPlayer + ", connectionToClient: "+ connectionToClient);

			base.OnStartAuthority();
		}
		public override void OnStartClient()
		{
						//Debug.LogError("OnStartClient - isServer: " + isServer + ", isLocalPlayer: "+ isLocalPlayer + ", connectionToClient: "+ connectionToClient);

			base.OnStartClient();
		}
		public override void OnStartLocalPlayer()
		{
						//Debug.LogError("OnStartLocalPlayer - isServer: " + isServer + ", isLocalPlayer: "+ isLocalPlayer + ", connectionToClient: "+ connectionToClient);

			base.OnStartLocalPlayer();
		}
		public override void OnStartServer()
		{
			//Debug.LogError("OnStartServer - isServer: " + isServer + ", isLocalPlayer: "+ isLocalPlayer + ", connectionToClient: "+ connectionToClient);
			base.OnStartServer();

			//if (isLocalPlayer) return; //TODO?

			Game.map.SendMapToClient(connectionToClient);
		}
		#endregion Lifecycle

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







		#region Event callbacks
		private void OnDrillTileAt(GameEventArgs args)
		{
			var drillArgs = (DrillEventArgs) args;

			CmdDrillTileAt(drillArgs.pos, drillArgs.damage);
		}


		private void OnPlayerMovedToTile(GameEventArgs args)
		{
			Game.map.PlayerMovedToTile(args);

			//var movedArgs = (PlayerMovedToTileArgs) args;

			//_mapCollision.UpdateCollisionInArea(movedArgs.pos);
			//_fog.UpdateFog(movedArgs.pos);
			//Debug.Log("OnPlayerMovedToTile - args: " + movedArgs.pos);
		}
		#endregion Event callbacks



		
		#region Commands
		[Command]
		public void CmdDrillTileAt(Pos pos, float damage)
		{
			Game.map.DrillTileAt(pos, damage);
		}

		#endregion Commands
	}
}