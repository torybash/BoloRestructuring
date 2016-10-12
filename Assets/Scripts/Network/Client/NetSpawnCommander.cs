using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Bolo.Net
{
	public class NetSpawnCommander : NetworkBehaviour
	{

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
			//TODO check if valid request!

			Game.server.CreatePlayerVehicle(connectionToClient);
		}
		#endregion

	}
}