using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using Bolo.Spawns;
using Bolo.Net;

namespace Bolo
{
	public class SpawnsManager : NetworkBehaviour
	{
		public GameObject bulletPrefab; //TODO <---REMOVE, use prefab library

		[SerializeField]
		private Spawner _spawner;

		#region Server
		//[Server]
		public void SpawnPlayerVehicle(NetworkConnection connectionToClient)
		{
			var vehicleInstance = _spawner.GetPlayerVehicleInstance(); 
			NetworkServer.SpawnWithClientAuthority(vehicleInstance, connectionToClient);
		}

		public void ShootProjectile(Vector3 pos, Vector3 dir, WeaponData weapon, int connId)
		{
			Debug.Log("OnShootProjectile - connId: " + connId + ", connectionToClient: " + connectionToClient + ", Game.localPlayer.connectionToClient: " + Game.localPlayer.connectionToClient);

			var clientConn = Host.GetConnectionFromId(connId);
			if (clientConn != null)
			{
				var hash = default(NetworkHash128);
				var projectileInst = _spawner.GetProjectileInstance(out hash);
				projectileInst.Init(pos, dir, weapon, clientConn);

				NetworkServer.Spawn(projectileInst.gameObject, hash);
			}
		}
		#endregion Server
	}

}
