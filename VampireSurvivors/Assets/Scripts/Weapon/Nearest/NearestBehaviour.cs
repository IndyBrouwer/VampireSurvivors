using UnityEngine;

public class NearestBehaviour : MonoBehaviour, IWeaponBehaviour
{
    private WeaponData weaponData;

    [Header("Detection Settings")]
    [SerializeField] private float maxRadius = 10f;
    private GameObject closestTarget;
    private float closestDistance;

    [Header("Scritps")]
    private Weapon weaponScript;

    public void Initialize(WeaponData scriptableObject, Weapon weaponBrainScript)
    {
        //Setup vars
        weaponData = scriptableObject;
        weaponScript = weaponBrainScript;
    }    

    public void Fire()
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

    private void CreateBullet(Vector2 targetPosition)
    {
        //Spawn bullet
        GameObject newBullet = PoolManager.Instance.DeQueuePoolObject(weaponData.weaponID);
        newBullet.transform.position = transform.position;
        newBullet.transform.rotation = Quaternion.identity;

        //Initialize bullet (bullet prefab already has nearest script)
        newBullet.GetComponent<Nearest>().Initialize(weaponData, this);

        Rigidbody2D bulletRigidbody = newBullet.GetComponent<Rigidbody2D>();
        
        //Calculate direction from the bullet (spawn location) to the (closest) target
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        //Give direction and the movespeed from scriptable object
        bulletRigidbody.linearVelocity = direction * weaponData.moveSpeed;
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
}