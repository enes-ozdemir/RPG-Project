namespace _Scripts.Managers
{
    using UnityEngine;
    using UnityEngine.Events;
    using System.Collections.Generic;

    public class EventManager : MonoBehaviour
    {
        private static Dictionary<GameEvent, UnityEvent> _eventDictionary;

        private void Awake()
        {
            _eventDictionary = new Dictionary<GameEvent, UnityEvent>();
        }

        public static void AddListener(GameEvent eventName, UnityAction listener)
        {
            if (_eventDictionary.TryGetValue(eventName, out var e))
            {
                e.AddListener(listener);
            }
            else
            {
                var newEvent = new UnityEvent();
                newEvent.AddListener(listener);
                _eventDictionary.Add(eventName, newEvent);
            }
        }

        public static void RemoveListener(GameEvent eventName, UnityAction listener)
        {
            if (_eventDictionary.TryGetValue(eventName, out var e))
            {
                e.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(GameEvent eventName)
        {
            if (_eventDictionary.TryGetValue(eventName, out var e))
            {
                e.Invoke();
            }
        }
    }
}