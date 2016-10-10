using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Bolo.Net
{
	public class NetPlayerSpawning : NetworkBehaviour
	{

		private NetManagerPrefabs _netPrefabs;
		public NetManagerPrefabs netPrefabs
		{
			get
			{
				if (_netPrefabs == null) _netPrefabs = NetworkManager.singleton.GetComponent<NetManagerPrefabs>(); //TODO make custom NetworkManager!!
				return _netPrefabs;
			}
		}

		#region Lifetime
		void OnDestroy()
		{
			if (isLocalPlayer)
			{
				Game.events.StopListening("RequestPlayerVehicle", OnRequestPlayerVehicle);
			}
		}
		#endregion


		#region Client
		public override void OnStartLocalPlayer()
		{
			base.OnStartLocalPlayer();

			Game.events.StartListening("RequestPlayerVehicle", OnRequestPlayerVehicle);
		}

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
}