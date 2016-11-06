using UnityEngine;
using UnityEngine.Networking;

namespace Bolo.DataClasses
{
	public abstract class CommanderBehaviour : NetworkBehaviour
	{

		public override void OnStartLocalPlayer()
		{
			base.OnStartLocalPlayer();

			Listen();
		}

		public override void OnNetworkDestroy()
		{
			base.OnNetworkDestroy();

			UnListen();
		}


		protected virtual void Listen(){}
		protected virtual void UnListen(){}


	}
}