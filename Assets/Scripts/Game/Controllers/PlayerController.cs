using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using Bolo.Util;
using Bolo.DataClasses;
using Bolo.Network;

namespace Bolo
{
	public class PlayerController : ControllerBehaviour
	{
		private PlayerVehicle _vehicle;	
		public PlayerVehicle vehicle { get { return _vehicle; } private set { _vehicle = value; } }
		[SerializeField] private PlayerInput _input;	
		//public PlayerInput input { get { return _input; } private set { _input = value; } }

		public void SetVehicle(PlayerVehicle vehicle)
		{
			_vehicle = vehicle;
			_input.vehicle = vehicle;
		}
	}

}

