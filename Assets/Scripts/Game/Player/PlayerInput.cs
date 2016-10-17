using UnityEngine;
using System.Collections;

namespace Bolo.Network
{
	public class PlayerInput : MonoBehaviour
	{
		private Vector2 _moveInput = Vector2.zero;
		private bool drilling = false;

		public PlayerVehicle vehicle;

		void Update()
		{
			_moveInput.x = Input.GetAxis("Horizontal"); 
			_moveInput.y = Input.GetAxis("Vertical");

			drilling = Input.GetKey(KeyCode.Space);
		}


		void FixedUpdate()
		{
			if (vehicle != null)
			{
				vehicle.InputUpdate(_moveInput, drilling);
			}
		}
	 
	}
}
