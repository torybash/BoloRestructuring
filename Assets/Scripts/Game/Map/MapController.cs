using UnityEngine;
using Bolo.DataClasses;

namespace Bolo.Map
{
	public class MapController : ControllerBehaviour<MapController>
	{
		[SerializeField]
		private MapGenerator _mapGen;

		[SerializeField]
		private MapInfoHelper _infoHelper;
	

		public int CreateMapSeed()
		{
			return Random.Range(0, int.MaxValue);
		}

		public void CreateMapFromSeed(int seed)
		{
			var _mapInfo = _infoHelper.GetRandomMapInfo();
			var map = _mapGen.GenerateMap(_mapInfo);
		}
	}
}