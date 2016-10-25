using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bolo.Events
{
	public class DrillEventArgs : GameEventArgs
	{
		public Pos pos { get; private set; }
        public float damage { get; private set; }

        public DrillEventArgs(Pos pos, float damage)
        {
            this.pos = pos;
            this.damage = damage;
		}
	}

	public class PlayerMovedToTileArgs : GameEventArgs
	{
		public Pos pos { get; private set; }

        public PlayerMovedToTileArgs(Pos pos)
        {
            this.pos = pos;
		}
	}

	public class ShootProjectileArgs : GameEventArgs 
	{
		public Vector3 dir { get; private set; }
		//public WeaponData, TODO maybe extract bullet data from weapon data?

		public ShootProjectileArgs(Vector3 dir)
        {
            this.dir = dir;
		}
	}
}
