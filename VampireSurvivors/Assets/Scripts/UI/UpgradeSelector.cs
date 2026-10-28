using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeSelector : MonoBehaviour
{
    [Header("Levelup menu slots")]
    [SerializeField] private GameObject[] slots;

    [SerializeField] private Inventory inventoryScript;

    //Check which weapons, upgrades and items the player currently has, (required inventory script)

    //Pick the presented upgrades on level up based on what player has

    //Call for each upgrade to setup itself by giving it the upgradeData or itemData

    public void FillSlots()
    {
        var unlockables = inventoryScript.GetWeaponDictionary();

        //use dictionary.ToList() to loop a copy of the dictionary while removing invalid entries from the actual dictionary
        foreach (var weapon in unlockables.ToList())
        {
            //if (weapon.Value is false)
            //{
            //    unlockables.Remove(weapon.Key);
            //}

            if (!weapon.Key.CanUpgrade())
            {
                unlockables.Remove(weapon.Key);
            }
        }

        //else update slot with info and remove from list

        for (int index = 0; index < 3; index++)
        {
            //Check if list is empty, continue.
            if (unlockables.Count > 0)
            {
                //Generate random number between 0 and weaponownedstatus.count
                int randomPick = Random.Range(0, unlockables.Count);

                LevelupSlot slotScript = slots[index].GetComponent<LevelupSlot>();
                slotScript.SetupSlot(unlockables.ElementAt(randomPick).Key, unlockables.ElementAt(randomPick).Value, inventoryScript);
            }
            else
            {
                continue;
            }

            //slots[index].GetComponent<LevelupSlot>().SetupSlot(weaponOwnedStatus.ElementAt(index).Key, weaponOwnedStatus.ElementAt(index).Value, weaponManagerScript);
        }
    }
}