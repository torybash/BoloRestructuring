using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

namespace Bolo.Events
{
	public static class EventManager
	{
		private static Dictionary<string, UnityGameEvent> _eventDictionary = new Dictionary<string, UnityGameEvent>();


		public static void AddListener(string eventName, UnityAction<GameEventArgs> listener)
		{
			UnityGameEvent thisEvent = null;
			if (_eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent.AddListener(listener);
			}
			else
			{
				thisEvent = new UnityGameEvent();
				thisEvent.AddListener(listener);
				_eventDictionary.Add(eventName, thisEvent);
			}
		}

		public static void RemoveListener(string eventName, UnityAction<GameEventArgs> listener)
		{
			UnityGameEvent thisEvent = null;
			if (_eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent.RemoveListener(listener);
			}
		}

		public static void TriggerEvent(string eventName, GameEventArgs args)
		{
			UnityGameEvent thisEvent = null;
			if (_eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent.Invoke(args);
			}
		}
	}
}