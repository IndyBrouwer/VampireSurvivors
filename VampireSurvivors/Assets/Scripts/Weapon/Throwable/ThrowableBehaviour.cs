using UnityEngine;

public class ThrowableBehaviour : MonoBehaviour, IWeaponBehaviour
{
    private WeaponData weaponData;

    [Header("Scripts")]
    private Weapon weaponScript;

    public void Initialize(WeaponData scriptableObject, Weapon weaponBrainScript)
    {
        weaponData = scriptableObject;
        weaponScript = weaponBrainScript;
    }

    public void Fire()
    {
        SpawnThrowable();
    }

    private void SpawnThrowable()
    {
        //Spawn the throwable object at the weapon's position
        GameObject newThrowable = PoolManager.Instance.DeQueuePoolObject(weaponData.weaponID);
        newThrowable.transform.position = transform.position;
        newThrowable.transform.rotation = Quaternion.identity;

        //Initialize the throwable object with the weapon data
        newThrowable.GetComponent<Throwable>().Initialize(weaponData);

        GiveVelocity(newThrowable);
    }

    private void GiveVelocity(GameObject newThrowable)
    {
        //Give the throwable object a force in the direction the player is facing
        Rigidbody2D throwableRigidbody = newThrowable.GetComponent<Rigidbody2D>();

        //Random direction to simulate throwing, (Y should only be positive so the throwable object goes up
        Vector2 direction = new Vector2(Random.Range(-200, 200), Random.Range(150, 350));

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
        throwableRigidbody.AddForce(direction * weaponData.moveSpeed);
    }
}