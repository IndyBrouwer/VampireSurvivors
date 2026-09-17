using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Identification")]
    public string enemyName;
    public Sprite sprite;

    [Header("Enemy Settings")]
    public float health;
    public float speed;
    public float damage;
    public float attackCooldown;

    [Header("Drop Settings")]
    public float dropRate;
    public DropData[] possibleDrops;
    public float xpAmount;
    public float goldAmount;
}