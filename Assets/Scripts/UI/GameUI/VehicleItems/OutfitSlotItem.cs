using UnityEngine;
using Bolo.Events;
using Bolo.DataClasses;

namespace Bolo.Player
{
	public class OutfitSlotItem : MonoBehaviour
	{
		public OutfitData.OutfitPlacement Placement { get; private set; }
		public int PositionIdx { get; private set; }

		public void Init(OutfitData.OutfitPlacement placement, int positionIdx = -1) 
		{
			Placement = placement;
			PositionIdx = positionIdx;
		}
	}
}
