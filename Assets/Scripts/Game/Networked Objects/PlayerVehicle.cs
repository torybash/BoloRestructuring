using UnityEngine;
using System.Collections;

public class PlayerVehicle : Actor
{
	public override void OnStartAuthority()
	{
		Debug.Log("OnStartAuthority");
		base.OnStartAuthority();
	}

	public override void OnStartLocalPlayer()
	{
		Debug.Log("OnStartLocalPlayer");
		base.OnStartLocalPlayer();
	}

	public override void OnStartClient()
	{
		Debug.Log("OnStartClient");
		base.OnStartClient();
	}

	public override void OnStartServer()
	{
		Debug.Log("OnStartServer");
		base.OnStartServer();
	}


}
