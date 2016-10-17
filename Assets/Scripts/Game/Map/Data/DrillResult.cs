using UnityEngine;
using System.Collections;
using Bolo.DataClasses;
using System;

namespace Bolo.Map
{
	public class DrillResult
	{
		public Pos pos;
		public bool removeTile = false;
		public PickupData[] pickups;
	}
}
