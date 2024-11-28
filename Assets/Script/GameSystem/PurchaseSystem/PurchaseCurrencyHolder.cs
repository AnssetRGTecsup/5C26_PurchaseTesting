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

        public void UpdateCurrency(int num)
        {
            Debug.Log("Called to Buy");

            int coins = PlayerPrefs.GetInt("totalCoins");
            coins += num;
            PlayerPrefs.SetInt("totalCoins", coins);
            StartCoroutine(startCoinShakeEffect(coins - num, coins, .5f));
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
                currencyText.text = ((int)(val)).ToString();
                yield return null;
            }

        }
    }
}