using UnityEngine;

[CreateAssetMenu(fileName = "DropData", menuName = "Scriptable Objects/DropData")]
public class DropData : ScriptableObject
{
    [Header("Drop Identification")]
    public string dropName;
    public DropType dropType;
    public string description;
    public Sprite sprite;

    public GameObject prefab;
    public float dropRate;
}

public enum DropType
{
    Health,
    Gold,
    XP
}