using UnityEngine;

public class LevelupController : MonoBehaviour
{
    [SerializeField] private GameObject levelupSelector;

    public void EnableSelector()
    {
        levelupSelector.SetActive(true);
    }

    public void DisableSelector()
    {
        levelupSelector.SetActive(false);
    }
}