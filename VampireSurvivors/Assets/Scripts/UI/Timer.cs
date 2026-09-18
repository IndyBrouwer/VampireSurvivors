using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float currentTime;

    private void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;

        int seconds = Mathf.FloorToInt(currentTime % 60);
        int minutes = Mathf.FloorToInt(currentTime / 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}