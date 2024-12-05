using Assets.Script.GameSystem.GameEvents;
using Assets.Script.GameSystem.PurchaseSystem;
using UnityEngine;

namespace Assets.Script.GameSystem
{
    [CreateAssetMenu(fileName = "Non Consuamble Product", menuName = "Scriptable Objects/Product/Non Consumable")]
    public class ProductItem : ScriptableObject
    {
        [SerializeField] protected CurrencyData currencyData;
        [SerializeField] protected NonConsumableItem item;
        [SerializeField] protected GameEventCurrency currencyEvent;

        [SerializeField, TextArea] protected string description;

        public CurrencyData CurrencyData => currencyData;
        public NonConsumableItem Item => item;

        public void RaiseEvent()
        {
            currencyEvent.Raise(currencyData);
        }
    }
}