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

		public PlayerVehicle vehicle { get; private set; }


		#region Lifecycle
		void Update()
		{
			if (vehicle) _input.HandleInput();
		}

		void FixedUpdate()
		{
			if (vehicle) _input.ApplyInput();
		}
		#endregion Lifecycle


		public void Init()
		{
			_inventory = new PlayerInventory();
		}
		

		public void SetVehicle(PlayerVehicle vehicle)
		{
			this.vehicle = vehicle;

			//Initialize stuff
			_input.Init(vehicle, Game.cam.GetCamera());

			//Update map to get collision and fog of war
			var pos = new Pos((int)vehicle.transform.position.x,(int) vehicle.transform.position.y); //TODO Pos conversion!
			Game.map.UpdateMap(pos);
		}

		public void AddResouces(ResourceType type, int resourceCount)
		{
			_inventory.AddResources(type, resourceCount);
			
			//TODO Update GUI call, or make events?
		}
	}

}

