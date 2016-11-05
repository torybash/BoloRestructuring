using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bolo.DataClasses;

namespace Bolo.Player
{
	public class PlayerInventory
	{
		private Dictionary<ResourceType, int> _resources;

		private List<OutfitData> _outfit;

		public List<OutfitData> Outfit { get { return _outfit; } } //TODO share outfit/inventory in other way?

		public PlayerInventory()
		{
			var enumVals = Enum.GetValues(typeof(ResourceType));
			_resources = new Dictionary<ResourceType, int>();

			for (int i = 1; i < enumVals.Length; i++)
			{
				_resources.Add((ResourceType)enumVals.GetValue(i), 0);
			}

			_outfit = new List<OutfitData>();


			//TODO: Add start weapon from server msg instead?
			var cannonOutfit = new WeaponOutfitData { placement = OutfitData.OutfitPlacement.PrimaryWeapon, weaponType = WeaponType.CANNON };
			_outfit.Add(cannonOutfit);

			//DBG Adding all weapons to inventory TODO REMOVE!
			for (int i = 1; i < Enum.GetValues(typeof(WeaponType)).Length; i++)
			{
				var weaponType = (WeaponType)Enum.GetValues(typeof(WeaponType)).GetValue(i); 
				var weaponOutfit = new WeaponOutfitData { placement = OutfitData.OutfitPlacement.Inventory, weaponType = weaponType, positionIdx = i - 1};
				_outfit.Add(weaponOutfit);
			}


		}


		public WeaponType GetPrimaryWeapon()
		{
			var weapon = WeaponType.NONE;
			var outfitData = _outfit.FirstOrDefault(x => x.placement == OutfitData.OutfitPlacement.PrimaryWeapon);
			if (outfitData != null) weapon = (outfitData as WeaponOutfitData).weaponType;
			return weapon;
		}

		public WeaponType GetSecondaryWeapon()
		{
			var weapon = WeaponType.NONE;
			var outfitData = _outfit.FirstOrDefault(x => x.placement == OutfitData.OutfitPlacement.SecondaryWeapon);
			if (outfitData != null) weapon = (outfitData as WeaponOutfitData).weaponType;
			return weapon;
		}

		public void AddResources(ResourceType type, int resourceCount)
		{
			_resources[type] += resourceCount;

			D.Log(ObjectDumper.Dump(_resources));
		}

		public void ChangeOutfitSlot(OutfitData outfit, OutfitData.OutfitPlacement placement, int positionIdx)
		{
			//int outfitIdx = _outfit.IndexOf(outfit);
			

			outfit.placement = placement;
			outfit.positionIdx = positionIdx;


		}
	}
}
