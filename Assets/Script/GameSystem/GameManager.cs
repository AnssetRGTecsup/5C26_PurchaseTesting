using Assets.Script.Game;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.GameSystem
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SpaceShipData spaceShipData;

        public UnityEvent<SpaceShipData> OnStartGame;

        private void Start()
        {
            OnStartGame?.Invoke(spaceShipData);
        }
    }
}