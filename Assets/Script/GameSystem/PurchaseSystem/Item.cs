using UnityEngine;

namespace Assets.Script.GameSystem.PurchaseSystem
{
    public abstract class Item : ScriptableObject
    {
        [SerializeField] protected string itemName;
        [SerializeField] protected string id;
        [SerializeField, TextArea] protected string description;
        [SerializeField] protected int amount;
        [SerializeField] protected float price;

        public string ItemName => itemName;
        public string Id => id;
        public int Amount => amount;
        public float Price => price;
    }
}