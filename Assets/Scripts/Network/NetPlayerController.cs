using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetPlayerController : NetworkBehaviour
{

	[SerializeField]
	NetPlayerSpawning _spawning;

	#region Client
	public override void OnStartClient()
	{
		base.OnStartClient();

		Game.Init(this);
	}
	#endregion


}
