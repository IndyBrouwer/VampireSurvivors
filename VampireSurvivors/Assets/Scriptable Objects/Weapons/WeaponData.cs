using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Identification")]
    public string weaponName;
    public string weaponID;
    public Sprite sprite;

    [Header("Weapon Settings")]
    public float damage;
    public float interval;
    public float moveSpeed;
    public GameObject projectile;
}