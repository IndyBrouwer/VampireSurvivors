using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Identification")]
    public string weaponName;
    public string weaponID;
    public Sprite sprite;
    public string description;

    [Header("Weapon Settings")]
    public float damage;
    public float interval;
    public float[] radius;
    public float moveSpeed;
    public GameObject projectile;

    [Header("Upgrade Settings")]
    [Range(0, 5)]
    public int maxUprades;
    public WeaponUpgradeData[] upgrades;
}