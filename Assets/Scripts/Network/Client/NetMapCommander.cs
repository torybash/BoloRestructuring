using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Bolo.Net
{
	public class NetMapCommander : NetworkBehaviour
	{


		#region Client

		[ClientRpc]
		public void RpcSetMap(int seed)
		{
			Game.map.CreateMapFromSeed(seed);
		}

		#endregion


		#region Server
		public override void OnStartServer()
		{
			base.OnStartServer();
		}

		//public void ShareMap(int seed)
		//{
		//	RpcSetMap(seed);
		//}
		#endregion

	}
}