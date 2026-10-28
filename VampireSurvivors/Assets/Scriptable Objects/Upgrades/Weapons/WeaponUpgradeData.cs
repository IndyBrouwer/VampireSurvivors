using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUpgradeData", menuName = "Scriptable Objects/WeaponUpgradeData")]
public class WeaponUpgradeData : ScriptableObject
{
    //Upgrade description
    public string upgradeDescription;

    //The level of this upgrade
    [Range(1, 5)]
    public int upgradeLevel;

    [Header("Weapon upgrade values")]
    public float powerMultiplier;
    public float radiusMultiplier;
    public float speedMultiplier;
    public float cooldownReduction;

    //Amount of weapons
    public int increaseAmount;

    //Increase enemies it can attack
    public int increaseHits;
}