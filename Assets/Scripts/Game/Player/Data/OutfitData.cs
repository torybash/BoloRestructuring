using Bolo.DataClasses;
using System;
using UnityEngine;

namespace Bolo.Player
{
	[Serializable]
	public abstract class OutfitData
	{
		[Serializable]
		public enum OutfitPlacement
		{
			Inventory,
			PrimaryWeapon,
			SecondaryWeapon,
			Armour,
			Drill,
		}

		public OutfitPlacement placement;
		public int positionIdx;
	}

	[Serializable]
	public class WeaponOutfitData : OutfitData
	{
		public WeaponType weaponType;
	}

	[Serializable]
	public class DrillOutfitData : OutfitData //TODO DrillOutfitData
	{
		//public WeaponType weaponType;
	}

	[Serializable]
	public class ArmourOutfitData : OutfitData //TODO ArmourOutfitData
	{
		//public WeaponType weaponType;
	}
}