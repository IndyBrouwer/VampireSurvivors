using TMPro;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    private int coinCount;

    private void Start()
    {
        coinCount = PlayerPrefs.GetInt("CoinCount", 0);

        UpdateUI();
    }

    public void AddCoins(float amount)
    {
        //Make float into int, (round up)
        int intAmount = Mathf.CeilToInt(amount);

        //Add the amount to the coin count
        coinCount += intAmount;

        PlayerPrefs.SetInt("CoinCount", coinCount);

        UpdateUI();
    }

    public void TakeCoins(int amount)
    {
        //Subtract the amount from the coin count
        coinCount -= amount;

        PlayerPrefs.SetInt("CoinCount", coinCount);

        UpdateUI();
    }

    private void UpdateUI()
    {
        coinText.text = coinCount.ToString();
    }
}