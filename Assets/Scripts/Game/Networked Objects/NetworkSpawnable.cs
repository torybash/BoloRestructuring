using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Bolo
{
	public abstract class NetworkSpawnable : NetworkBehaviour
	{

		[SerializeField]
		private NetworkTransform _netTrans;
		
		public NetworkTransform netTrans
		{
			get
			{
				if (_netTrans == null) _netTrans = GetComponent<NetworkTransform>();
				return _netTrans;
			}
		}

		//public override void OnStartClient() {
		//	base.OnStartClient();
		//}
	}
}