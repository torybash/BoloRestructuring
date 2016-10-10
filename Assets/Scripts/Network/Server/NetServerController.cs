using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Bolo.Net
{
	public class NetServerController : NetworkBehaviour {

		[ServerCallback]
		void Start()
		{	
			int mapSeed = Game.map.CreateMapSeed();
			Game.player.map.ShareMap(mapSeed);
		}
	}
}