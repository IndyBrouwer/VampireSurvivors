using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponSlots;
    [SerializeField] private GameObject[] itemSlots;

    public void AddWeapon(WeaponData weapon)
    {
        foreach (GameObject slot in weaponSlots)
        {
            Image slotImage = slot.GetComponentInChildren<Image>();

            //If a slot with no image is found, place newly aquired weapon there
            if (slotImage.sprite == null)
            {
                //Set slot sprite to weapon sprite
                slotImage.sprite = weapon.sprite;

                //Get color component so we can adjust alpha
                Color slotColor = slotImage.GetComponent<Color>();

                //Set alpha of sprite to 1
                slotColor.a = 1;
            }
        }
    }

    public void AddItem(ItemData item)
    {
        foreach (GameObject slot in itemSlots)
        {
            Image slotImage = slot.GetComponentInChildren<Image>();

            //If a slot with no image is found, place newly aquired weapon there
            if (slotImage.sprite == null)
            {
                //Set slot sprite to item sprite
                slotImage.sprite = item.sprite;

                //Get color component so we can adjust alpha
                Color slotColor = slotImage.GetComponent<Color>();

                //Set alpha of sprite to 1
                slotColor.a = 1;
            }
        }
    }
}
