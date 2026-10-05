using UnityEngine;

public class Throwable : MonoBehaviour
{
    private WeaponData weaponData;

    [Header("Lifespan Settings")]
    private float lifeSpan = 4f;
    private float timeAliveTimer = 0f;

    //This can be set to false with an upgrade, put this in initialize method for upgrade system
    private bool despawnOnHit = true; 

    public void Initialize(WeaponData data)
    {
        weaponData = data;
    }

    private void FixedUpdate()
    {
        timeAliveTimer += Time.fixedDeltaTime;
        if (timeAliveTimer >= lifeSpan)
        {
            timeAliveTimer = 0f;
            Despawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the collided object has the enemy interface
        if (collision.TryGetComponent<IDamageable>(out IDamageable damageableObject) && !collision.CompareTag("Player"))
        {
            //Deal damage on hit
            damageableObject.TakeDamage(weaponData.damage);

            if (despawnOnHit)
            {
                //Despawn if throwable hit something?! (With upgrade it can be set to not despawn on hit)
                Despawn();
            }
        }
    }

    private void Despawn()
    {
        //Despawn throwable object
        PoolManager.Instance.EnQueuePoolObject(this.gameObject);
    }
}