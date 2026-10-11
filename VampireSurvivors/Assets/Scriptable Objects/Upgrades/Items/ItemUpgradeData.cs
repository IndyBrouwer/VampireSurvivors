using UnityEngine;

[CreateAssetMenu(fileName = "ItemUpgradeData", menuName = "Scriptable Objects/ItemUpgradeData")]
public class ItemUpgradeData : ScriptableObject
{
    //Upgrade description
    public string upgradeDescription;

    //Needs a max and min value
    [Range(1, 5)]
    public int upgradeLevel;
}
