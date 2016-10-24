using Bolo.DataClasses;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bolo
{
	[Serializable]
	public class WeaponData
	{
		public string title;

		public float cooldownDuration;

		//Bullet stats
		public ProjectileType type; //For gfx + maybe scripts/behaviours?
		public float speed;
		public float damage;
	}
}
