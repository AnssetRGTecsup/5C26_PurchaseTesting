using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Events;
using System;
using TMPro;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Script.GameSystem.PurchaseSystem
{
    public class ShopManager : MonoBehaviour, IStoreListener
    {
        [Header("Items")]
        [SerializeField] private NonConsumableItem itemNonConsumable;
        [SerializeField] private ConsumableItem itemConsumable;
        [SerializeField] private SubscriptionItem itemSubscription;

        private IStoreController _storeController;

        public UnityEvent<int> OnPurchaseCurrency;

        public UnityEvent OnPurchaseRemoveAdds;

        public UnityEvent OnPurchaseSubscription;

        private void SetUpBuider()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            builder.AddProduct(itemNonConsumable.Id, ProductType.NonConsumable);
            builder.AddProduct(itemConsumable.Id, ProductType.Consumable);
            builder.AddProduct(itemSubscription.Id, ProductType.Subscription);

            UnityPurchasing.Initialize(this, builder);
        }


        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("Success");

            _storeController = controller;
        }

        public void BuyConsumableItem()
        {
            _storeController.InitiatePurchase(itemConsumable.Id);
        }

        public void BuyNonConsumableItem()
        {
            _storeController.InitiatePurchase(itemNonConsumable.Id);
        }

        public void BuySubscriptionItem()
        {
            _storeController.InitiatePurchase(itemSubscription.Id);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            var product = purchaseEvent.purchasedProduct;

            print("Purchase Complete" + product.definition.id);

            if (product.definition.id == itemConsumable.Id)//consumable item is pressed
            {
                /*string receipt = product.receipt;
                data = JsonUtility.FromJson<Data>(receipt);
                payload = JsonUtility.FromJson<Payload>(data.Payload);
                payloadData = JsonUtility.FromJson<PayloadData>(payload.json);

                int quantity = payloadData.quantity;

                for (int i = 0; i < quantity; i++)
                {
                    AddCoins(50);
                }*/

                OnPurchaseCurrency?.Invoke(50);
            }
            else if (product.definition.id == itemNonConsumable.Id)//non consumable
            {
                //RemoveAds();

                OnPurchaseRemoveAdds?.Invoke();
            }
            else if (product.definition.id == itemSubscription.Id)//subscribed
            {
                //ActivateElitePass();

                OnPurchaseSubscription?.Invoke();
            }

            return PurchaseProcessingResult.Complete;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("Error when Initializing: " + error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.Log("Error when Initializing: " + error + message);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log("Error when Purchasing: " + product.definition.id + failureReason);
        }

        private void CheckConsumable(string id)
        {
            if (_storeController == null)
            {
                var product = _storeController.products.WithID(id);

                if (product != null)
                {
                    //REMOVE ADSS
                }
                else
                {
                    //SHOW ADS
                }
            }
        }

        private void CheckSubscription(string id)
        {

            var subProduct = _storeController.products.WithID(id);
            if (subProduct != null)
            {
                try
                {
                    if (subProduct.hasReceipt)
                    {
                        var subManager = new SubscriptionManager(subProduct, null);
                        var info = subManager.getSubscriptionInfo();
                        /*print(info.getCancelDate());
                        print(info.getExpireDate());
                        print(info.getFreeTrialPeriod());
                        print(info.getIntroductoryPrice());
                        print(info.getProductId());
                        print(info.getPurchaseDate());
                        print(info.getRemainingTime());
                        print(info.getSkuDetails());
                        print(info.getSubscriptionPeriod());
                        print(info.isAutoRenewing());
                        print(info.isCancelled());
                        print(info.isExpired());
                        print(info.isFreeTrial());
                        print(info.isSubscribed());*/


                        if (info.isSubscribed() == Result.True)
                        {
                            print("We are subscribed");
                            //ActivateElitePass();
                        }
                        else
                        {
                            print("Un subscribed");
                            //DeActivateElitePass();
                        }

                    }
                    else
                    {
                        print("receipt not found !!");
                    }
                }
                catch (Exception)
                {

                    print("It only work for Google store, app store, amazon store, you are using fake store!!");
                }
            }
            else
            {
                print("product not found !!");
            }

        }

        [Header("Consumable")]
        public TextMeshProUGUI coinTxt;
        void AddCoins(int num)
        {
            int coins = PlayerPrefs.GetInt("totalCoins");
            coins += num;
            PlayerPrefs.SetInt("totalCoins", coins);
            StartCoroutine(startCoinShakeEffect(coins - num, coins, .5f));
        }
        float val;
        IEnumerator startCoinShakeEffect(int oldValue, int newValue, float animTime)
        {
            float ct = 0;
            float nt;
            float tot = animTime;
            coinTxt.GetComponent<Animation>().Play("textShake");
            while (ct < tot)
            {
                ct += Time.deltaTime;
                nt = ct / tot;
                val = Mathf.Lerp(oldValue, newValue, nt);
                coinTxt.text = ((int)(val)).ToString();
                yield return null;
            }
            coinTxt.GetComponent<Animation>().Stop();

        }
    }

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

    public class PremiumAccess : MonoBehaviour
    {
        [SerializeField] private GameObject premiumHolderGameObject;

        public void DisablePremium()
        {
            premiumHolderGameObject.SetActive(true);
        }

        public void EnablePremium()
        {
            premiumHolderGameObject.SetActive(false);
        }
    }

    public class PurchaseCurrencyHolder : MonoBehaviour
    {
        [Header("Currency Data")]
        [SerializeField] private CurrencyData currencyData;

        [Header("Data Holders")]
        [SerializeField] private Image currencyIcon;
        [SerializeField] private TextMeshProUGUI currencyText;

        [Header("Purchase Settings")]
        [SerializeField] private Button buyCurrencyBtn;
        [SerializeField] private TextMeshProUGUI buyAmountText;
        [SerializeField] private Image currencyIconBtn;

        private void Start()
        {
            currencyIcon.sprite = currencyData.Icon;
            currencyText.text = currencyData.Quantity.ToString("000");

            currencyIconBtn.sprite = currencyData.Icon;
            buyAmountText.text = $"+{currencyData.Item.Price.ToString("00")}";
        }

        private void UpdateCurrency()
        {

        }

        void AddCoins(int num)
        {
            int coins = PlayerPrefs.GetInt("totalCoins");
            coins += num;
            PlayerPrefs.SetInt("totalCoins", coins);
            StartCoroutine(startCoinShakeEffect(coins - num, coins, .5f));
        }
        float val;
        IEnumerator startCoinShakeEffect(int oldValue, int newValue, float animTime)
        {
            float ct = 0;
            float nt;
            float tot = animTime;
            while (ct < tot)
            {
                ct += Time.deltaTime;
                nt = ct / tot;
                val = Mathf.Lerp(oldValue, newValue, nt);
                currencyText.text = ((int)(val)).ToString();
                yield return null;
            }

        }
    }
}