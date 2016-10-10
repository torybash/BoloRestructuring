using UnityEngine;
using System.Collections;
using Bolo;

namespace Bolo.UI
{
	public class SpawnPanel : UIPanel
	{
		public void Click_SpawnVehicle()
		{
			Game.events.TriggerEvent("RequestPlayerVehicle");
		}
	}
}
