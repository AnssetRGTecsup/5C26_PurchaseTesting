using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.GameSystem.GameEvents
{
    [CreateAssetMenu(fileName = "Event Int", menuName = "Game Events/Int", order = 1)]
    public class GameEventInt: ScriptableObject
    {
        private List<EventIntListener> _gameEventListeners = new ();

        [Tooltip("Description of the Event")]
        [SerializeField, TextArea] private string description;

        public void Raise(int value)
        {
            Debug.Log("Rising Event");

            foreach (var listener in _gameEventListeners)
            {
                listener.OnEventRaised(value);
            }
        }

        public void Register(EventIntListener listener)
        {
            if (_gameEventListeners.Contains(listener)) return;

            _gameEventListeners.Add(listener);
        }

        public void Unregister(EventIntListener listener)
        {
            if (!_gameEventListeners.Contains(listener)) return;

            _gameEventListeners.Remove(listener);
        }
    }
}