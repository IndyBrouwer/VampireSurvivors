using UnityEngine;

public class Nearest : MonoBehaviour
{
    private WeaponData weaponData;

    private Vector3 startPosition;
    private float maxDistance;

    [Header("Interfaces")]
    private IDamageable damageableObject;

    private NearestBehaviour nearestBehaviourScipt;

    public void Initialize(WeaponData scriptableObject, NearestBehaviour script)
    {
        weaponData = scriptableObject;

        nearestBehaviourScipt = script;
        maxDistance = nearestBehaviourScipt.GetMaxRadius();

        startPosition = transform.position;
    }

    //Despawn when maxradius reached from parent
    private void FixedUpdate()
    {
        //maxDistance * 1.1 so the bullet doesnt despawn on screen but also doesnt go much further than the max range of the weapon
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance * 1.1)
        {
            Despawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out damageableObject) && !collision.CompareTag("Player"))
        {
            Attack();
        }
    }

    //Deal damage to hit IDamageable if not player
    private void Attack()
    {
        //Deal damage on hit
        damageableObject.TakeDamage(weaponData.damage);

        Despawn();
    }

    private void Despawn()
    {
        //Despawn bullet
        PoolManager.Instance.EnQueuePoolObject(this.gameObject);
    }
}