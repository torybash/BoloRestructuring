using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bolo.Events
{
	//TODO Remove and only use UnityGameEvent
	public class GameEvent
	{


		public UnityEvent<GameEventArgs> uEvent;

		public GameEvent()
		{
			uEvent = new UnityGameEvent();
		}
	}

	
	public class UnityGameEvent : UnityEvent<GameEventArgs>
	{

	}
}
