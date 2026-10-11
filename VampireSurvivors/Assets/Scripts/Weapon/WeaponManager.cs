using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("List of weapon controllers and weapon data")]
    [SerializeField] private GameObject[] allControllers;
    
    private Dictionary<WeaponData, GameObject> weaponPairs = new Dictionary<WeaponData, GameObject>();

    public void SetupDictionary(WeaponData[] allWeapons)
    {
        for (int index = 0; index < allWeapons.Length; index++)
        {
            weaponPairs.Add(allWeapons[index], allControllers[index]);

            Debug.Log("Added a new weaponPair: "+ allWeapons[index] + allControllers[index].name);
        }
    }

    public void SearchPair(WeaponData weapon)
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