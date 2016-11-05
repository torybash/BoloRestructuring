using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Bolo.DataClasses;
using System;

namespace Bolo.Util
{
	[CreateAssetMenu(fileName = "WeaponLibrary", menuName = "Bolo/ScriptableObjects/WeaponLibrary", order = 1)]
	public class WeaponLibrary : LibraryObject<WeaponLibrary>
	{
		[SerializeField]
		private List<WeaponData> _weapons;

		
		public static List<WeaponData> Weapons
		{
			get
			{
				return libObject._weapons;
			}
		}

		public void Init()
		{
			//TODO Init stuff?
		}

		public WeaponData GetWeaponData(WeaponType type)
		{
			var weaponData = _weapons.First(x => x.type == type);
			return weaponData;
		}
	}

}
