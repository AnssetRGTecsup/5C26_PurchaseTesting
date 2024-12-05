using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.GameSystem.GameEvents
{
    public class EventCurrencyListener : MonoBehaviour
    {
        [SerializeField] private GameEventCurrency GameEvent;

        public UnityEvent<CurrencyData> Response;

        private void OnEnable()
        {
            GameEvent.Register(this);
        }

        private void OnDisable()
        {
            GameEvent.Unregister(this);
        }

        public void OnEventRaised(CurrencyData value)
        {
            Debug.Log("Notifying Reponse");

            Response?.Invoke(value);
        }
    }
}