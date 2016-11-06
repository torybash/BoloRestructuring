using UnityEngine;
using System.Collections;
using Bolo.Events;
using Bolo.DataClasses;

namespace Bolo.Player
{
	public class PlayerInput : MonoBehaviour
	{
		private PlayerInputData _input = new PlayerInputData();

		private PlayerVehicle _vehicle;
		private Camera _cam;
		




		public void Init(PlayerVehicle vehicle, Camera cam)
		{
			_vehicle = vehicle;
			_cam = cam;
		}

		public void HandleInput()
		{
			_input.moveInput.x = Input.GetAxis("Horizontal"); 
			_input.moveInput.y = Input.GetAxis("Vertical");

			_input.useAction = Input.GetKey(KeyCode.Space);

			_input.mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);

			_input.primaryShoot = Input.GetMouseButton(0);
			_input.secondaryShoot = Input.GetMouseButton(1);
		}

		public void ApplyInput()
		{
			Movement();
			UseAction();
			MouseInput();
		}




		
		private void Movement()
		{
			var currPos = new Pos( (int)_vehicle.bodyVec.x, (int)_vehicle.bodyVec.y);
			var moveVec = new Vector2 (_input.moveInput.x, _input.moveInput.y) * Time.fixedDeltaTime;

			var endWorldPos = _vehicle.ApplyMovement(moveVec);

			//Check if reched new tile
			var newPos = new Pos((int)endWorldPos.x, (int)endWorldPos.y);
			if (currPos != newPos){
				//Moved to new tile
				EventManager.TriggerEvent("PlayerMovedToTile", new PlayerMovedToTileArgs(newPos));
			}
		}

		private void UseAction()
		{
			_vehicle.SetDrilling(_input.useAction);
		}

		private void MouseInput()
		{
			//TODO, other stuff than shooting!?
			_vehicle.SetDirection(_input.mousePosition);
			if (_input.primaryShoot) _vehicle.Shooting(WeaponPositionType.PRIMARY);
			if (_input.secondaryShoot) _vehicle.Shooting(WeaponPositionType.SECONDARY);
		}
	 
	}
}
