using UnityEngine;
using System.Collections;
using Bolo;
using Bolo.Util;
using Bolo.Events;

namespace Bolo.UI
{
	public class SpawnPanel : UIPanel
	{
		public void Click_SpawnVehicle()
		{
			EventManager.TriggerEvent("RequestPlayerVehicle", null);
		}
	}
}
