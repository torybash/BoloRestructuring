using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using Bolo.Util;
using UnityEngine.Events;

namespace Bolo.DataClasses
{
	public abstract class CommanderBehaviour : NetworkBehaviour
	{

		public override void OnStartClient()
		{
			base.OnStartClient();
			//if (!isServer) return;

			Listen();
		}

		public override void OnNetworkDestroy()
		{
			base.OnNetworkDestroy();
			//if (!isServer) return;

			UnListen();
		}


		protected virtual void Listen(){}
		protected virtual void UnListen(){}


	}
}