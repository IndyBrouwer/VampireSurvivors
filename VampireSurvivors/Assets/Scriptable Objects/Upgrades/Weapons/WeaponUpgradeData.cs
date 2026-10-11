using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUpgradeData", menuName = "Scriptable Objects/WeaponUpgradeData")]
public class WeaponUpgradeData : ScriptableObject
{
    //Upgrade description
    public string upgradeDescription;

    //Needs a max and min value
    [Range(1, 5)]
    public int upgradeLevel;
}