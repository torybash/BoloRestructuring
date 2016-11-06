using UnityEngine;
using System.Collections;
using Bolo.Net;
using Bolo.DataClasses;
using System;

namespace Bolo
{
	public class Game : SingletonBehaviour<Game>
	{
		[Header("Net player")]
		[SerializeField] private NetPlayer _client;

		[Header("Controllers")]
		[SerializeField] private CameraController _cam;

		[SerializeField] private GameUIController _ui;
		[SerializeField] private PlayerController _player;

		[Header("Commanders")]
		[SerializeField] private MapManager _map;
		[SerializeField] private SpawnsManager _spawns;


		//Properties
		public static CameraController Cam { get { return I._cam; } }
		public static GameUIController Ui { get { return I._ui; } }
		public static PlayerController Player { get { return I._player; } }

		public static MapManager Map { get { return I._map; } }
		public static SpawnsManager Spawns { get { return I._spawns; } }

		public static NetPlayer Client { get { return I._client; } }


		public static void SetLocalPlayer(NetPlayer netPlayer)
		{
			//Set ref to network player
			I._client = netPlayer;

			//Init controllers
			Cam.Init();
			Ui.Init();
			Player.Init();
		}
	}
}