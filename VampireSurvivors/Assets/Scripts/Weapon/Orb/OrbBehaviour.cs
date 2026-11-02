using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBehaviour : MonoBehaviour, IWeaponBehaviour
{
    private WeaponData weaponData;

    [Header("Game Objects")]
    [SerializeField] private GameObject player;
    private List<GameObject> orbs = new List<GameObject>();
    private List<VoidOrb> orbScripts = new List<VoidOrb>();

    [Header("Upgrade Stats")]
    private float damage;
    private float speed;
    private float radius;
    private int projectileAmount = 1;

    private bool isOrbiting = false;

    [SerializeField] private AudioClip[] sfx;

    [Header("Scripts")]
    private Weapon weaponScript;

    public void Initialize(WeaponData scriptableObject, Weapon weaponBrainScript)
    {
        weaponData = scriptableObject;
        weaponScript = weaponBrainScript;

        damage = weaponData.damage;
        speed = weaponData.moveSpeed;
        radius = weaponData.radius[0];
    }

    private void SetupOrbs()
    {
        //Wipe orb and orbScripts list
        orbs.Clear();
        orbScripts.Clear();

        Vector2 center = new Vector2(player.transform.position.x, player.transform.position.y);

        for (int index = 0; index < projectileAmount; index++)
        {
            //Spawn orb in
            GameObject newOrb = PoolManager.Instance.DeQueuePoolObject(weaponData.weaponID);

            #region Decide position
            //Create a circle and devide it by the amount of projectiles that need to spawn, so that they are evenly spaced out
            float step = 2 * Mathf.PI / projectileAmount;
            float x = Mathf.Cos(step * index);
            float y = Mathf.Sin(step * index);

            Vector2 direction = new Vector2(x, y);

            Vector2 position = center + direction * radius;
            #endregion

            VoidOrb orbScript = newOrb.GetComponent<VoidOrb>();

            orbScript.Initialize(weaponData, player.transform, position);
            orbScript.SetValues(damage, speed, radius);

            //Add to list of active orbs
            orbs.Add(newOrb);

            //Add orb script to active scripts
            orbScripts.Add(orbScript);
        }
    }

    //For increase in orbs for levels. Get a full circle, then devide it by the amount of orbs there are supposed to be to get their position.
    public void Fire()
    {
        PickSFX();

        if (isOrbiting)
        {
            isOrbiting = false;

            foreach (VoidOrb orbScripts in orbScripts)
            {
                orbScripts.Despawn();
            }

            //Wait for anim
            StartCoroutine(WaitForAnim());
        }
        else
        {
            isOrbiting = true;

            //Spawning in
            SetupOrbs();
        }
    }

    private IEnumerator WaitForAnim()
    {
        //Wait for the length of orb_despawn anim
        yield return new WaitForSeconds(0.68f);

        foreach (GameObject orb in orbs)
        {
            PoolManager.Instance.EnQueuePoolObject(orb);
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
            //Increase radius (How close to player)
            radius *= radiusMultiplyer;
        }

        if (speedMultiplyer != 0)
        {
            speed *= speedMultiplyer;
        }

        if (projectileIncrease != 0)
        {
            //Increase projectile amount
            projectileAmount += projectileIncrease;
        }

        foreach (VoidOrb orb in orbScripts)
        {
            orb.SetValues(damage, speed, radius);
        }
    }

    private void PickSFX()
    {
        int randomPick = Random.Range(0, sfx.Length);

        AudioManager.Instance.sfxManager.PlaySFX(sfx[randomPick]);
    }
}