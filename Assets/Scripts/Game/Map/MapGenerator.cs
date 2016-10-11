using UnityEngine;
using System.Collections;

namespace Bolo.Map
{
	public class MapGenerator : MonoBehaviour
	{
		[SerializeField]
		private Map _map;


		public Map GenerateMap(MapInfo mapInfo)
		{

			if (_map) Destroy(_map.gameObject);
			_map = new GameObject("MapContainer").AddComponent<Map>();
			_map.transform.SetParent(transform, false);
			_map.GenerateChunks(mapInfo);

			return _map;
		}

	}
}
