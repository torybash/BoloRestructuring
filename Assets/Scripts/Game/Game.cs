﻿using UnityEngine;
using System.Collections;
using Bolo.Util;
using Bolo.Net;
using Bolo.DataClasses;
using System;

namespace Bolo
{
	public class Game : SingletonBehaviour<Game>
	{

		//Fields
		[Header("Net player")]
		[SerializeField] private NetPlayer _client;

		[Header("Controllers")]
		[SerializeField] private CameraController _cam;

		[SerializeField] private GameUIController _ui;
		[SerializeField] private PlayerController _player;

		[Header("Commanders")]
		[SerializeField] private MapManager _map;
		[SerializeField] private SpawnsManager _spawns;
		//[SerializeField] private MapCommander _map;
		//[SerializeField] private SpawnsCommander _spawns;

		[Header("Asset References")]
		[SerializeField] private PrefabLibrary _prefabsLib;


		//Properties
		public static CameraController cam { get { return I._cam; } }
		public static GameUIController ui { get { return I._ui; } }
		public static PlayerController player { get { return I._player; } }

		public static MapManager map { get { return I._map; } }
		public static SpawnsManager spawns { get { return I._spawns; } }

		public static NetPlayer client { get { return I._client; } }

		public static PrefabLibrary prefabsLib { get { return I._prefabsLib; } }


		void Awake()
		{
			//Init prefabs-library
			prefabsLib.Init();
		}

		public static void SetLocalPlayer(NetPlayer netPlayer)
		{
			//Set ref to network player
			I._client = netPlayer;

			//Init controllers
			cam.Init();
			ui.Init();
			player.Init();
		}
	}
}