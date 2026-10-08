using TMPro;
using UnityEngine;

public class ShopWallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    private int totalGoldAmount;

    void Start()
    {
        totalGoldAmount = PlayerPrefs.GetInt("CoinCount", 0);

        UpdateUI();
    }

    public void TakeCoins(int amount)
    {
        //Subtract the amount from the coin count
        totalGoldAmount -= amount;

        PlayerPrefs.SetInt("CoinCount", totalGoldAmount);

        UpdateUI();
    }

    private void UpdateUI()
    {
        coinText.text = totalGoldAmount.ToString();
    }
}