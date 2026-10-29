using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    //Dictionary for each category with a bool to keep track of if the player has that item
    [Header("Dictionaries")]
    private Dictionary<Weapon, bool> weaponStatuses = new Dictionary<Weapon, bool>();
    private Dictionary<WeaponUpgradeData, bool> weaponUpgradeStatuses = new Dictionary<WeaponUpgradeData, bool>();
    private Dictionary<ItemData, bool> itemStatuses = new Dictionary<ItemData, bool>();
    private Dictionary<ItemUpgradeData, bool> itemUpgradeStatuses = new Dictionary<ItemUpgradeData, bool>();

    [Header("Weapon Lists")]
    [SerializeField] private Weapon[] allWeapons;
    //[SerializeField] private WeaponUpgradeData[] allWeaponUpgrades;
    private List<WeaponUpgradeData> allWeaponUpgrades = new List<WeaponUpgradeData>();

    [Header("Item Lists")]
    [SerializeField] private ItemData[] allItems;
    [SerializeField] private ItemUpgradeData[] allItemUpgrades;

    [Header("Scripts")]
    [SerializeField] private WeaponManager weaponManager;
    private InventoryUI inventoryUIScript;

    private void Awake()
    {
        Instance = this;

        CreateUpgradeList();
    }

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
            if (startingWeaponID == allWeapons[index].GetWeaponData().weaponID)
            {
                AddWeapon(allWeapons[index]);
            }
        }
    }

    private void CreateUpgradeList()
    {
        foreach (Weapon weapon in allWeapons)
        {
            WeaponData weaponData = weapon.GetWeaponData();

            foreach (WeaponUpgradeData upgrade in weaponData.upgrades)
            {
                allWeaponUpgrades.Add(upgrade);
            }
        }
    }

    private void SetupDictionaries()
    {
        foreach (Weapon weapon in allWeapons)
        {
            weaponStatuses.Add(weapon, false);
        }

        foreach (WeaponUpgradeData weaponUpgrade in allWeaponUpgrades)
        {
            weaponUpgradeStatuses.Add(weaponUpgrade, false);
        }
        
        foreach (ItemData item in allItems)
        {
            itemStatuses.Add(item, false);
        }

        foreach (ItemUpgradeData itemUpgrade in allItemUpgrades)
        {
            itemUpgradeStatuses.Add(itemUpgrade, false);
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        //Edit weapons value instead of trying to add it again as it already exists
        weaponStatuses[weapon] = true;

        //Add weapon icon to UI
        inventoryUIScript.AddWeapon(weapon.GetWeaponData());

        //Enable newly added weapon
        weaponManager.SearchPair(weapon);
    }

    public void AddWeaponUpgrade(WeaponUpgradeData upgrade)
    {
        //Edit weaponupgrades value instead of trying to add it again as it already exists
        weaponUpgradeStatuses[upgrade] = true;

        //Apply upgrade to weapon
    }

    public void AddItem(ItemData item)
    {
        //Edit items value instead of trying to add it again as it already exists
        itemStatuses[item] = true;

        //Add item icon to UI
        inventoryUIScript.AddItem(item);

        //Apply effects of item to gameplay
    }

    public void AddItemUpgrade(ItemUpgradeData upgrade)
    {
        //Edit itemUpgrades value instead of trying to add it again as it already exists
        itemUpgradeStatuses[upgrade] = true;

        //Apply upgrade to item
    }

    public Dictionary<Weapon, bool> GetWeaponDictionary()
    {
        return weaponStatuses;
    }
}