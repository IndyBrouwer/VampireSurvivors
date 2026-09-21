using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Identification")]
    public string weaponName;
    public Sprite sprite;

    [Header("Weapon Settings")]
    public float damage;
    public float attackCooldown;
    public float moveSpeed;
}