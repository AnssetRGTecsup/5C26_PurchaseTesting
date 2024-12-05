using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Events;
using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Script.GameSystem.PurchaseSystem
{
    public class PurchaseSystemManager : MonoBehaviour, IStoreListener
    {
        [Header("Non Consumable Items")]
        [SerializeField] private List<ProductItem> nonConsumables;

        [Header("Game Events")]

        public UnityEvent OnPurchaseRemoveAdds;

        public UnityEvent OnPurchaseSubscription;
        
        private IStoreController _storeController;
        private ProductItem _currentProduct;

        private void Start()
        {
            Debug.Log("Called from Start");
            SetUpBuider();
        }

        private void SetUpBuider()
        {
            Debug.Log("Called from SetUpBuilder");

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (var product in nonConsumables)
            {
                builder.AddProduct(product.Item.Id, ProductType.NonConsumable);
            }

            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("Success");

            _storeController = controller;
        }

        public void BuyNonConsumableItem(ProductItem product)
        {
            _currentProduct = product;

            _storeController.InitiatePurchase(product.Item.Id);   
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("Error when Initializing: " + error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.Log("Error when Initializing: " + error + message);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            var product = purchaseEvent.purchasedProduct;

            print("Purchase Complete" + product.definition.id);

            if (_currentProduct != null)
            {
                _currentProduct.RaiseEvent();
            }
            //TO DO: CONSUMABLES AND SUBSCRIPTIONS

            _currentProduct = null;

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log("Error when Purchasing: " + product.definition.id + failureReason);
        }

        
    }
}