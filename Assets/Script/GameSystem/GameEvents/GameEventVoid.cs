using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.GameSystem.GameEvents
{
    [CreateAssetMenu(fileName = "Event Void", menuName = "Game Events/Void", order = 0)]
    public class GameEventVoid : ScriptableObject
    {
        private List<EventVoidListener> _gameEventListeners = new ();

        [Tooltip("Description of the Event")]
        [SerializeField, TextArea] private string description;

        public void Raise()
        {
            Debug.Log("Rising Event");

            foreach (var listener in _gameEventListeners)
            {
                listener.OnEventRaised();
            }
        }

        public void Register(EventVoidListener listener)
        {
            if (_gameEventListeners.Contains(listener)) return;

            _gameEventListeners.Add(listener);
        }

        public void Unregister(EventVoidListener listener)
        {
            if (!_gameEventListeners.Contains(listener)) return;

            _gameEventListeners.Remove(listener);
        }
    }
}