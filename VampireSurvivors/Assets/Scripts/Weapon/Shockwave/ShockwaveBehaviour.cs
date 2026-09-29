using System.Collections;
using UnityEngine;

public class ShockwaveBehaviour : MonoBehaviour, IWeaponBehaviour
{
    private WeaponData weaponData;

    [Header("Game Objects")]
    [SerializeField] private GameObject shockwaveWeapon;

    private float timeActive = 0.5f;
    private float timer = 0;

    private bool shockwaveActive = false;

    private Animator shockwaveAnimator;

    private Weapon weaponScript;

    public void Initialize(WeaponData scriptableObject, Weapon weaponBrainScript)
    {
        //Setup vars
        weaponData = scriptableObject;
        weaponScript = weaponBrainScript;

        shockwaveWeapon.GetComponent<Shockwave>().Initialize(weaponData, this);
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
}