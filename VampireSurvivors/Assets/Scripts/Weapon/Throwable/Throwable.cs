using UnityEngine;

public class Throwable : MonoBehaviour
{
    private WeaponData weaponData;

    [Header("Lifespan Settings")]
    private float lifeSpan = 4f;
    private float timeAliveTimer = 0f;

    private float damage;

    //This can be set to a higher amount with an upgrade
    private int maxEnemyHits = 1;
    private int hitsLeft;

    public void Initialize(WeaponData data)
    {
        weaponData = data;
    }

    public void SetValues(float power, int maxHits)
    {
        damage = power;
        maxEnemyHits = maxHits;

        //Reset hitsLeft as we dont actually destroy weapons
        hitsLeft = maxEnemyHits;
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
            damageableObject.TakeDamage(damage);

            hitsLeft--;

            if (hitsLeft <= 0)
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