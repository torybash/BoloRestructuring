using UnityEngine;
using UnityEngine.Networking;
using Bolo.DataClasses;
using Bolo.Events;

namespace Bolo
{
	public class MapCommander : CommanderBehaviour
	{

		#region Lifecycle
		public override void OnStartServer()
		{
			//Debug.LogError("OnStartServer - isServer: " + isServer + ", isLocalPlayer: "+ isLocalPlayer + ", connectionToClient: "+ connectionToClient);
			base.OnStartServer();

			if (!NetworkClient.active) //Dont send if host's commander
				Game.Map.SendMapToClient(connectionToClient);
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
			Game.Map.PlayerMovedToTile(args);

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
			Game.Map.DrillTileAt(pos, damage);
		}
		#endregion Commands
	}
}