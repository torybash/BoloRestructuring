using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bolo.DataClasses;

namespace Bolo.Util
{
	[CreateAssetMenu(fileName = "WeaponLibrary", menuName = "Bolo/ScriptableObjects/WeaponLibrary", order = 1)]
	public class WeaponLibrary : LibraryObject<WeaponLibrary>
	{
		[SerializeField]
		private List<WeaponData> _weapons;

		
		public static List<WeaponData> GetWeapons()
		{
			return libObject._weapons;
		}

	}

}
