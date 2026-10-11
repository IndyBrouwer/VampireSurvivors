using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
    }

    //When player clicks esc key or maybe the pause button later on
    public void TogglePauseMenu()
    {
        if (Time.timeScale == 1f)
        {
            //Pause the game
            Time.timeScale = 0f;

            pauseMenu.SetActive(true);
        }
        else
        {
            //Resume the game
            Time.timeScale = 1f;

            pauseMenu.SetActive(false);
        }
    }

    //Triggered on actions that should not trigger the menu (player levelup, player death)
    public void Pause()
    {
        //Pause time
        Time.timeScale = 0f;
    }

    //Triggered after player bought a levelup reward
    public void Resume()
    {
        Time.timeScale = 1f;
    }
}