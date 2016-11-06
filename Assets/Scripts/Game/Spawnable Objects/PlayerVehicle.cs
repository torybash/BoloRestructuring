using UnityEngine;
using UnityEngine.Networking;
using Bolo.DataClasses;
using Bolo.Net;
using Bolo.Map;

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

		private float _nextDrillTime = 0;

		private Vector2 _cannonDirection = Vector2.zero;
		private float[] _nextShotTimes = new float[2];

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
			var weapon = Game.Player.Inventory.GetPrimaryWeapon();
			if (weapon != WeaponType.NONE) _PrimaryWeapon = WeaponLibrary.GetWeaponData(weapon);
			else _PrimaryWeapon = null;

			weapon = Game.Player.Inventory.GetSecondaryWeapon();
			if (weapon != WeaponType.NONE) _SecondaryWeapon = WeaponLibrary.GetWeaponData(weapon);
			else _SecondaryWeapon = null;
		}

		public Vector3 ApplyMovement(Vector2 moveVec)
		{
			var newWorldPos = _body.position + maxSpeed * moveVec;
			_body.MovePosition(newWorldPos);

			var normalizedMoveVec = Vector3.Normalize(moveVec);
			_angle = Mathf.Atan2(normalizedMoveVec.y,normalizedMoveVec.x) - Mathf.PI/2f;

			if (moveVec.magnitude > 0.0001){
				transform.eulerAngles = new Vector3(0, 0, _angle * 180f / Mathf.PI);
			}
			return newWorldPos;
		}


		public void SetDrilling(bool drilling)
		{
			netAnim.animator.SetBool("Drilling", drilling);
			if (!drilling || Time.time < _nextDrillTime) return;
			_nextDrillTime = Time.time + drillCooldown;
			var drillPosition = (Vector2) transform.position + Util.AngleToForward(_angle) * drillLength;

			var drillCmd = new DrillCommand
			{
				pos = MapHelper.WorldToPos(drillPosition),
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


		public void Shooting(WeaponPositionType weaponPos)
		{
			var weapon = weaponPos == WeaponPositionType.PRIMARY ? _PrimaryWeapon : _SecondaryWeapon;
			if (weapon == null || Time.time < _nextShotTimes[(int)weaponPos]) return;			
			_nextShotTimes[(int)weaponPos] = Time.time + weapon.cooldownDuration;

			//Fire a bit in front of position
			var shootPos = (Vector2)transform.position + Util.AngleToForward(_angle) * 0.25f; //TODO, make value part of weapon? maybe find pos from turret?

			//Create and send command
			var shootCmd = new ShootProjectileCommand
			{
				type = weapon.type, 
				pos = transform.position,
				dir = _cannonDirection
			};
			CmdShooting(shootCmd, Game.Client.connectionToServer.connectionId);
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
			var weapon = WeaponLibrary.GetWeaponData(cmd.type);
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