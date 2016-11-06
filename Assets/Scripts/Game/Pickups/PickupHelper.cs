using Bolo.DataClasses;
using UnityEngine;

namespace Bolo.Pickups
{
	public class PickupHelper
	{
		//TODO use block type! , Create library to edit values
		public static ResourceData[] GeneratePickupData(BlockType block)
		{
			var resources = new ResourceData[Random.Range(2, 5)];
			for (int i = 0; i < resources.Length; i++)
			{
				resources[i] = new ResourceData {
					resourceCount = Random.Range(4, 10),
					type = ResourceType.METAL
				};
			}
			return resources;
		}
	}
}
