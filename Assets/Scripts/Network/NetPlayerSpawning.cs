using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetPlayerSpawning : NetworkBehaviour
{

	private NetManagerPrefabs _netPrefabs;
	private NetManagerPrefabs netPrefabs
	{
		get
		{
			if (_netPrefabs == null) _netPrefabs = NetworkManager.singleton.GetComponent<NetManagerPrefabs>(); //TODO make custom NetworkManager!!
			return _netPrefabs;
		}
	}

	#region Client
	#region Lifetime
	void Awake()
	{
		Game.events.StartListening("RequestPlayerVehicle", OnRequestPlayerVehicle);
	}

	void OnDestroy()
	{
		Game.events.StopListening("RequestPlayerVehicle", OnRequestPlayerVehicle);
	}
	#endregion


	public void OnRequestPlayerVehicle()
	{
		CmdRequestSpawnPlayerVehicle();
	}
	#endregion

	#region Server
	[Command]
	public void CmdRequestSpawnPlayerVehicle()
	{
		var vehicleInstance = netPrefabs.Create("PlayerVehicle");
		if (vehicleInstance)
		{
			NetworkServer.SpawnWithClientAuthority(vehicleInstance.gameObject, connectionToClient);
		}
	}
	#endregion

}
