using UnityEngine;
using System.Collections;
using System;

namespace Bolo
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Projectile : NetworkSpawnable
	{
		public void Init(Vector3 pos, Vector3 vel)
		{
			transform.position = pos;
			GetComponent<Rigidbody2D>().velocity = vel;
		}
	}
}