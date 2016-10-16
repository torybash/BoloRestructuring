using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using Bolo.Util;
using Bolo.DataClasses;

namespace Bolo
{
	public class PlayerController : ControllerBehaviour
	{
		private PlayerVehicle _vehicle;	
		public PlayerVehicle vehicle { get { return _vehicle; } private set { _vehicle = value; } }

		public void SetVehicle(PlayerVehicle vehicle)
		{
			_vehicle = vehicle;
		}
	}

}

