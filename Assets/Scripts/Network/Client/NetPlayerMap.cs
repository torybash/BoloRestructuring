using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Bolo.Net
{
	public class NetPlayerMap : NetworkBehaviour
	{


		#region Client

		[ClientRpc]
		private void RpcSetMap(int seed)
		{
			Game.map.CreateMapFromSeed(seed);
		}

		#endregion


		#region Server
		public override void OnStartServer()
		{
			base.OnStartServer();


		}

		public void ShareMap(int seed)
		{
			RpcSetMap(seed);
		}
		#endregion

	}
}