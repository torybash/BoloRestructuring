﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Bolo.Events;
using Bolo.Player;
using System;
using Bolo.DataClasses;
using Bolo.Net;

namespace Bolo
{
	public class PlayerVehicle : Actor
	{
		[SerializeField] private Rigidbody2D _body;
		[SerializeField] private VehicleTurret _turret;


		#region Fields
		//Vehicle state data
		public float maxSpeed = 2f; //TODO!! Make vehicle/actor data-class instead!
		public float drillDamage = 1f;
		public float drillLength = 0.5f;
		public float drillCooldown = 0.2f;



		//Variables
		private float _hp;

		private Vector2 _drillPosition = Vector2.zero;
		private float _nextDrillTime = 0;

		private Vector2 _cannonDirection = Vector2.zero;
		private float _nextShotTime = 0;

		private float _angle = 0;

		//Animation ids
		private int _drillHash = Animator.StringToHash("Drilling");
		#endregion Fields


		#region Properties
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

		private WeaponData _PrimaryWeapon { get; set; }
		private WeaponData _SecondaryWeapon { get; set; }
		#endregion Properties


		#region Client
		public override void OnStartAuthority()
		{
			base.OnStartAuthority();

			Game.Player.SetVehicle(this);

			UpdateOutfit();
		}

		public void UpdateOutfit()
		{
			//Set vehicle weapons
			var weapon = Game.Player.Inventory.GetPrimaryWeapon();
			if (weapon != WeaponType.NONE) _PrimaryWeapon = Game.WeaponsLib.GetWeaponData(weapon);

			weapon = Game.Player.Inventory.GetSecondaryWeapon();
			if (weapon != WeaponType.NONE) _SecondaryWeapon = Game.WeaponsLib.GetWeaponData(weapon);
		}

		public Vector3 ApplyMovement(Vector2 moveVec)
		{
			var newWorldPos = _body.position + maxSpeed * moveVec;
			_body.MovePosition(newWorldPos);

			var normalizedMoveVec = Vector3.Normalize(moveVec);
			_angle = Mathf.Atan2(normalizedMoveVec.y,normalizedMoveVec.x) - Mathf.PI/2f;

			if (moveVec.magnitude > 0.0001){
				//rotate

				transform.eulerAngles = new Vector3(0, 0, _angle * 180f / Mathf.PI);
			}
			return newWorldPos;
		}


		public void SetDrilling(bool drilling)
		{
			netAnim.animator.SetBool("Drilling", drilling);
			if (!drilling || Time.time < _nextDrillTime) return;
			_nextDrillTime = Time.time + drillCooldown;
			_drillPosition = transform.position;
			_drillPosition.x -= drillLength * Mathf.Sin(_angle);
			_drillPosition.y += drillLength * Mathf.Cos(_angle);

			var drillPos = new Pos((int)_drillPosition.x, (int)_drillPosition.y);

			var drillCmd = new DrillCommand
			{
				pos = new Pos((int)_drillPosition.x, (int)_drillPosition.y),
				damage = drillDamage
			};

			CmdDrillingTileAt(drillCmd, Game.Client.playerControllerId);
		}


		public void SetDirection(Vector2 mousePosition)
		{
			_cannonDirection = (mousePosition - (Vector2)transform.position).normalized;
			float cannonAngle = Mathf.Atan2(_cannonDirection.y, _cannonDirection.x);
			_turret.transform.eulerAngles = new Vector3(0, 0, cannonAngle * 180f / Mathf.PI);
		}

		public void Shooting(bool shooting)
		{
			if (!shooting || Time.time < _nextShotTime) return;
			_nextShotTime = Time.time + _PrimaryWeapon.cooldownDuration;

			//Fire a bit in front of position
			var shootPos = transform.position;
			shootPos.x -= 0.25f * Mathf.Sin(_angle); //TODO, make value part of weapon? maybe find pos from turret?
			shootPos.y += 0.25f * Mathf.Cos(_angle);

			//Create and send command
			var shootCmd = new ShootProjectileCommand
			{
				type = _PrimaryWeapon.type, //TODO make right-click to secondary shot
				pos = transform.position,
				dir = _cannonDirection
			};
			CmdShooting(shootCmd, Game.Client.connectionToServer.connectionId); //TODO, send more shot data, send weapon id!
		}
		#endregion Client


		#region Server
		[Command]
		private void CmdDrillingTileAt(DrillCommand cmd, int playerId)
		{
			//Debug.Log("CmdDrillingTileAt - drillPos: " + cmd.pos + ", drillDmg: " + cmd.damage + ", client conn: " + connectionToClient);

			Game.Map.DrillTileAt(cmd.pos, cmd.damage);
		}
		
		[Command]
		private void CmdShooting(ShootProjectileCommand cmd, int connId)
		{
			var weapon = Game.WeaponsLib.GetWeaponData(cmd.type);
			Game.Spawns.ShootProjectile(cmd.pos, cmd.dir, weapon, connId);
		}

		[Command]
		public void CmdHitByProjectile(Vector2 knockBackForce, float damage)
		{
			_hp -= damage;
			_body.AddForce(knockBackForce);

			//TODO, make property for HP, make Hurt/Die method
		}

		[Command]
		public void CmdPickUpResource(ResourceType type, int resourceCount)
		{
			Game.Player.AddResources(type, resourceCount);
		}
		#endregion Server
	}
}