using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using Bolo.Util;
using Bolo.DataClasses;
using Bolo.Spawns;
using Bolo.Events;

namespace Bolo.Net
{
	public class SpawnsCommander : CommanderBehaviour
	{

		[SerializeField]
		private Spawner _spawner;
		
		#region Client
		protected override void Listen()
		{
			EventManager.AddListener("RequestPlayerVehicle", OnRequestPlayerVehicle);
		}

		protected override void UnListen()
		{
			EventManager.RemoveListener("RequestPlayerVehicle", OnRequestPlayerVehicle);
		}


		
		public void OnRequestPlayerVehicle(GameEventArgs args)
		{
			Debug.Log("OnRequestPlayerVehicle - clientConn: " + connectionToClient + ", Game.localPlayer.connectionToClient: "+ Game.localPlayer.connectionToClient);

			
			CmdRequestSpawnPlayerVehicle(Game.localPlayer.connectionToClient.connectionId);
		}
		#endregion


		#region Server
		[Command]
		public void CmdRequestSpawnPlayerVehicle(int connId)
		{
			//TODO check if valid request!
			NetworkConnection netConn = null;
			if (connId >= 0 || connId < NetworkServer.connections.Count)
				netConn = NetworkServer.connections[connId];

			//Debug.Log("CmdRequestSpawnPlayerVehicle - clientConn: " + connectionToClient + ", connId: "+ connId + ", netConn: "+ netConn);

			var vehicleInstance = _spawner.SpawnPlayerVehicle();
			NetworkServer.SpawnWithClientAuthority(vehicleInstance, netConn);

		}
		#endregion
	}
}
