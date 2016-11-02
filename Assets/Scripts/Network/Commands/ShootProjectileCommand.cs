using Bolo.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Bolo.Net
{
	public class ShootProjectileCommand
	{
		public WeaponType type;
		public int upgradeLevel;

		public Vector3 dir;
		public Vector3 pos;
	}
}
