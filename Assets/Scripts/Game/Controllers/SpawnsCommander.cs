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
		public GameObject bulletPrefab; //TODO <---REMOVE

		#region Client
		protected override void Listen()
		{
			EventManager.AddListener("RequestPlayerVehicle", OnRequestPlayerVehicle);
			EventManager.AddListener("ShootProjectile", OnShootProjectile);
		}

		protected override void UnListen()
		{
			EventManager.RemoveListener("RequestPlayerVehicle", OnRequestPlayerVehicle);
			EventManager.RemoveListener("ShootProjectile", OnShootProjectile);
		}
		#endregion Client

						

		#region Event callbacks
		public void OnRequestPlayerVehicle(GameEventArgs args)
		{
			Debug.Log("OnRequestPlayerVehicle - clientConn: " + connectionToClient + ", Game.localPlayer.connectionToClient: " + Game.localPlayer.connectionToClient);


			CmdRequestSpawnPlayerVehicle();
		}

		public void OnShootProjectile(GameEventArgs args)
		{
			//Debug.Log("OnShootProjectile - clientConn: " + connectionToClient + ", Game.localPlayer.connectionToClient: " + Game.localPlayer.connectionToClient);

			var shootArgs = (ShootProjectileArgs)args;

			var bullet = Instantiate(bulletPrefab);
			bullet.GetComponent<Projectile>().Init(shootArgs.pos, shootArgs.dir * shootArgs.weapon.speed);

			 //ClientScene.RegisterSpawnHandler()
			 //CmdShootProjectile();
		}

		#endregion Event callbacks


		#region Server
		[Command]
		public void CmdRequestSpawnPlayerVehicle()
		{
			//TODO check if valid request!
			//NetworkConnection netConn = null;
			//if (connId >= 0 || connId < NetworkServer.connections.Count)
			//	netConn = NetworkServer.connections[connId];

			Debug.Log("CmdRequestSpawnPlayerVehicle - clientConn: " + connectionToClient); // + ", connId: " + connId + ", netConn: " + netConn);

			Game.spawns.SpawnPlayerVehicle(connectionToClient);

		}

		//[Command]
		//public void CmdShootProjectile()
		//{
		//	//TODO check if valid request!
		//	//NetworkConnection netConn = null;
		//	//if (connId >= 0 || connId < NetworkServer.connections.Count)
		//	//	netConn = NetworkServer.connections[connId];

		//	Debug.Log("CmdRequestSpawnPlayerVehicle - clientConn: " + connectionToClient); // + ", connId: " + connId + ", netConn: " + netConn);

		//	Game.spawns.SpawnPlayerVehicle(connectionToClient);

		//}
		#endregion
	}
}
