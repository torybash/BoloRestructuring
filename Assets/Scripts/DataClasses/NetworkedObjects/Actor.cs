using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Bolo
{
	public abstract class Actor : NetworkSpawnable
	{
		[SerializeField]
		private NetworkAnimator _netAnim;
		
		public NetworkAnimator netAnim
		{
			get
			{
				if (_netAnim == null) _netAnim = GetComponent<NetworkAnimator>();
				return _netAnim;
			}
		}


	}
}