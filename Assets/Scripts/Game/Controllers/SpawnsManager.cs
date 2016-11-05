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
			Debug.Log("OnShootProjectile - connId: " + connId + ", connectionToClient: " + connectionToClient + ", Game.localPlayer.connectionToClient: " + Game.Client.connectionToClient);

			var clientConn = Host.GetConnectionFromId(connId);
			if (clientConn != null)
			{
				var hash = default(NetworkHash128);
				var projectileInst = _spawner.GetProjectileInstance(out hash);
				projectileInst.Init(pos, dir, weapon, clientConn);

				NetworkServer.Spawn(projectileInst.gameObject, hash);
			}
		}

		public void SpawnResources(Vector2 pos, ResourceData[] pickups)
		{
			for (int i = 0; i < pickups.Length; i++)
			{
				var pickup = pickups[i];

				var hash = default(NetworkHash128);
				var resourceInst = _spawner.GetResourceInstance(out hash);
				resourceInst.Init(pickup);
				resourceInst.transform.position = pos;

				NetworkServer.Spawn(resourceInst.gameObject, hash);
			}

		}
		#endregion Server
	}

}
