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
            if (!weapon.Key.CanUpgrade())
            {
                unlockables.Remove(weapon.Key);
            }
        }

        //else update slot with info and remove from list

        var availableUnlockables = unlockables.ToList();

        //Loop through all slots
        for (int index = 0; index < 3; index++)
        {
            // Stop if there are no more weapons available
            if (availableUnlockables.Count == 0)
            {
                slots[index].SetActive(false);

                //If all slots are false, disable levelup menu

                continue;
            }

            //Generate random number between 0 and weaponownedstatus.count
            int randomPick = Random.Range(0, availableUnlockables.Count);

            var unlockable = availableUnlockables[randomPick];

            LevelupSlot slotScript = slots[index].GetComponent<LevelupSlot>();
            slotScript.SetupSlot(unlockable.Key, unlockable.Value, inventoryScript);

            //Remove from the list used for this shop setup (avoid duplicates)
            availableUnlockables.RemoveAt(randomPick);
        }
    }
}