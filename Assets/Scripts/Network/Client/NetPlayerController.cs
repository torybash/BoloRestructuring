using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Bolo.Net
{
	public class NetPlayerController : NetworkBehaviour
	{
		[SerializeField]
		private NetSpawnCommander _spawnComm;
		[SerializeField]
		private NetMapCommander _mapComm;

		public NetSpawnCommander spawnComm { get { return _spawnComm; } }
		public NetMapCommander mapComm { get { return _mapComm; } }


		#region Client
		public override void OnStartLocalPlayer()
		{
			base.OnStartLocalPlayer();

			Game.Init(this);
		}
		#endregion
	}
}