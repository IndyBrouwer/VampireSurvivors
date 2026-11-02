using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour, IUpgradable
{
    [SerializeField] private WeaponData weaponData;
    private WeaponUpgradeData[] allUpgradeData;

    private IWeaponBehaviour weaponBehaviour;
    private float cooldown;
    private int currentLevel = 0;

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

        cooldown = weaponData.interval;

        allUpgradeData = weaponData.upgrades;
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

    public WeaponData GetWeaponData()
    {
        return weaponData;
    }

    public int GetWeaponLevel()
    {
        return currentLevel;
    }

    public bool CanUpgrade()
    {
        return currentLevel < weaponData.maxUprades;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void Upgrade()
    {
        if (CanUpgrade())
        {
            //Search for correct upgradeData based on currentlevel (BEFORE leveling up)
            WeaponUpgradeData currentUpgrade = allUpgradeData[currentLevel];

            //Apply upgrades
            weaponBehaviour.SetValues(currentUpgrade.powerMultiplier, currentUpgrade.radiusMultiplier, currentUpgrade.speedMultiplier, currentUpgrade.increaseAmount, currentUpgrade.increaseHits);

            //If upgrade has a cooldown reduction upgrade, apply upgrade
            if (currentUpgrade.cooldownReduction != 0)
            {
                cooldown -= currentUpgrade.cooldownReduction;
            }

            currentLevel++;
        }
    }
}