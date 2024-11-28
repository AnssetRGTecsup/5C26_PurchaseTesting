using UnityEngine;

namespace Assets.Script.GameSystem.PurchaseSystem
{
    public class AdHolder : MonoBehaviour
    {
        [SerializeField] private GameObject adHolderGameObject;

        public void SetUpAd()
        {
            adHolderGameObject.SetActive(true);
        }

        public void DisableAds()
        {
            adHolderGameObject.SetActive(false);

            Destroy(this);
        }
    }
}