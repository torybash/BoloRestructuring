﻿using UnityEngine;
using System.Collections;

namespace Bolo
{
	public class PlayerVehicle : Actor
	{
		public override void OnStartAuthority()
		{
			Debug.Log("OnStartAuthority");
			base.OnStartAuthority();

			Game.SetVehicle(this);
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
}