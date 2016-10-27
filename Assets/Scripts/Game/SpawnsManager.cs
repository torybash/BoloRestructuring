using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using Bolo.Spawns;

namespace Bolo
{
	public class SpawnsManager : NetworkBehaviour
	{

		[SerializeField]
		private Spawner _spawner;

		#region Server
		//[Server]
		public void SpawnPlayerVehicle(NetworkConnection connectionToClient)
		{
			var vehicleInstance = _spawner.SpawnPlayerVehicle(); 
			NetworkServer.SpawnWithClientAuthority(vehicleInstance, connectionToClient);
		}
		#endregion Server
	}

}
