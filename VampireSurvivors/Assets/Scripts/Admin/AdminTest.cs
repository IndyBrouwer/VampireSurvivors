using UnityEngine;

public class AdminTest : MonoBehaviour
{
    [SerializeField] private PlayerLeveling levelingScript;

    public void AdminXP()
    {
        levelingScript.AddExperience(50);
    }
}
