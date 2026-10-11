using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponSlots;
    [SerializeField] private GameObject[] itemSlots;

    public void AddWeapon(WeaponData weapon)
    {
        SetImage(weaponSlots, weapon.sprite);
    }

    public void AddItem(ItemData item)
    {
        SetImage(itemSlots, item.sprite);
    }

    public void SetImage(GameObject[] list, Sprite sprite)
    {
        foreach (GameObject slot in weaponSlots)
        {
            foreach (Image slotImage in slot.GetComponentsInChildren<Image>())
            {
                //If a slot with no image is found, place newly aquired weapon there
                if (slotImage.sprite == null)
                {
                    //Set slot sprite to weapon sprite
                    slotImage.sprite = sprite;

                    //Adjust alpha so image will be visible
                    Color slotColor = slotImage.color;
                    slotColor.a = 1f;
                    slotImage.color = slotColor;

                    //Return instead of break so it exits all loops, (exits setimage completely)
                    return;
                }
            }
        }
    }
}
