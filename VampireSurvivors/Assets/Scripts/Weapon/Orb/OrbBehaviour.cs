using System.Collections;
using UnityEngine;

public class OrbBehaviour : MonoBehaviour, IWeaponBehaviour
{
    private WeaponData weaponData;

    [Header("Game Objects")]
    [SerializeField] private GameObject orbWeapon;

    private Animator orbAnimator;

    [Header("Scripts")]
    private Weapon weaponScript;

    public void Initialize(WeaponData scriptableObject, Weapon weaponBrainScript)
    {
        weaponData = scriptableObject;
        weaponScript = weaponBrainScript;

        orbWeapon.GetComponent<VoidOrb>().Initialize(weaponData);
        orbAnimator = orbWeapon.GetComponent<Animator>();
    }

    public void Fire()
    {
        if (orbWeapon.activeSelf)
        {
            //Disable collider?!

            orbAnimator.SetTrigger("Despawn");

            //Wait for anim
            StartCoroutine(WaitForAnim());
        }
        else
        {
            //Spawning in
            orbWeapon.SetActive(!orbWeapon.activeSelf);
        }
    }

    private IEnumerator WaitForAnim()
    {
        //Wait for the length of orb_despawn anim
        yield return new WaitForSeconds(0.68f);
        
        //Despawn
        orbWeapon.SetActive(!orbWeapon.activeSelf);
    }

    //For increase in orbs for levels. Get a full circle, then devide it by the amount of orbs there are supposed to be to get their position.
}