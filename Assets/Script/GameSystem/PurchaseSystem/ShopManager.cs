using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Events;
using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;

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

        private void Start()
        {
            Debug.Log("Called from Start");
            SetUpBuider();
        }

        private void SetUpBuider()
        {
            Debug.Log("Called from SetUpBuilder");

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

            if (product.definition.id == itemNonConsumable.Id)//consumable item is pressed
            {
                OnPurchaseCurrency?.Invoke(50);
            }
            else if (product.definition.id == itemConsumable.Id)//non consumable
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
    }
}