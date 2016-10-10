using UnityEngine;
using System.Collections;
using Bolo.DataClasses;

namespace Bolo.UI
{
	public class GameUIController : ControllerBehaviour<GameUIController>
	{

		[SerializeField]
		private UIPanel _spawnPanel;

		#region Lifetime
		void Awake()
		{
			Game.events.StartListening("RequestPlayerVehicle", OnRequestPlayerVehicle);
		}

		void OnDestroy()
		{
			Game.events.StopListening("RequestPlayerVehicle", OnRequestPlayerVehicle);
		}
		#endregion

		#region Setup
		public void Init()
		{
			_spawnPanel.Enable(true);
		}
		#endregion

		#region Event Handlers
		private void OnRequestPlayerVehicle()
		{
			_spawnPanel.Enable(false);
		}
		#endregion
	}
}