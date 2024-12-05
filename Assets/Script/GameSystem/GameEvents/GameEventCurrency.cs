using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.GameSystem.GameEvents
{
    [CreateAssetMenu(fileName = "Event Currency", menuName = "Game Events/Currency", order = 3)]
    public class GameEventCurrency : ScriptableObject
    {
        private List<EventCurrencyListener> _gameEventListeners = new();

        [Tooltip("Description of the Event")]
        [SerializeField, TextArea] private string description;

        private void OnEnable()
        {
            _gameEventListeners = new();
        }

        private void OnDisable()
        {
            _gameEventListeners.Clear();
        }

        public void Raise(CurrencyData value)
        {
            Debug.Log("Rising Event");

            foreach (var listener in _gameEventListeners)
            {
                listener.OnEventRaised(value);
            }
        }

        public void Register(EventCurrencyListener listener)
        {

            if (_gameEventListeners.Contains(listener)) return;

            _gameEventListeners.Add(listener);
        }

        public void Unregister(EventCurrencyListener listener)
        {
            if (!_gameEventListeners.Contains(listener)) return;

            _gameEventListeners.Remove(listener);
        }
    }
}