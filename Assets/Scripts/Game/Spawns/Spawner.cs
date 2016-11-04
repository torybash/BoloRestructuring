using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bolo.Net;
using System.Linq;
using UnityEngine.Networking;

namespace Bolo.Spawns
{
	public class Spawner : MonoBehaviour
	{

		[SerializeField] private Projectile _projectilePrefab;
		private SpawnPool<Projectile> _projectilePool;

		[SerializeField] private ResourcePickup _resourcePickupPrefab;
		private SpawnPool<ResourcePickup> _resourcePickupPool;

	 	private List<NetworkSpawnable> _spawnableList = new List<NetworkSpawnable>();

		//TODO make custom NetworkManager!!
		private NetManagerPrefabs _netPrefabs;
		public NetManagerPrefabs netPrefabs
		{
			get
			{
				if (_netPrefabs == null) _netPrefabs = NetworkManager.singleton.GetComponent<NetManagerPrefabs>(); 
				return _netPrefabs;
			}
		}

		void Awake()
		{
			_projectilePool = new SpawnPool<Projectile>(_projectilePrefab, 100);
			_resourcePickupPool = new SpawnPool<ResourcePickup>(_resourcePickupPrefab, 100);
		}

		private List<Vector2> GetAllPlayerPositions()
		{
			var vehicles = _spawnableList.Where((x) => x.GetType().Equals(typeof(PlayerVehicle)));
			var playerPositions = new List<Vector2>();
			foreach (var item in vehicles)
			{
				playerPositions.Add(item.transform.position);
			}
			return playerPositions;
		}


		public GameObject GetPlayerVehicleInstance()
		{
			var playerPositions = GetAllPlayerPositions();
			var spawnPos = Game.map.GetNewSpawnPosition(playerPositions);
			var vehicleInstance = netPrefabs.Create("PlayerVehicle");

			vehicleInstance.transform.position = spawnPos + 0.5f * Vector2.one; //TODO make tile position helper
			if (vehicleInstance)
			{
				//var localConn = Game.localPlayer.connectionToClient;
				_spawnableList.Add(vehicleInstance.GetComponent<NetworkSpawnable>());
			}

			return vehicleInstance;
		}

		public Projectile GetProjectileInstance(out NetworkHash128 assetId)
		{
			assetId = _projectilePool.assetId;
			var projectileInst = _projectilePool.GetInstance();
			return projectileInst.GetComponent<Projectile>();
		}
		
		public ResourcePickup GetResourceInstance(out NetworkHash128 assetId)
		{
			assetId = _resourcePickupPool.assetId;
			var projectileInst = _resourcePickupPool.GetInstance();
			return projectileInst.GetComponent<ResourcePickup>();
		}

		
	}
}
