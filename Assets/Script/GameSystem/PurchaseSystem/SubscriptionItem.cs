using UnityEngine;

namespace Assets.Script.GameSystem.PurchaseSystem
{
    [CreateAssetMenu(fileName = "Subscription Item", menuName = "Scriptable Objects/Items/Subscription", order = 2)]
    public class SubscriptionItem : Item
    {
        [SerializeField] protected int timeDuration;

        public int TimeDuration => timeDuration;
    }
}