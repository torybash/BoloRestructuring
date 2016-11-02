using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Bolo.Map;
using System.Collections.Generic;
using Bolo.Events;
using Bolo.DataClasses;
using Bolo.Net;
using System;
using Random = UnityEngine.Random;

namespace Bolo
{
	public class MapManager : NetworkBehaviour
	{

		[SerializeField]
		private MapTiles _tileMap;

		[SerializeField]
		private FogOfWar _fog;

		[SerializeField]
		private MapCollision _mapCollision;


		#region Lifecycle
		public override void OnStartClient()
		{
			base.OnStartClient();

			NetworkManager.singleton.client.RegisterHandler(MsgType.Highest + 1, OnMsgCreateMap); //TODO!!! Make msg types
		}
		#endregion Lifecycle


		#region Client Messages
		private void OnMsgCreateMap(NetworkMessage netMsg)
		{
			var genParamsMsg = netMsg.ReadMessage<MapGenerationParametersMessage>();
			var genParams = new MapGenerationParameters { seed = genParamsMsg.seed, size = genParamsMsg.size };
			CreateMap(genParams);
		}
		#endregion Client Messages


		#region Client
		public void Setup()
		{
			//Debug.LogError("Setup - isServer: " + isServer);

			//NetworkManager.singleton.client.RegisterHandler(MsgType.Highest + 1, OnMsgCreateMap); //TODO!!! Make msg types
		}

		public void PlayerMovedToTile(GameEventArgs args)
		{
			var movedArgs = (PlayerMovedToTileArgs)args;
			UpdateMap(movedArgs.pos);
		}


		public void CreateMap(MapGenerationParameters genParams)
		{
			//Create new map
			_tileMap.InitMap(genParams, transform); // = mapGen.GenerateMap(genParams, transform);

			//Init stuff for map
			var mapInfo = _tileMap.mapInfo;
			_fog.Setup(mapInfo);
			_mapCollision.Setup(mapInfo);
		}

		public void UpdateMap(Pos pos)
		{
			_mapCollision.UpdateCollisionInArea(pos);
			_fog.UpdateFog(pos);
		}
		#endregion Client


		#region Server
		//[Server]
		public void SendMapToClient(NetworkConnection clientConn) {
			//Debug.LogError("SendMapToClient - isServer: " + isServer + ", _tileMap.mapInfo: " + _tileMap.mapInfo + ", clientConn: "+ clientConn);
			if (_tileMap.mapInfo == null) return; //TODO? Make sure not null beforehand

			var msg = new MapGenerationParametersMessage { seed = _tileMap.genParams.seed, size = _tileMap.genParams.size };
			NetworkServer.SendToClient(clientConn.connectionId, MsgType.Highest + 1, msg);
		}

		//[Server]
		public void GenerateAndCreateMap()
		{
			var genParams = new MapGenerationParameters { seed = Random.Range(0, int.MaxValue), size = 256 }; //TODO make size customizable!																									  //Game.map.RpcCreateMap(mapParams);
			CreateMap(genParams);
		}

		//[Server]
		public Vector2 GetNewSpawnPosition(List<Vector2> playerPositions)
		{
			return _tileMap.GetNewSpawnPosition(playerPositions);
		}

		//[Server]
		public void DrillTileAt(Pos pos, float damage)
		{
			var result = _tileMap.DrillTileAt(pos, damage);	
			if (result.removeTile)
			{
				var cmd = new ChangeBlockCommand { pos = pos, block = BlockType.EMPTY };
				RpcChangeTileAt(cmd);

				//TODO Spawn pickups!
			}
			RpcDrillEffectAt(pos);
		}
		#endregion Server


		#region RPCs
		[ClientRpc]
		public void RpcDrillEffectAt(Pos pos)
		{
			//TODO
		}

		[ClientRpc]
		public void RpcChangeTileAt(ChangeBlockCommand cmd)
		{
			_tileMap.ChangeTileAt(cmd);
			var playerPos = new Pos((int)Game.player.vehicle.transform.position.x, (int)Game.player.vehicle.transform.position.y); //TODO! convert pos, nullchecks!!!
			_fog.UpdateFog(playerPos);
			_mapCollision.UpdateCollisionInArea(playerPos);
		}
		#endregion RPCs
	}


	public class MapGenerationParametersMessage : MessageBase
    {
		public int seed;
		public int size;	
    }
}