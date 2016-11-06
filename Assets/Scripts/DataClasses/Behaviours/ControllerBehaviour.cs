using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Bolo.DataClasses
{
	public abstract class ControllerBehaviour : MonoBehaviour
	{

		#region Lifetime
		void OnEnable()
		{
			if (!Application.isPlaying) return;

			Listen();
		}

		void OnDisable()
		{
			if (!Application.isPlaying) return;

			UnListen();
		}
		#endregion

		protected virtual void Listen(){}
		protected virtual void UnListen(){}


	}
}