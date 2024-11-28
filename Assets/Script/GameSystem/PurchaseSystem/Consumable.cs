using UnityEditor;
using System;
using UnityEditor.VersionControl;
using UnityEditor.Purchasing;
using UnityEditor.PackageManager;
using static UnityEditor.Progress;

namespace Assets.Script.GameSystem.PurchaseSystem
{
    [Serializable]
    public abstract class Item
    {
        public string Name;
        public string Id;
        public string Description;
        public float Price;
    }

    [Serializable]
    public class NonConsumableItem : Item
    {

    }

    [Serializable]
    public class ConsumableItem : Item
    {

    }

    [Serializable]
    public class SubscriptionItem : Item
    {
        public int TimeDuration;
    }
}