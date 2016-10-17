using UnityEngine;
using System.Collections;
using Bolo.Util;
using Bolo.Net;
using Bolo.DataClasses;

namespace Bolo
{
	public class Game : SingletonBehaviour<Game>
	{

		//Fields
		[Header("Net player")]
		[SerializeField] private NetPlayer _localPlayer;

		[Header("Controllers")]
		[SerializeField] private CameraController _cam;
		[SerializeField] private GameUIController _ui;
		[SerializeField] private PlayerController _player;

		[Header("Commanders")]
		[SerializeField] private MapCommander _map;
		[SerializeField] private SpawnsCommander _spawns;

		[Header("Asset References")]
		[SerializeField] private PrefabLibrary _prefabsLib;


		//Properties
		public static CameraController cam { get { return I._cam; } }
		public static GameUIController ui { get { return I._ui; } }
		public static PlayerController player { get { return I._player; } }

		public static MapCommander map { get { return I._map; } }
		public static SpawnsCommander spawns { get { return I._spawns; } }

		public static NetPlayer localPlayer { get { return I._localPlayer; } }

		public static PrefabLibrary prefabsLib { get { return I._prefabsLib; } }

		void Awake()
		{
			//Init stuff
			prefabsLib.Init();
			ui.Init();
		}

		public static void SetLocalPlayer(NetPlayer netPlayer)
		{
			//Set ref to network player
			I._localPlayer = netPlayer;
		}

	}
}