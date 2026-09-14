using UnityEngine;

[CreateAssetMenu(fileName = "DropData", menuName = "Scriptable Objects/DropData")]
public class DropData : ScriptableObject
{
    [Header("Drop Identification")]
    public string dropName;
    public string description;
    public Sprite sprite;
}