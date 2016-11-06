using System;
using System.Collections.Generic;
using UnityEngine;


public static class Util
{
	public static Vector2 AngleToForward(float angle)
	{
		var pos = new Vector2();
		pos.x -= Mathf.Sin(angle);
		pos.y += Mathf.Cos(angle);
		return pos;
	}
}