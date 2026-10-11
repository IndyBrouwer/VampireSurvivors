using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Dictionary for each category with a bool to keep track of if the player has that item
    [Header("Dictionaries")]
    private Dictionary<WeaponData, bool> ownedWeapons = new Dictionary<WeaponData, bool>();
    private Dictionary<WeaponUpgradeData, bool> ownedWeaponUpgrades = new Dictionary<WeaponUpgradeData, bool>();
    private Dictionary<ItemData, bool> ownedItems = new Dictionary<ItemData, bool>();
    private Dictionary<ItemUpgradeData, bool> ownedItemUpgrades = new Dictionary<ItemUpgradeData, bool>();

    [Header("Weapon Lists")]
    [SerializeField] private WeaponData[] allWeapons;
    [SerializeField] private WeaponUpgradeData[] allWeaponUpgrades;

    [Header("Item Lists")]
    [SerializeField] private ItemData[] allItems;
    [SerializeField] private ItemUpgradeData[] allItemUpgrades;

    [Header("Scripts")]
    [SerializeField] private WeaponManager weaponManager;
    private InventoryUI inventoryUIScript;

    private void Start()
    {
        inventoryUIScript = GetComponent<InventoryUI>();

        //Setup weaponManager dictionary
        weaponManager.SetupDictionary(allWeapons);

        SetupDictionaries();

        //Get starting weapon
        string startingWeaponID = PlayerPrefs.GetString("StartingWeapon", "weapon_nearest");

        //Search for startingweaponid in allweapon dictionary
        for (int index = 0; index < allWeapons.Length; index++)
        {
            if (startingWeaponID == allWeapons[index].weaponID)
            {
                AddWeapon(allWeapons[index]);
            }
        }
    }

    private void SetupDictionaries()
    {
        foreach (WeaponData weapon in allWeapons)
        {
            ownedWeapons.Add(weapon, false);
        }

        foreach (WeaponUpgradeData weaponUpgrade in allWeaponUpgrades)
        {
            ownedWeaponUpgrades.Add(weaponUpgrade, false);
        }
        
        foreach (ItemData item in allItems)
        {
            ownedItems.Add(item, false);
        }

        foreach (ItemUpgradeData itemUpgrade in allItemUpgrades)
        {
            ownedItemUpgrades.Add(itemUpgrade, false);
        }
    }

    public void AddWeapon(WeaponData weapon)
    {
        //Edit weapons value instead of trying to add it again as it already exists
        ownedWeapons[weapon] = true;

        //Add weapon icon to UI
        inventoryUIScript.AddWeapon(weapon);

        //Enable newly added weapon
        weaponManager.SearchPair(weapon);
    }

    public void AddWeaponUpgrade(WeaponUpgradeData upgrade)
    {
        //Edit weaponupgrades value instead of trying to add it again as it already exists
        ownedWeaponUpgrades[upgrade] = true;

        //Apply upgrade to weapon
    }

    public void AddItem(ItemData item)
    {
        //Edit items value instead of trying to add it again as it already exists
        ownedItems[item] = true;

        //Add item icon to UI
        inventoryUIScript.AddItem(item);

        //Apply effects of item to gameplay
    }

    public void AddItemUpgrade(ItemUpgradeData upgrade)
    {
        //Edit itemUpgrades value instead of trying to add it again as it already exists
        ownedItemUpgrades[upgrade] = true;

        //Apply upgrade to item
    }
}