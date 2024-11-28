using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.GameSystem.PurchaseSystem
{
    public class PremiumAccess : MonoBehaviour
    {
        [SerializeField] private GameObject premiumHolderGameObject;

        public UnityEvent OnEnablePremium;
        public UnityEvent OnDisablePremium;

        public void DisablePremium()
        {
            premiumHolderGameObject.SetActive(true);

            OnDisablePremium?.Invoke();
        }

        public void EnablePremium()
        {
            premiumHolderGameObject.SetActive(false);

            OnEnablePremium?.Invoke();
        }
    }
}