using Assets.Script.Game;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.GameSystem
{
    public class FieldNumberUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text textMeshPro;
        [SerializeField] private string numberFormat = "00";

        public void UpdateText(int newValue)
        {
            textMeshPro.text = newValue.ToString(numberFormat);
        }
    }
}