using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Bolo.Events;
using Bolo.Player;
using System;

namespace Bolo
{
	public class PlayerVehicle : Actor
	{
		[SerializeField] private Rigidbody2D _body;
		[SerializeField] private VehicleTurret _turret;

		//Defintion values
		public float maxSpeed = 2f; //TODO!! Make vehicle/actor data-class instead!
		public float drillDamage = 1f;
		public float drillLength = 0.5f;
		public float drillCooldown = 0.2f;

		//Variables
		private int _hp;

		private Vector2 _drillPosition = Vector2.zero;
		private float _nextDrillTime = 0;

		private Vector2 _cannonDirection = Vector2.zero;
		private float _nextShotTime = 0;

		private float _angle = 0;

		//Animation ids
		private int _drillHash = Animator.StringToHash("Drilling");
		
		public Vector2 bodyVec {
			get
			{
				return _body.position;
			}
		}

		public Pos drillPos
		{
			get
			{
				return new Pos((int)_drillPosition.x, (int)_drillPosition.y);
			}
		}

		public Vector2 cannonDirection
		{
			get
			{
				return _cannonDirection;
			}
		}

		#region Client
		public override void OnStartClient()
		{
			//Debug.Log("OnStartClient - isServer: " + isServer);
			base.OnStartClient();
			//if (isServer) return; //TODO?

			Game.player.SetVehicle(this);
		}


		public Vector3 ApplyMovement(Vector2 moveVec)
		{
			var newWorldPos = _body.position + maxSpeed * moveVec;
			_body.MovePosition(newWorldPos);

			if (moveVec.magnitude > 0.0001){
				//rotate
				var normalizedMoveVec = Vector3.Normalize(moveVec);
				_angle = Mathf.Atan2(normalizedMoveVec.y,normalizedMoveVec.x) - Mathf.PI/2f;
				transform.eulerAngles = new Vector3(0, 0, _angle * 180f / Mathf.PI);
			}
			return newWorldPos;
		}


		public bool SetDrilling(bool drilling)
		{
			netAnim.animator.SetBool("Drilling", drilling);
			if (!drilling || Time.time < _nextDrillTime) return false;
			_nextDrillTime = Time.time + drillCooldown;
			_drillPosition = transform.position;
			_drillPosition.x -= drillLength * Mathf.Sin(_angle);
			_drillPosition.y += drillLength * Mathf.Cos(_angle);
			return true;
		}


		public void SetDirection(Vector2 mousePosition)
		{
			_cannonDirection = mousePosition - (Vector2)transform.position;
			float cannonAngle = Mathf.Atan2(_cannonDirection.y, _cannonDirection.x);
			_turret.transform.eulerAngles = new Vector3(0, 0, cannonAngle * 180f / Mathf.PI);
		}

		public bool Shooting(bool shooting)
		{
			if (!shooting || Time.time < _nextShotTime) return false;
			_nextShotTime = Time.time + 0.2f; //TODO!!
			return true;
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