using UnityEngine;
using Bolo.Events;

namespace Bolo.Player
{
	public class SpawnPanel : UIPanel
	{
		public void Click_SpawnVehicle()
		{
			EventManager.TriggerEvent("RequestPlayerVehicle", null);
		}
	}
}
