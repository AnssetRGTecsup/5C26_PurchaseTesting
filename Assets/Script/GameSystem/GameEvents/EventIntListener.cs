using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.GameSystem.GameEvents
{
    public class EventIntListener : MonoBehaviour
    {
        [SerializeField] private GameEventInt GameEvent;

        public UnityEvent<int> Response;

        private void OnEnable()
        {
            GameEvent.Register(this);
        }

        private void OnDisable()
        {
            GameEvent.Unregister(this);
        }

        public void OnEventRaised(int value)
        {
            Debug.Log("Notifying Reponse");

            Response?.Invoke(value);
        }
    }
}