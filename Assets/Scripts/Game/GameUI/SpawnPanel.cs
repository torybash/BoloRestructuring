using UnityEngine;
using System.Collections;

public class SpawnPanel : UIPanel
{



	public void Click_SpawnVehicle()
	{
		Game.events.TriggerEvent("RequestPlayerVehicle");
	}
}
