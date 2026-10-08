using TMPro;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    private int runGoldAmount;
    private int totalGoldAmount;

    private void Start()
    {
        runGoldAmount = 0;
        totalGoldAmount = PlayerPrefs.GetInt("CoinCount", 0);

        UpdateUI();
    }

    public void AddCoins(float amount)
    {
        //Make float into int, (round up)
        int intAmount = Mathf.CeilToInt(amount);

        //Add the amount to the coin count
        runGoldAmount += intAmount;

        int newTotal = totalGoldAmount + runGoldAmount;

        PlayerPrefs.SetInt("CoinCount", newTotal);

        Debug.Log(newTotal);

        UpdateUI();
    }

    private void UpdateUI()
    {
        coinText.text = runGoldAmount.ToString();
    }
}