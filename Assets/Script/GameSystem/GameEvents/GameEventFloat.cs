using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.GameSystem.GameEvents
{
    [CreateAssetMenu(fileName = "Event Float", menuName = "Game Events/Float", order = 2)]
    public class GameEventFloat : ScriptableObject
    {
        private List<EventFloatListener> _gameEventListeners = new ();

        [Tooltip("Description of the Event")]
        [SerializeField, TextArea] private string description;

        public void Raise(float value)
        {
            Debug.Log("Rising Event");

            foreach (var listener in _gameEventListeners)
            {
                listener.OnEventRaised(value);
            }
        }

        public void Register(EventFloatListener listener)
        {
            if (_gameEventListeners.Contains(listener)) return;

            _gameEventListeners.Add(listener);
        }

        public void Unregister(EventFloatListener listener)
        {
            if (!_gameEventListeners.Contains(listener)) return;

            _gameEventListeners.Remove(listener);
        }
    }
}