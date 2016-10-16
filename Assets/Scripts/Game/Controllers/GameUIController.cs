using UnityEngine;
using System.Collections;
using Bolo.DataClasses;
using Bolo.Util;
using Bolo.UI;
using System;
using Bolo.Events;

namespace Bolo
{
	public class GameUIController : ControllerBehaviour
	{

		[SerializeField]
		private UIPanel _spawnPanel;


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
			_spawnPanel.Enable(true);
		}
		#endregion

		#region Event Handlers
		private void OnRequestPlayerVehicle(GameEventArgs arg0)
		{
			_spawnPanel.Enable(false);
		}
		#endregion
	}
}