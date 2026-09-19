using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killCounterText;
    private int killCount = 0;

    public void IncreaseKillCount()
    {
        killCount++;
        UpdateKillCounter();
    }

    private void UpdateKillCounter()
    {
        killCounterText.text = killCount.ToString();
    }
}