using UnityEngine;
using UnityEngine.UI;
using Bolo.Events;
using UnityEngine.EventSystems;
using Bolo.DataClasses;

namespace Bolo.Player
{
	public class OutfitItem : MonoBehaviour
	{
		//TODO inherited classes for weapon/armour etc? Types at least?
		[SerializeField]
		private Text nameTxt;
		[SerializeField]
		private Image iconImg;


		private VehiclePanel _Panel
		{
			get; set;
		}

		public OutfitSlotItem Slot
		{
			get; set;
		}
		public OutfitData Outfit
		{
			get; private set;
		}

		public void Init(VehiclePanel panel, OutfitSlotItem slot, OutfitData outfit)
		{
			_Panel = panel;
			Slot = slot;
			Outfit = outfit;

			var weaponData = outfit as WeaponOutfitData;
			if (weaponData != null)
			{
				nameTxt.text = WeaponLibrary.GetWeaponData(weaponData.weaponType).name;
				iconImg.sprite = WeaponLibrary.GetWeaponData(weaponData.weaponType).iconSprite;
			}
		}

		public void OnStartDrag(BaseEventData data)
		{
			Debug.Log("OnStartDrag - data.selectedObject: " + data.selectedObject);

			_Panel.DragOutfit(this, (PointerEventData)data);
		}

		public void OnEndDrag(BaseEventData data)
		{
			Debug.Log("OnEndDrag - data.selectedObject: " + data.selectedObject);

			_Panel.EndDragOutfit(this, (PointerEventData)data);
		}
	}
}