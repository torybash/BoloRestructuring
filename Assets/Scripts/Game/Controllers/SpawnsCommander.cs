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
			CmdRequestSpawnPlayerVehicle();
		}
		#endregion


		#region Server
		[Command]
		public void CmdRequestSpawnPlayerVehicle()
		{
			//TODO check if valid request!

			var playerPositions = _spawner.GetAllPlayerPositions();

			var spawnPos = Game.map.tiles.GetNewSpawnPosition(playerPositions);
			_spawner.SpawnPlayerVehicle(connectionToClient, spawnPos);
		}
		#endregion
	}
}
