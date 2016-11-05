using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bolo;
using Bolo.Util;
using Bolo.Events;
using UnityEngine.EventSystems;
using System;
using Bolo.DataClasses;

namespace Bolo.Player
{
	public class VehiclePanel : UIPanel
	{

		[SerializeField]
		private OutfitItem _outfitItemTemplate;

		//Outfit slot items
		[SerializeField] private OutfitSlotItem[] _outfitSlots;
		//Inventory slot items
		[SerializeField] private OutfitSlotItem[] _inventorySlots;



		//Outfit items
		private List<OutfitItem> _outfitItems = new List<OutfitItem>();


		//Upgrade items


		private bool _isDragging;

		private const int WEAPON1_SLOT	= 0;
		private const int WEAPON2_SLOT	= 1;
		private const int DRILL_SLOT	= 2;
		private const int ARMOUR_SLOT	= 3;

		private EventSystem _Events { get; set; }




		void Awake()
		{
			_Events = EventSystem.current;

			for (int i = 0; i < _outfitSlots.Length; i++)
			{
				_outfitSlots[i].Init((OutfitData.OutfitPlacement)(i + 1));
			}
			for (int i = 0; i < _inventorySlots.Length; i++)
			{
				_inventorySlots[i].Init(OutfitData.OutfitPlacement.Inventory, i);
			}
		}



		protected override void Opening()
		{
			//TODO init stuff

			foreach (var item in _outfitItems) //TODO pool instead of destroying
			{
				Destroy(item.gameObject);
			}

			foreach (var outfit in Game.Player.Inventory.Outfit)
			{
				var slot = GetSlotFromOutfit(outfit);

				var outfitInst = Instantiate(_outfitItemTemplate);
				outfitInst.gameObject.SetActive(true);

				SetItemParent(outfitInst, slot.transform);

				outfitInst.Init(this, slot, outfit);
				_outfitItems.Add(outfitInst);
			}
		}

		private OutfitSlotItem GetSlotFromOutfit(OutfitData outfit)
		{
			OutfitSlotItem slot = null;
			switch (outfit.placement)
			{
			case OutfitData.OutfitPlacement.Inventory:
				slot = _inventorySlots[outfit.positionIdx];
				break;
			case OutfitData.OutfitPlacement.PrimaryWeapon:
				slot = _outfitSlots[WEAPON1_SLOT];
				break;
			case OutfitData.OutfitPlacement.SecondaryWeapon:
				slot = _outfitSlots[WEAPON2_SLOT];
				break;
			case OutfitData.OutfitPlacement.Armour:
				slot = _outfitSlots[DRILL_SLOT];
				break;
			case OutfitData.OutfitPlacement.Drill:
				slot = _outfitSlots[ARMOUR_SLOT];
				break;
			}
			return slot;
		}

		internal void DragOutfit(OutfitItem outfitItem, PointerEventData data)
		{
			//data.position
			outfitItem.GetComponent<CanvasGroup>().blocksRaycasts = false;

			_isDragging = true;
			StartCoroutine(_DoDragOutfit(outfitItem));
		}

		internal void EndDragOutfit(OutfitItem outfitItem, PointerEventData data)
		{
			var raycastObj = data.pointerCurrentRaycast.gameObject;
			if (raycastObj != null && raycastObj.GetComponent<OutfitSlotItem>() != null)
			{
				//Update player inventory + update player vehicle weapons

				var slot = raycastObj.GetComponent<OutfitSlotItem>();

				//outfitItem.SetSlot(slot);

				//Get outfit from Game.Player.Inventory.Outfit
				Game.Player.ChangeOutfitSlot(outfitItem.Outfit, slot.Placement, slot.PositionIdx);

				//Get type of slot 

				//Set placement and inventory pos

				//Update UI 
				outfitItem.Slot = slot;
				SetItemParent(outfitItem, slot.transform);
			}
			else
			{
				SetItemParent(outfitItem, outfitItem.Slot.transform);
			}

			outfitItem.GetComponent<CanvasGroup>().blocksRaycasts = true;			
			_isDragging = false;
		}

		#region Coroutines
		private IEnumerator _DoDragOutfit(OutfitItem outfitItem)
		{
			SetItemParent(outfitItem, transform);
	
			while (_isDragging)
			{
				
				outfitItem.transform.position = Input.mousePosition;
				yield return null;
			}
			
		}
		#endregion Coroutines


		//TODO Move to Helper class?
		#region Helpers 
		private void SetItemParent(OutfitItem outfitItem, Transform parent)
		{
			outfitItem.transform.SetParent(parent, false);
			outfitItem.transform.localScale = Vector2.one;
			outfitItem.transform.localPosition = Vector2.one;
			//outfitItem.GetComponent<RectTransform>().offsetMax = Vector2.zero;
			//outfitItem.GetComponent<RectTransform>().offsetMin = Vector2.zero;
		}
		#endregion Helpers
	}
}
