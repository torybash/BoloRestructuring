﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;

namespace Bolo.Net
{
	public class NetServerSpawning : NetworkBehaviour
	{
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

		
		public List<Vector2> GetAllPlayerPositions()
		{
			var vehicles = _spawnableList.Where((x) => x.GetType().Equals(typeof(PlayerVehicle)));
			var playerPositions = new List<Vector2>();
			foreach (var item in vehicles)
			{
				playerPositions.Add(item.transform.position);
			}
			return playerPositions;
		}


		public void SpawnPlayerVehicle(NetworkConnection connectionToClient, Vector2 spawnPos)
		{
			var vehicleInstance = netPrefabs.Create("PlayerVehicle");
			vehicleInstance.transform.position = spawnPos + 0.5f * Vector2.one; //TODO make tile position helper
			if (vehicleInstance)
			{
				NetworkServer.SpawnWithClientAuthority(vehicleInstance, connectionToClient);

				_spawnableList.Add(vehicleInstance.GetComponent<NetworkSpawnable>());
			}
		}
	}
}