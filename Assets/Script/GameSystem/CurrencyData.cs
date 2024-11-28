using Assets.Script.GameSystem.PurchaseSystem;
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
        [SerializeField] protected NonConsumableItem item;

        [SerializeField, TextArea] protected string description;

        public Sprite Icon => icon;
        public int Quantity => quantity;
        public NonConsumableItem Item => item;

        public event Action<int> OnGainCurrency;


    }
}