using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private SceneController sceneControllerScript;

    public void ShowGameOverUI()
    {
        gameObject.SetActive(true);
    }

    public void HideGameOverUI()
    {
        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        HideGameOverUI();

        sceneControllerScript.StartGame();
    }

    public void ReturnToMainMenu()
    {
        HideGameOverUI();
        sceneControllerScript.MainMenu();
    }
}