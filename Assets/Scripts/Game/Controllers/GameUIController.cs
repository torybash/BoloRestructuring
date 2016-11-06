using UnityEngine;
using System.Collections;
using Bolo.DataClasses;
using System;
using Bolo.Events;
using Bolo.Player;

namespace Bolo
{
	public class GameUIController : ControllerBehaviour
	{

		[SerializeField]
		private UIPanel _spawnPanel;

		[SerializeField]
		private UIPanel _vehiclePanel;

		#region Setup
		protected override void Listen()
		{
			EventManager.AddListener("RequestPlayerVehicle", OnRequestPlayerVehicle);
		}

		protected override void UnListen()
		{
			EventManager.RemoveListener("RequestPlayerVehicle", OnRequestPlayerVehicle);
		}


		public void Init()
		{
			_spawnPanel.Enable(true); //TODO do from Editor?
			_vehiclePanel.Enable(false);
		}
		#endregion


		#region Lifecycle
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				_vehiclePanel.Enable(!_vehiclePanel.IsEnabled);
			}
		}
		#endregion Lifecycle

		#region Event Handlers
		private void OnRequestPlayerVehicle(GameEventArgs arg0)
		{
			_spawnPanel.Enable(false);
		}
		#endregion
	}
}