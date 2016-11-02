using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

namespace Bolo
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Projectile : NetworkSpawnable
	{
		public void Init(Vector3 pos, Vector3 dir, WeaponData weapon, NetworkConnection clientConn)
		{

			transform.position = pos;
			GetComponent<Rigidbody2D>().velocity = dir * weapon.speed;
		}
	}
}