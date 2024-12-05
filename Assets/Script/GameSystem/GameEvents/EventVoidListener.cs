using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.GameSystem.GameEvents
{
    public class EventVoidListener : MonoBehaviour
    {
        [SerializeField] private GameEventVoid GameEvent;

        public UnityEvent Response;

        private void OnEnable()
        {
            GameEvent.Register(this);
        }

        private void OnDisable()
        {
            GameEvent.Unregister(this);
        }

        public void OnEventRaised()
        {
            Debug.Log("Notifying Reponse");

            Response?.Invoke();
        }
    }
}