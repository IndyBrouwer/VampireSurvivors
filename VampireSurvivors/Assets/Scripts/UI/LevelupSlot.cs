using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelupSlot : MonoBehaviour, IClickable
{
    [Header("UI")]
    [SerializeField] private Image upgradeIcon;
    [SerializeField] private TextMeshProUGUI upgradeObjectName;
    [SerializeField] private TextMeshProUGUI upgradeDescription;
    
    private bool owned;

    [SerializeField] private AudioClip sfx;

    [Header("Scripts")]
    [SerializeField] private LevelupController levelupControllerScript;
    [SerializeField] private PauseGame pauseGameScript;
    private Inventory inventoryScript;
    private Weapon weaponScript;

    public void SetupSlot(Weapon weapon, bool isUnlocked, Inventory inventory)
    {
        weaponScript = weapon;
        owned = isUnlocked;
        inventoryScript = inventory;

        if (owned)
        {
            //Display upgrade description
            int level = weapon.GetCurrentLevel();
            WeaponUpgradeData[] upgrades = weapon.GetWeaponData().upgrades;

            WeaponUpgradeData currentUpgrade = upgrades[level];

            upgradeIcon.sprite = weapon.GetWeaponData().sprite;
            upgradeObjectName.text = weapon.GetWeaponData().weaponName;
            upgradeDescription.text = currentUpgrade.upgradeDescription;
        }
        else
        {
            upgradeIcon.sprite = weapon.GetWeaponData().sprite;
            upgradeObjectName.text = weapon.GetWeaponData().weaponName;
            upgradeDescription.text = weapon.GetWeaponData().description;
        }
    }

    public void OnButtonClick()
    {
        //Apply upgrade
        if (owned)
        {
            weaponScript.Upgrade();
        }
        else
        {
            inventoryScript.AddWeapon(weaponScript);
        }

        AudioManager.Instance.sfxManager.PlaySFX(sfx);

        levelupControllerScript.DisableSelector();

        pauseGameScript.Resume();
    }
}