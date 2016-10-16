using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
}
