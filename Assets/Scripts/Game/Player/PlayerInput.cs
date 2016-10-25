using UnityEngine;
using System.Collections;
using Bolo.Events;

namespace Bolo.Player
{
	public class PlayerInput : MonoBehaviour
	{
		//private Vector2 _moveInput = Vector2.zero;
		//private bool drilling = false;

		private PlayerInputData _input = new PlayerInputData();

		private PlayerVehicle _vehicle;
		private Camera _cam;

		

		#region Lifecycle
		void Update()
		{
			if (_vehicle != null)
			{
				_input.moveInput.x = Input.GetAxis("Horizontal"); 
				_input.moveInput.y = Input.GetAxis("Vertical");

				_input.useAction = Input.GetKey(KeyCode.Space);

				_input.mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);

				_input.shooting = Input.GetMouseButton(0);
			}
			
		}


		void FixedUpdate()
		{
			if (_vehicle != null)
			{
				//_vehicle.InputUpdate(_input);
				Movement();
				UseAction();
				MouseInput();
				//_vehicle.Movement(_input.moveInput);
				//_vehicle.Drilling(_input.useAction);
				//_vehicle.Shooting(_input.mousePosition, _input.shooting);

			}
		}

		private void Movement()
		{
			var currPos = new Pos( (int)_vehicle.bodyVec.x, (int)_vehicle.bodyVec.y);
			var moveVec = new Vector2 (_input.moveInput.x, _input.moveInput.y) * Time.fixedDeltaTime;

			var endWorldPos = _vehicle.ApplyMovement(moveVec);

			var newPos = new Pos((int)endWorldPos.x, (int)endWorldPos.y);

			if (currPos != newPos){
				//Moved to new tile
				EventManager.TriggerEvent("PlayerMovedToTile", new PlayerMovedToTileArgs(newPos));
			}
		}

		private void UseAction()
		{
			//TODO, other stuff than drilling!?
			bool drilling = _vehicle.SetDrilling(_input.useAction);
			if (drilling)
			{
				var drillArgs = new DrillEventArgs(_vehicle.drillPos, _vehicle.drillDamage);
				EventManager.TriggerEvent("DrillTileAt", drillArgs);
			}
		}

		private void MouseInput()
		{
			//TODO, other stuff than shooting!?
			_vehicle.SetDirection(_input.mousePosition);

			bool shooting = _vehicle.Shooting(_input.shooting);
			if (shooting)
			{
				var shootArgs = new ShootProjectileArgs(_vehicle.cannonDirection);
				EventManager.TriggerEvent("ShootProjectile", shootArgs);
			}
		}
		#endregion Lifecycle


		public void Init(PlayerVehicle vehicle, Camera cam)
		{
			_vehicle = vehicle;
			_cam = cam;
		}
	 
	}
}
