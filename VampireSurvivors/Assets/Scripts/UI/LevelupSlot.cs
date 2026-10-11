using UnityEngine;

public class LevelupSlot : MonoBehaviour, IClickable
{
    [SerializeField] private LevelupController levelupControllerScript;
    [SerializeField] private PauseGame pauseGameScript;

    public void OnButtonClick()
    {
        levelupControllerScript.DisableSelector();

        pauseGameScript.Resume();
    }
}