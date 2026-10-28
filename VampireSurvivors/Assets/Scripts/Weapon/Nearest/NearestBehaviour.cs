using UnityEngine;

public class NearestBehaviour : MonoBehaviour, IWeaponBehaviour
{
    private WeaponData weaponData;

    [Header("Detection Settings")]
    [SerializeField] private float maxRadius;
    private GameObject closestTarget;
    private float closestDistance;

    [Header("Upgrade Stats")]
    private float damage;
    private float speed;
    private int projectileAmount = 1;

    [Header("Scritps")]
    private Weapon weaponScript;

    public void Initialize(WeaponData scriptableObject, Weapon weaponBrainScript)
    {
        //Setup vars
        weaponData = scriptableObject;
        weaponScript = weaponBrainScript;

        maxRadius = weaponData.radius[0];
        damage = weaponData.damage;
        speed = weaponData.moveSpeed;
    }

    public void Fire()
    {
        //Create a loop so we can fire multiple bullets (different targets!)
        for (int index = 0; index < projectileAmount; index++)
        {
            //Reset closestDistance var and closesthit var every time a new cycle starts for this weapon
            closestDistance = maxRadius;
            closestTarget = null;

            //Draw max range circle
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, maxRadius);

            //Check for enemies within circle
            foreach (Collider2D hit in hits)
            {
                IDamageable damageable = hit.GetComponent<IDamageable>();

                if (damageable == null || hit.CompareTag("Player"))
                {
                    //If object is no damageable object or is player, skip this object
                    continue;
                }

                float distance = Vector3.Distance(transform.position, hit.transform.position);

                //If theres an enemy hit, check for the enemy closest to the player
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = hit.gameObject;
                }
            }

            //Spawn bullet and send in that direction
            if (closestTarget != null)
            {
                CreateBullet(closestTarget.transform.position);
            }
        }
    }

    private void CreateBullet(Vector2 targetPosition)
    {
        //Spawn bullet
        GameObject newBullet = PoolManager.Instance.DeQueuePoolObject(weaponData.weaponID);
        newBullet.transform.position = transform.position;
        newBullet.transform.rotation = Quaternion.identity;

        //Initialize bullet (bullet prefab already has nearest script)
        Nearest projectileScript = newBullet.GetComponent<Nearest>();
        projectileScript.Initialize(weaponData, this);
        projectileScript.SetValues(damage, maxRadius);

        Rigidbody2D bulletRigidbody = newBullet.GetComponent<Rigidbody2D>();
        
        //Calculate direction from the bullet (spawn location) to the (closest) target
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        //Give direction and the movespeed from scriptable object
        bulletRigidbody.linearVelocity = direction * speed;
    }

    public float GetMaxRadius()
    {
        return maxRadius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRadius);
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
            //Inrease max radius
            maxRadius *= radiusMultiplyer;
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
    }
}