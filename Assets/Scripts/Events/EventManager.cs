using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

namespace Bolo.Events
{
	public static class EventManager
	{
		//private static Dictionary<string, UnityEvent<GameEventArgs>> _eventDictionary = new Dictionary<string, UnityEvent<GameEventArgs>>();
		private static Dictionary<string, GameEvent> _eventDictionary = new Dictionary<string, GameEvent>();

		//public static void AddListener<T>(string eventName, UnityAction<T> listener) where T : GameEventArgs
		//{
		//	GameEvent thisEvent = null;
		//	if (_eventDictionary.TryGetValue(eventName, out thisEvent))
		//	{
		//		thisEvent.uEvent.AddListener(listener as UnityAction<GameEventArgs>);
		//	}
		//	else
		//	{
		//		thisEvent = new GameEvent();
		//		thisEvent.uEvent.AddListener(listener as UnityAction<GameEventArgs>);
		//		_eventDictionary.Add(eventName, thisEvent);
		//	}
		//}

		//public static void RemoveListener<T>(string eventName, UnityAction<T> listener) where T : GameEventArgs
		//{
		//	GameEvent thisEvent = null;
		//	if (_eventDictionary.TryGetValue(eventName, out thisEvent))
		//	{
		//		thisEvent.uEvent.RemoveListener(listener as UnityAction<GameEventArgs>);
		//	}
		//}

		public static void AddListener(string eventName, UnityAction<GameEventArgs> listener)
		{
			GameEvent thisEvent = null;
			if (_eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent.uEvent.AddListener(listener);
			}
			else
			{
				thisEvent = new GameEvent();
				thisEvent.uEvent.AddListener(listener);
				_eventDictionary.Add(eventName, thisEvent);
			}
		}

		public static void RemoveListener(string eventName, UnityAction<GameEventArgs> listener)
		{
			GameEvent thisEvent = null;
			if (_eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent.uEvent.RemoveListener(listener);
			}
		}

		public static void TriggerEvent(string eventName, GameEventArgs args)
		{
			GameEvent thisEvent = null;
			if (_eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent.uEvent.Invoke(args);
			}
		}
	}
}