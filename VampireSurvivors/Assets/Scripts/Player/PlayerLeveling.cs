using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLeveling : MonoBehaviour
{
    [SerializeField] private Slider experienceSlider;
    [SerializeField] private TextMeshProUGUI levelText;

    private float currentExperience = 0;
    private int currentLevel = 0;

    private void Start()
    {
        UpdateLevelUI();
    }

    private void UpdateLevelUI()
    {
        experienceSlider.value = currentExperience;
        levelText.text = $"Lvl: {currentLevel}";
    }
}
