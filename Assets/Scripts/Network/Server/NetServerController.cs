using UnityEngine;
using UnityEngine.Networking;


namespace Bolo.Net
{
	public class NetServerController : NetworkBehaviour
	{
		[SerializeField]
		private NetServerSpawning _spawning;

		

		[ServerCallback]
		void Awake()
		{
			Game.InitServer(this);
		}

		[ServerCallback]
		void Start()
		{
			InitGame();
		}

		private void InitGame()
		{
			int mapSeed = Game.map.CreateMapSeed();

			Game.player.mapComm.RpcSetMap(mapSeed);
		}

		public void CreatePlayerVehicle(NetworkConnection connectionToClient)
		{
			var playerPositions = _spawning.GetAllPlayerPositions();

			var spawnPos = Game.map.GetNewSpawnPosition(playerPositions);
			_spawning.SpawnPlayerVehicle(connectionToClient, spawnPos);
		}
	}
}