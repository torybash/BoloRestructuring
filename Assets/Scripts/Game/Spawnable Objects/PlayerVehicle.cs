using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Bolo.Events;

namespace Bolo
{
	public class PlayerVehicle : Actor
	{
		[SerializeField] private Rigidbody2D _body;

		//Defintion values
		public float maxSpeed = 2f; //TODO!! Make vehicle/actor data-class instead!
		public float drillDamage = 1f;
		public float drillLength = 0.5f;
		public float drillCooldown = 0.2f;

		//Variables
		private int _hp;

		private Vector2 _drillPosition = Vector2.zero;
		private float _drillTimer = 0;
		private bool _isDrilling = false;

		private float _angle = 0;


		

		#region Client
		public override void OnStartClient()
		{
			//Debug.Log("OnStartClient - isServer: " + isServer);
			base.OnStartClient();
			//if (isServer) return;

			Game.player.SetVehicle(this);
		}


		public void InputUpdate(Vector2 move, bool drilling)
		{
			Movement(move);
			Drilling(drilling);
		}


		private void Movement(Vector2 move){
			var currPos = new Pos( (int)_body.position.x, (int)_body.position.y);

			var movementVector = new Vector2 (move.x, move.y) * Time.fixedDeltaTime;
			var newWorldPos = _body.position + maxSpeed * movementVector;
			_body.MovePosition(newWorldPos);

			var newPos = new Pos((int)newWorldPos.x, (int)newWorldPos.y);

			if (currPos != newPos){
				//Moved to new tile
				//tileMap.PlayerMovedToTileAt(xnewtile, ynewtile, inputAccess.playerInfo.getOwner());
				EventManager.TriggerEvent("PlayerMovedToTile", new PlayerMovedToTileArgs(newPos));
				//TODO! send event: PlayerMovedToTile
			}

			if (movementVector.magnitude > 0.0001){
				//rotate
				Vector3 normalizedMovement = Vector3.Normalize(movementVector);
				_angle = Mathf.Atan2(normalizedMovement.y,normalizedMovement.x) - Mathf.PI/2f;
				transform.eulerAngles = new Vector3(0, 0, _angle * 180f / Mathf.PI);
			}
		}

		private void Drilling(bool drilling)
		{
			//TODO animate.

			if (_drillTimer >= 0){
				_drillTimer -= Time.fixedDeltaTime;
			}else{
				//if (_isDrilling) GetComponent<NetworkView>().RPC("RPCSetAnimation", RPCMode.AllBuffered, false);
				_isDrilling = false;
			}

			if (drilling && _drillTimer < 0)
			{
				_drillPosition = transform.position;
				_drillPosition.x -= drillLength * Mathf.Sin(_angle);
				_drillPosition.y += drillLength * Mathf.Cos(_angle);

				var drillPos = new Pos((int)_drillPosition.x, (int)_drillPosition.y);

				_isDrilling = true;
				_drillTimer = drillCooldown;

				EventManager.TriggerEvent("DrillTileAt", new DrillEventArgs(drillPos, drillDamage));
				//Game.map.DrillTileAt(xtileInFront, ytileInFront, _drillDamage);
			}
		}

		#endregion Client


		//#region Server

		//[Command]
		//private void CmdDrillingTileAt(int xtileInFront, int ytileInFront, float drillDmg)
		//{
		//	Debug.Log("CmdDrillingTileAt - xtileInFront: " + xtileInFront + ", ytileInFront: " + ytileInFront + ", drillDmg: " + drillDmg + ", client conn: "+ connectionToClient);
		//	//connectionToClient
		//	Game.map.DrillTileAt(xtileInFront, ytileInFront, drillDmg);
		//}

		//#endregion Server
	}
}