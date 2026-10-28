using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("List of all weapon controllers")]
    [SerializeField] private GameObject[] allControllers;
    
    private Dictionary<Weapon, GameObject> weaponPairs = new Dictionary<Weapon, GameObject>();

    public void SetupDictionary(Weapon[] allWeapons)
    {
        for (int index = 0; index < allWeapons.Length; index++)
        {
            weaponPairs.Add(allWeapons[index], allControllers[index]);

            Debug.Log("Added a new weaponPair: "+ allWeapons[index] + allControllers[index].name);
        }
    }

    public void SearchPair(Weapon weapon)
    {
        //Search dictionary for gameobject match of weapondata
        foreach (var pair in weaponPairs)
        {
            //If received weaponData is in dictionary
            if (pair.Key == weapon)
            {
                //Get the gameobject that belongs to that scriptabe object
                //Enable that gameobject
                EnableWeapon(pair.Value);
            }
        }
    }

    public void EnableWeapon(GameObject weaponController)
    {
        weaponController.SetActive(true);
    }

    public void DisableWeapons()
    {
        foreach (GameObject controller in allControllers)
        {
            controller.SetActive(false);
        }
    }
}