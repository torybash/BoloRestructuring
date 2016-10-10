using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Bolo.Util
{
	public class EventManager
	{
		private Dictionary<string, UnityEvent> _eventDictionary = new Dictionary<string, UnityEvent>();

		public void StartListening(string eventName, UnityAction listener)
		{
			UnityEvent thisEvent = null;
			if (_eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent.AddListener(listener);
			}
			else
			{
				thisEvent = new UnityEvent();
				thisEvent.AddListener(listener);
				_eventDictionary.Add(eventName, thisEvent);
			}
		}

		public void StopListening(string eventName, UnityAction listener)
		{
			UnityEvent thisEvent = null;
			if (_eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent.RemoveListener(listener);
			}
		}

		public void TriggerEvent(string eventName)
		{
			UnityEvent thisEvent = null;
			if (_eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent.Invoke();
			}
		}
	}
}