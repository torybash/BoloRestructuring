using System;
using Bolo.DataClasses;
using Bolo.Map;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace Bolo.Net
{
	public class Host : NetworkBehaviour
	{
		[SerializeField]
		private List<NetPlayer> _players;

		private static Host s_instance;

		[ServerCallback]
		void Awake()
		{
			s_instance = this;
		}

		[ServerCallback]
		void Start()
		{
			InitGame();
		}

		private void InitGame()
		{
			//Init stuff
			_players = new List<NetPlayer>();

			//Create map
			Game.map.GenerateAndCreateMap();
		}

		public static void AddPlayer(NetPlayer netPlayer)
		{
			s_instance._players.Add(netPlayer);
		}
	}
}