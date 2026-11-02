using UnityEngine;

public class ThrowableBehaviour : MonoBehaviour, IWeaponBehaviour
{
    private WeaponData weaponData;

    private float damage;
    private float speed;
    private int projectileAmount = 1;
    private int maxHits;

    [SerializeField] private AudioClip[] sfx;

    [Header("Scripts")]
    private Weapon weaponScript;

    public void Initialize(WeaponData scriptableObject, Weapon weaponBrainScript)
    {
        weaponData = scriptableObject;
        weaponScript = weaponBrainScript;

        damage = weaponData.damage;
        speed = weaponData.moveSpeed;
    }

    public void Fire()
    {
        SpawnThrowable();
    }

    private void SpawnThrowable()
    {
        //Place this in for loop so we can easily spawn multiple at a time
        for (int index = 0; index < projectileAmount; index++)
        {
            //Spawn the throwable object at the weapon's position
            GameObject newThrowable = PoolManager.Instance.DeQueuePoolObject(weaponData.weaponID);
            newThrowable.transform.position = transform.position;
            newThrowable.transform.rotation = Quaternion.identity;

            Throwable throwableScript = newThrowable.GetComponent<Throwable>();

            //Initialize the throwable object with the weapon data
            throwableScript.Initialize(weaponData);
            throwableScript.SetValues(damage, maxHits);

            GiveVelocity(newThrowable);
        }

        PickSFX();
    }

    private void GiveVelocity(GameObject newThrowable)
    {
        //Give the throwable object a force in the direction the player is facing
        Rigidbody2D throwableRigidbody = newThrowable.GetComponent<Rigidbody2D>();

        //Random direction to simulate throwing, (Y should only be positive so the throwable object goes up
        Vector2 direction = new Vector2(Random.Range(-weaponData.radius[0], weaponData.radius[0]), Random.Range(150, 350));

        //Spawn flipped if thrown on the right side of the player, else spawn normal
        if (direction.x < 0)
        {
            newThrowable.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            newThrowable.GetComponent<SpriteRenderer>().flipX = true;
        }

        //Give direction and the movespeed from scriptable object
        throwableRigidbody.AddForce(direction * speed);
    }

    public void SetValues(float damageMultiplyer, float radiusMultiplyer, float speedMultiplyer, int projectileIncrease, int increaseHits)
    {
        if (damageMultiplyer != 0)
        {
            //Increase damage
            damage *= damageMultiplyer;
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

        if (increaseHits != 0)
        {
            //Increase enemies it can attack
            maxHits += increaseHits;
        }
    }

    private void PickSFX()
    {
        int pick = Random.Range(0, sfx.Length);

        AudioManager.Instance.sfxManager.PlaySFX(sfx[pick]);
    }
}