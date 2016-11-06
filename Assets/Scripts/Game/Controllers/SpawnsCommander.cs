using UnityEngine;
using UnityEngine.Networking;
using Bolo.DataClasses;
using Bolo.Events;

namespace Bolo.Net
{
	public class SpawnsCommander : CommanderBehaviour
	{
		

		#region Client
		protected override void Listen()
		{
			EventManager.AddListener("RequestPlayerVehicle", OnRequestPlayerVehicle);
		}

		protected override void UnListen()
		{
			EventManager.RemoveListener("RequestPlayerVehicle", OnRequestPlayerVehicle);
		}
		#endregion Client


		#region Event callbacks
		public void OnRequestPlayerVehicle(GameEventArgs args)
		{
			Debug.Log("OnRequestPlayerVehicle - clientConn: " + connectionToClient + ", Game.localPlayer.connectionToClient: " + Game.Client.connectionToClient);
			CmdRequestSpawnPlayerVehicle();
		}
		#endregion Event callbacks


		#region Server
		[Command]
		public void CmdRequestSpawnPlayerVehicle()
		{
			//TODO check if valid request!
			Debug.Log("CmdRequestSpawnPlayerVehicle - clientConn: " + connectionToClient);
			Game.Spawns.SpawnPlayerVehicle(connectionToClient);
		}
		#endregion
	}
}
