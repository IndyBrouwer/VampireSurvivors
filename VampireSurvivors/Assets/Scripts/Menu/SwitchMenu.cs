using UnityEngine;

public class SwitchMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseOverview;
    [SerializeField] private GameObject optionsOverview;

    public void PauseOverview()
    {
        optionsOverview.SetActive(false);
        pauseOverview.SetActive(true);
    }

    public void OptionsOverview()
    {
        pauseOverview.SetActive(false);
        optionsOverview.SetActive(true);
    }
}
