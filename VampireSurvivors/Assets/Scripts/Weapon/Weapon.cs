using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    private IWeaponBehaviour weaponBehaviour;
    private float cooldown;
    private int currentLevel;

    private void Awake()
    {
        if (TryGetComponent<IWeaponBehaviour>(out weaponBehaviour))
        {
            weaponBehaviour.Initialize(weaponData, this);
        }
        else
        {
            Debug.LogWarning("IWeaponBehaviour component not found on " + gameObject.name);
        }
    }

    //In update with time.deltatime as this is more accurate for small time frames like this compared to IEnumerators
    private void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            weaponBehaviour.Fire();

            //Maybe will make this cooldown += weaponData.interval; later.
            cooldown = weaponData.interval;
        }
    }

    public int GetWeaponLevel()
    {
        return currentLevel;
    }
}