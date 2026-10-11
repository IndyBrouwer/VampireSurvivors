using UnityEngine;

public class LevelupSlot : MonoBehaviour
{
    [SerializeField] private LevelupController levelupControllerScript;
    [SerializeField] private PauseGame pauseGameScript;

    public void OnButtonClick()
    {
        levelupControllerScript.DisableSelector();

        pauseGameScript.Resume();
    }
}