using UnityEngine;
using Bolo.DataClasses;

namespace Bolo.Map
{
	public class MapController : ControllerBehaviour<MapController>
	{
		[SerializeField]
		private MapGenerator _mapGen;

		

		public int CreateMapSeed()
		{
			return Random.Range(0, int.MaxValue);
		}

		public void CreateMapFromSeed(int seed)
		{
			_mapGen.GenerateMap(seed);
		}
	}
}