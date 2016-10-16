using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

namespace Bolo.Net
{
	public class NetPlayer : NetworkBehaviour
	{


		

		#region Client
		public override void OnStartLocalPlayer()
		{
			base.OnStartLocalPlayer();

			Game.SetLocalPlayer(this);
		}

		#endregion

		#region Server
		public override void OnStartServer()
		{
			base.OnStartServer();

			Host.AddPlayer(this);
		}
		#endregion Server
	}
}