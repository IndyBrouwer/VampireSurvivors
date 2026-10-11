using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorSlot : MonoBehaviour
{
    [SerializeField] private WeaponData carriedWeapon;

    [Header("Scripts")]
    [SerializeField] private SceneController sceneControllerScript;

    public void OnClicked()
    {
        PlayerPrefs.SetString("StartingWeapon", carriedWeapon.weaponID);

        sceneControllerScript.StartGame();
    }
}