using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
using Bolo.DataClasses;

namespace Bolo
{
	[CreateAssetMenu(fileName = "WeaponLibrary", menuName = "Bolo/ScriptableObjects/WeaponLibrary", order = 1)]
	public class WeaponLibrary : LibraryObject<WeaponLibrary>
	{
		[SerializeField]
		private List<WeaponData> _weapons;

		public static List<WeaponData> Weapons { get { return sLibObject._weapons; } }


		public static WeaponData GetWeaponData(WeaponType type)
		{
			var weaponData = sLibObject._weapons.First(x => x.type == type);
			return weaponData;
		}
	}

}
