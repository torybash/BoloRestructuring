using System;
using Bolo.DataClasses;
using Bolo.Map;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using Bolo.Net;

namespace Bolo
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
			Game.Map.GenerateAndCreateMap();
		}

		public static void AddPlayer(NetPlayer netPlayer)
		{
			s_instance._players.Add(netPlayer);
		}

		public static NetworkConnection GetConnectionFromId(int connId)
		{
			NetworkConnection clientConn = null;
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				if (NetworkServer.connections[i] != null && NetworkServer.connections[i].connectionId == connId)
				{
					clientConn = NetworkServer.connections[i];
					break;
				}
			}
			return clientConn;
		}
	}
}