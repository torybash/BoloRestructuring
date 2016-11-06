using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;
using Bolo.DataClasses;

namespace Bolo
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Projectile : NetworkSpawnable
	{
		private SpawnPool<Projectile> _spawnPool;
		private Rigidbody2D _body;
		private SpriteRenderer _renderer;

		[SyncVar(hook = "OnSetType")]
		private WeaponType _type;

		private WeaponData weapon
		{
			get; set;
		}

		private Rigidbody2D _Body
		{
			get
			{
				if (_body == null) _body = GetComponent<Rigidbody2D>();
				return _body;
			}
		}

		private SpriteRenderer _Renderer
		{
			get
			{
				if (_renderer == null) _renderer = GetComponent<SpriteRenderer>();
				return _renderer;
			}
		}

		public void Init(Vector3 pos, Vector3 dir, WeaponData weapon, NetworkConnection clientConn)
		{
			this.weapon = weapon;
			_type = weapon.type;

			transform.position = pos;
			GetComponent<Rigidbody2D>().velocity = dir * weapon.speed;

			GetComponent<Collider2D>().enabled = false;
			StartCoroutine(EnableCollision()); //TODO, Maybe have different time for weapons? layer masks?
		}


		public override void SetPool<T>(SpawnPool<T> pool) 
		{
			_spawnPool = pool as SpawnPool<Projectile>;
		}


		private void OnSetType(WeaponType type)
		{

			_Renderer.sprite = WeaponLibrary.GetWeaponData(type).projectileSprite;
		}

		//Trigger are handled only on the server, as it have authority
		[ServerCallback]
		private void OnTriggerEnter2D(Collider2D other)
		{
			//Use for explosion effect?
			//Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, layerMask);
			
			//Player collision
			if (other.CompareTag("Player"))
			{
				var vehicle = other.GetComponent<PlayerVehicle>();
				var knockBackForce = _Body.velocity.normalized * weapon.knockBack;
				vehicle.CmdHitByProjectile(knockBackForce, weapon.damage);

			}

			//Destroy projectile
			_spawnPool.Destroy(this);
		}


		private IEnumerator EnableCollision()
		{
			yield return new WaitForSeconds(0.25f);
			GetComponent<Collider2D>().enabled = true;
		}
	}
}