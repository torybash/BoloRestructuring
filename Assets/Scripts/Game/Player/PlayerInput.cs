using UnityEngine;
using System.Collections;

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

				_input.drilling = Input.GetKey(KeyCode.Space);

				_input.mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);

				_input.shooting = Input.GetMouseButton(0);
			}
			
		}


		void FixedUpdate()
		{
			if (_vehicle != null)
			{
				_vehicle.InputUpdate(_input);
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
