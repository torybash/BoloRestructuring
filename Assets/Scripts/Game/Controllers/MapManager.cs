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


		[SerializeField]
		private int _defaultMapSize;

		#region Lifecycle
		public override void OnStartClient()
		{
			base.OnStartClient();

			NetworkManager.singleton.client.RegisterHandler(MsgTypes.CreateMap, OnMsgCreateMap);
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
		public void PlayerMovedToTile(GameEventArgs args)
		{
			var movedArgs = (PlayerMovedToTileArgs)args;
			UpdateMap(movedArgs.pos);
		}


		public void CreateMap(MapGenerationParameters genParams)
		{
			//Create new map
			_tileMap.InitMap(genParams, transform);

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
		[Server]
		public void SendMapToClient(NetworkConnection clientConn) {
			var msg = new MapGenerationParametersMessage { seed = _tileMap.genParams.seed, size = _tileMap.genParams.size };
			NetworkServer.SendToClient(clientConn.connectionId, MsgTypes.CreateMap, msg);
		}

		[Server]
		public void GenerateAndCreateMap()
		{
			var genParams = new MapGenerationParameters { seed = Random.Range(0, int.MaxValue), size = _defaultMapSize };																								
			CreateMap(genParams);
		}

		[Server]
		public Vector2 GetNewSpawnPosition(List<Vector2> playerPositions)
		{
			return _tileMap.GetNewSpawnPosition(playerPositions);
		}

		[Server]
		public void DrillTileAt(Pos pos, float damage)
		{
			var result = _tileMap.DrillTileAt(pos, damage);	
			if (result.removeTile)
			{
				var cmd = new ChangeBlockCommand { pos = pos, block = BlockType.EMPTY };
				RpcChangeTileAt(cmd);

				var worldPos = MapHelper.PosToWorld(pos);
				Game.Spawns.SpawnResources(worldPos, result.pickups);
			}
			RpcDrillEffectAt(pos);
		}
		#endregion Server


		#region RPCs
		[ClientRpc]
		public void RpcDrillEffectAt(Pos pos)
		{
			//TODO drill effect
		}

		[ClientRpc]
		public void RpcChangeTileAt(ChangeBlockCommand cmd)
		{
			_tileMap.ChangeTileAt(cmd);
			var playerVehicle = Game.Player.Vehicle;
			var pos = MapHelper.WorldToPos(playerVehicle.transform.position);
			_fog.UpdateFog(pos);
			_mapCollision.UpdateCollisionInArea(pos);
		}
		#endregion RPCs
	}


	public class MapGenerationParametersMessage : MessageBase
    {
		public int seed;
		public int size;	
    }
}