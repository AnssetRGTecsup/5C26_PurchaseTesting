using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Script.GameSystem.PurchaseSystem
{
    public class PurchaseCurrencyHolder : MonoBehaviour
    {
        [Header("Currency Data")]
        [SerializeField] private CurrencyData currencyData;
        [SerializeField] private NonConsumableItem nonConsumableItem;

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
            buyAmountText.text = $"+{nonConsumableItem.Amount.ToString("00")}";
        }

        public void UpdateCurrency()
        {
            Debug.Log("Called to Buy");

            int amount = nonConsumableItem.Amount;

            int _currentValue = currencyData.Quantity;
            currencyData.ModifyQuantity(amount);


            StartCoroutine(startCoinShakeEffect(_currentValue, currencyData.Quantity, .5f));
        }

        IEnumerator startCoinShakeEffect(int oldValue, int newValue, float animTime)
        {
            float ct = 0;
            float nt;
            float tot = animTime;
            float val;
            while (ct < tot)
            {
                ct += Time.deltaTime;
                nt = ct / tot;
                val = Mathf.Lerp(oldValue, newValue, nt);
                currencyText.text = ((int)(val)).ToString("000");
                yield return null;
            }

        }
    }
}