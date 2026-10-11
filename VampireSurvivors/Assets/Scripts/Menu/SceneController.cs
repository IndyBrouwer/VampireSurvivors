using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void WeaponSelector()
    {
        SceneManager.LoadScene("Selector");
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        #if UNITY_WEBGL
            Debug.Log("Quit not supported in WebGL. Returning to main menu...");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        #else
            Application.Quit();
        #endif
    }
}