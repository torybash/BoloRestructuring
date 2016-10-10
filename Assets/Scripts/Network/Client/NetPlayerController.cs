using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Bolo.Net
{
	public class NetPlayerController : NetworkBehaviour
	{
		[SerializeField]
		private NetPlayerSpawning _spawning;
		[SerializeField]
		private NetPlayerMap _map;

		public NetPlayerSpawning spawning { get { return _spawning; } }
		public NetPlayerMap map { get { return _map; } }


		#region Client
		public override void OnStartClient()
		{
			base.OnStartClient();

			Game.Init(this);
		}

		public override void OnStartLocalPlayer()
		{
			base.OnStartLocalPlayer();
		}
		#endregion

		#region Server
		public override void OnStartServer()
		{
			base.OnStartServer();
		}
		#endregion
	}
}