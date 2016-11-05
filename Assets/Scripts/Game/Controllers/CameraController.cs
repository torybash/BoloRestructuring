using UnityEngine;
using System.Collections;
using Bolo.DataClasses;
using System;

namespace Bolo
{
	[RequireComponent(typeof(Camera))]
	public class CameraController : ControllerBehaviour
	{
		private Camera _cam;
		private Camera cam
		{
			get
			{
				if (_cam == null) _cam = Camera.main;
				return _cam;
			}

		}

		#region Lifecycle
		void FixedUpdate()
		{
			//TODO Receive events, for focus etc.
			//TODO Follow on transform/player
			//TODO Player movement of object?

			if (Game.Player.Vehicle)
			{
				var newCamPos = Game.Player.Vehicle.transform.position;
				newCamPos.z = cam.transform.position.z;
				cam.transform.position = newCamPos;
			}

		}
		#endregion Lifecycle

		public Camera GetCamera()
		{
			return cam;
		}

		public void Init()
		{
			//TODO init camera parameters for client
		}

	}
}