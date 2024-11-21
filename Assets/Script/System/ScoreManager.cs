using Assets.Script.Game;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.System
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private SpaceShipData spaceShipData;
        [SerializeField] private int currentScore;

        public UnityEvent<int> ScoreComplete;

        private float _time;

        public void SetUpData(SpaceShipData newData)
        {
            spaceShipData = newData;

            ScoreComplete?.Invoke(currentScore);
        }

        private void Update()
        {
            _time += Time.deltaTime * spaceShipData.Speed;

            if(_time > 1f)
            {
                currentScore++;

                ScoreComplete?.Invoke(currentScore);

                _time = 0;
            }
        }

        public void ModifyScore(int value)
        {
            currentScore = Math.Clamp(currentScore + value, 0, 99999);

            ScoreComplete?.Invoke(currentScore);
        }
    }
}