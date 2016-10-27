using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Bolo.Map;
using System.Collections.Generic;
using Bolo.Events;
using Bolo.DataClasses;

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
		//public override void OnStartServer()
		//{
		//	Debug.LogError("OnStartServer - isServer: " + isServer + ", _tileMap.mapInfo: " + _tileMap.mapInfo);
		//	base.OnStartServer();

		//	if (_tileMap.mapInfo == null) return; //TODO make state or something to check

		//	SendMapToClient();
		//}
		public override void OnStartClient()
		{
			Debug.LogError("OnStartClient - isServer: " + isServer + ", _tileMap.mapInfo: " + _tileMap.mapInfo);

			base.OnStartClient();

			NetworkManager.singleton.client.RegisterHandler(MsgType.Highest + 1, OnMsgCreateMap); //TODO!!! Make msg types

			//if (!isServer) return;
			//if (_tileMap.mapInfo == null) return; //TODO make state or something to check

			//SendMapToClient();
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
			Debug.LogError("Setup - isServer: " + isServer);

			//NetworkManager.singleton.client.RegisterHandler(MsgType.Highest + 1, OnMsgCreateMap); //TODO!!! Make msg types
		}

		public void PlayerMovedToTile(GameEventArgs args)
		{
			var movedArgs = (PlayerMovedToTileArgs)args;

			_mapCollision.UpdateCollisionInArea(movedArgs.pos);
			_fog.UpdateFog(movedArgs.pos);
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
		#endregion Client


		#region Server
		//[Server]
		public void SendMapToClient(NetworkConnection clientConn) {
			Debug.LogError("SendMapToClient - isServer: " + isServer + ", _tileMap.mapInfo: " + _tileMap.mapInfo + ", clientConn: "+ clientConn);
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
				var changeParams = new ChangeBlockParameters { pos = pos, block = BlockType.EMPTY };
				RpcChangeTileAt(changeParams);

				//TODO Spawn pickups!
			}
			RpcDrillEffectAt(pos);
		}
		#endregion Server

		#region RPCs
		//[ClientRpc]
		//public void RpcCreateMap(MapGenerationParameters genParams)
		//{
		//	//Create new map
		//	_tileMap.InitMap(genParams, transform); // = mapGen.GenerateMap(genParams, transform);

		//	//Init stuff for map
		//	var mapInfo = _tileMap.mapInfo;
		//	_fog.Setup(mapInfo);
		//	_mapCollision.Setup(mapInfo);
		//}

		[ClientRpc]
		public void RpcDrillEffectAt(Pos pos)
		{
			//TODO
		}

		[ClientRpc]
		public void RpcChangeTileAt(ChangeBlockParameters changeParams)
		{
			_tileMap.ChangeTileAt(changeParams);
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