using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLeveling : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider experienceSlider;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Leveling Tracking")]
    private float currentExperience = 0;
    private int currentLevel = 0;

    [Header("Scripts")]
    [SerializeField] private LevelupController selectorControllerScript;
    [SerializeField] private PauseGame pauseGameScript;

    private void Start()
    {
        UpdateLevelUI(false);
    }

    public void AddExperience(float amount)
    {
        currentExperience += amount;

        //Check if the player has enough experience to level up
        if (currentExperience >= GetExperienceForCurrentLevel())
        {
            //Check if there was any leftover experience after leveling up
            if (currentExperience > GetExperienceForCurrentLevel())
            {
                currentExperience -= GetExperienceForCurrentLevel();
            }
            else
            {
                currentExperience = 0;
            }

            LevelUp();
        }

        UpdateExperienceUI();
    }

    private void LevelUp()
    {
        currentLevel++;

        UpdateLevelUI(true);
    }

    private float GetExperienceForCurrentLevel()
    {
        //Get the current max experience needed
        float currentExperienceNeeded = experienceSlider.maxValue;

        return currentExperienceNeeded;
    }

    private void UpdateExperienceUI()
    {
        //Update the experience sliders value to reflect the current experience
        experienceSlider.value = currentExperience;
    }

    private void UpdateLevelUI(bool increaseMax)
    {
        //Update the displayed level text
        levelText.text = $"Lvl: {currentLevel}";
        
        UpdateExperienceUI();

        //If player actually leveled up
        if (increaseMax)
        {
            //Pause game
            pauseGameScript.Pause();

            //Set the new max value of the experience slider
            experienceSlider.maxValue = GetExperienceForCurrentLevel() * 1.5f;

            selectorControllerScript.EnableSelector();
        }
    }
}