using UnityEngine;
using System.Collections;
using Bolo.DataClasses;

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

		void Update()
		{
			//TODO Receive events, for focus etc.
			//TODO Follow on transform/player
			//TODO Player movement of object?

			if (Game.player.vehicle)
			{
				var newCamPos = Game.player.vehicle.transform.position;
				newCamPos.z = cam.transform.position.z;
				cam.transform.position = newCamPos;
			}

		}
	}
}