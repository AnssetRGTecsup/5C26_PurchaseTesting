using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.GameSystem
{
    [CreateAssetMenu(fileName = "Custom Currency", menuName = "Scriptable Objects/Currency System/Custom Currency")]
    public class CurrencyData : ScriptableObject
    {
        [SerializeField] protected Sprite icon;
        [SerializeField] protected int quantity;

        [SerializeField, TextArea] protected string description;

        public Sprite Icon => icon;
        public int Quantity => quantity;

        public void ModifyQuantity(int value)
        {
            quantity = Mathf.Clamp(quantity + value, 0, 9999);
        }
    }
}