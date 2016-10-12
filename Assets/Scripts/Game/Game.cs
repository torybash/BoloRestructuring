using UnityEngine;
using System.Collections;
using Bolo.Util;
using Bolo.Net;
using Bolo.UI;
using Bolo.Camera;
using Bolo.DataClasses;
using Bolo.Map;

namespace Bolo
{
	public class Game : SingletonBehaviour<Game>
	{

		//Fields
		[Header("Controllers")]
		[SerializeField]
		private CameraController _cam;
		[SerializeField]
		private GameUIController _ui;
		[SerializeField]
		private MapController _map;

		[Header("Asset References")]
		[SerializeField]
		private PrefabLibrary _prefabsLib;

		private NetServerController _server;
		private NetPlayerController _player;
		private PlayerVehicle _vehicle;

		private EventManager _events = new EventManager();


		//Properties
		public static CameraController cam { get { return I._cam; } }
		public static GameUIController ui { get { return I._ui; } }
		public static MapController map { get { return I._map; } }

		public static PrefabLibrary prefabsLib { get { return I._prefabsLib; } }

		public static NetServerController server { get { return I._server; } private set { I._server = value; } }
		public static NetPlayerController player { get { return I._player; } private set { I._player = value; } }
		public static PlayerVehicle vehicle { get { return I._vehicle; } private set { I._vehicle = value; } }

		public static EventManager events { get { return I._events; } } 


		public static void Init(NetPlayerController newPlayer)
		{
			//Set ref to network player
			player = newPlayer;

			//Init stuff
			ui.Init();
			prefabsLib.Init();
		}

		public static void InitServer(NetServerController newServer)
		{
			server = newServer;
		}

		public static void SetVehicle(PlayerVehicle newVehicle)
		{
			vehicle = newVehicle;

			//TODO set camera properly
			var newCamPos = vehicle.transform.position;
			newCamPos.z = cam.transform.position.z;
			cam.transform.position = newCamPos ; 
		}

	}
}