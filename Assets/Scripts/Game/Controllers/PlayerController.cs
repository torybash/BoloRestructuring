using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using Bolo.Util;
using Bolo.DataClasses;
using Bolo.Player;

namespace Bolo
{
	public class PlayerController : ControllerBehaviour
	{
		private PlayerVehicle _vehicle;	
		public PlayerVehicle vehicle { get { return _vehicle; } private set { _vehicle = value; } }
		[SerializeField] private PlayerInput _input;	
		 
		public void SetVehicle(PlayerVehicle vehicle)
		{
			//Debug.LogError("SetVehicle");

			_vehicle = vehicle;
			_input.Init(vehicle, Game.cam.GetCamera());

			var pos = new Pos((int)_vehicle.transform.position.x,(int) _vehicle.transform.position.y); //TODO Pos conversion!
			Game.map.UpdateMap(pos);
		}
	}

}

