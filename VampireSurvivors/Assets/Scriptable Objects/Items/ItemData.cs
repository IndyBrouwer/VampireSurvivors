using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Item Identification")]
    public string itemName;
    public string itemID;
    public Sprite sprite;

    public string description;
}