using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.GameSystem.GameEvents
{
    public class EventFloatListener : MonoBehaviour
    {
        [SerializeField] private GameEventFloat GameEvent;

        public UnityEvent<float> Response;

        private void OnEnable()
        {
            GameEvent.Register(this);
        }

        private void OnDisable()
        {
            GameEvent.Unregister(this);
        }

        public void OnEventRaised(float value)
        {
            Debug.Log("Notifying Reponse");

            Response?.Invoke(value);
        }
    }
}