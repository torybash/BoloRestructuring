using UnityEngine;
using Bolo.DataClasses;
using Bolo.Player;
using System;

namespace Bolo
{
	public class PlayerController : ControllerBehaviour
	{
		[SerializeField]
		private PlayerInput _input;

		private PlayerInventory _inventory;

		public PlayerVehicle Vehicle { get; private set; }
		public PlayerInventory Inventory { get { return _inventory; } }


		#region Lifecycle
		void Update()
		{
			if (Vehicle) _input.HandleInput();
		}

		void FixedUpdate()
		{
			if (Vehicle) _input.ApplyInput();
		}
		#endregion Lifecycle


		public void Init()
		{
			_inventory = new PlayerInventory();
		}
		

		public void SetVehicle(PlayerVehicle vehicle)
		{
			this.Vehicle = vehicle;

			//Initialize stuff
			_input.Init(vehicle, Game.Cam.GetCamera());

			//Update map to get collision and fog of war
			var pos = new Pos((int)vehicle.transform.position.x,(int) vehicle.transform.position.y); //TODO Pos conversion!
			Game.Map.UpdateMap(pos);
		}

		public void AddResources(ResourceType type, int resourceCount)
		{
			_inventory.AddResources(type, resourceCount);
			
			//TODO Update GUI call, or make events?
		}

		public void ChangeOutfitSlot(OutfitData outfit, OutfitData.OutfitPlacement placement, int positionIdx)
		{
			Inventory.ChangeOutfitSlot(outfit, placement, positionIdx);

			Vehicle.UpdateOutfit();
		}
	}

}

