using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bolo.DataClasses;

namespace Bolo.Player
{
	public class PlayerInventory
	{
		private Dictionary<ResourceType, int> _resouces;


		public PlayerInventory()
		{
			var enumVals = Enum.GetValues(typeof(ResourceType));
			_resouces = new Dictionary<ResourceType, int>();

			for (int i = 1; i < enumVals.Length; i++)
			{
				_resouces.Add((ResourceType)enumVals.GetValue(i), 0);
			}
		}


		public void AddResources(ResourceType type, int resourceCount)
		{
			_resouces[type] += resourceCount;

			D.Log(ObjectDumper.Dump(_resouces));
		}
	}
}
