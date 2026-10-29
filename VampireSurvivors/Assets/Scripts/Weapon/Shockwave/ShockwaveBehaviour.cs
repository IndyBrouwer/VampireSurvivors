using System.Collections;
using UnityEngine;

public class ShockwaveBehaviour : MonoBehaviour, IWeaponBehaviour
{
    private WeaponData weaponData;

    [Header("Game Objects")]
    [SerializeField] private GameObject shockwaveWeapon;

    [Header("Shockwave Pulse Settings")]
    private float timeActive = 0.5f;
    private float timer = 0;
    private bool shockwaveActive = false;

    [Header("Ring Radius")]
    [SerializeField] private float smallestRadius = 1f;
    [SerializeField] private float middleRadius = 1.5f;
    [SerializeField] private float biggestRadius = 2f;
    private float sizeMultiplyer;

    [Header("Upgradable Stats")]
    private float damage;

    [Header("Components")]
    private Animator shockwaveAnimator;

    [Header("Scripts")]
    private Weapon weaponScript;
    private Shockwave shockwaveScript;

    public void Initialize(WeaponData scriptableObject, Weapon weaponBrainScript)
    {
        //Setup vars
        weaponData = scriptableObject;
        weaponScript = weaponBrainScript;

        damage = weaponData.damage;

        //Set radius from rings
        smallestRadius = weaponData.radius[0];
        middleRadius = weaponData.radius[1];
        biggestRadius = weaponData.radius[2];

        //Setup Shockwave script
        shockwaveScript = shockwaveWeapon.GetComponent<Shockwave>();
        shockwaveScript.Initialize(weaponData, this);
        shockwaveScript.SetValues(damage, smallestRadius, middleRadius, biggestRadius, sizeMultiplyer);

        shockwaveAnimator = shockwaveWeapon.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!shockwaveActive)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= timeActive)
        {
            shockwaveActive = false;
            timer = 0;

            shockwaveAnimator.SetTrigger("Deactivate");

            StartCoroutine(WaitForAnim());
        }
    }

    public bool IsShockwaveActive()
    {
        return shockwaveActive;
    }

    public void Fire()
    {
        StartCoroutine(WaitForAnim());
    }

    private IEnumerator WaitForAnim()
    {
        if (!shockwaveWeapon.activeSelf)
        {
            shockwaveWeapon.SetActive(true);

            yield return new WaitForSeconds(1f);

            shockwaveActive = true;
        }
        else
        {
            yield return new WaitForSeconds(1f);

            shockwaveWeapon.SetActive(false);
        }        
    }

    public void SetValues(float damageMultiplyer, float radiusMultiplyer, float speedMultiplyer, int projectileIncrease, int increaseHits)
    {
        if (damageMultiplyer != 0)
        {
            //Increase damage
            damage *= damageMultiplyer;
        }

        if (radiusMultiplyer != 0)
        {
            //Increase size
            smallestRadius *= radiusMultiplyer;
            middleRadius *= radiusMultiplyer;
            biggestRadius *= radiusMultiplyer;

            sizeMultiplyer = radiusMultiplyer;
        }
        else
        {
            sizeMultiplyer = 0;
        }

        shockwaveScript.SetValues(damage, smallestRadius, middleRadius, biggestRadius, sizeMultiplyer);
    }
}