using UnityEngine;
using System.Collections;
using Bolo.DataClasses;

namespace Bolo.Map
{
	public class MapHelper
	{
		public static Pos WorldToPos(Vector2 vec)
		{
			return new Pos((int)vec.x, (int)vec.y);
		}

		public static Vector2 PosToWorld(Pos pos)
		{
			return new Vector2(pos.x + 0.5f, pos.y + 0.5f);
		}

		public static Vector3 PosToWorld(Pos pos, float zPos)
		{
			return new Vector3(pos.x + 0.5f, pos.y + 0.5f, zPos);
		}
	}
}
