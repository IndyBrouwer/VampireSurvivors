using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private WeaponData weaponData;

    [Header("Ring Radius")]
    [SerializeField] private float smallestRadius;
    [SerializeField] private float middleRadius;
    [SerializeField] private float biggestRadius;

    [Header("Interfaces")]
    private IDamageable damageableObject;

    private ShockwaveBehaviour shockwaveBehaviourScript;

    public void Initialize(WeaponData scriptableObject, ShockwaveBehaviour script)
    {
        //Setup vars
        weaponData = scriptableObject;
        shockwaveBehaviourScript = script;
    }

    private void FixedUpdate()
    {
        //Is shockwave is not active (either literally disabled or in the middle of activity switch anim) return to avoid dealing damage.
        if (!shockwaveBehaviourScript.IsShockwaveActive())
        {
            return;
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, biggestRadius);

        foreach (Collider2D hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();

            if (damageable == null || hit.CompareTag("Player"))
            {
                //If object is no damageable object or is player, skip this object
                continue;
            }

            float distance = Vector3.Distance(transform.position, hit.transform.position);

            float damage = GetDamageForDistance(distance);
            Debug.Log("Enemy will receive: " + damage + " damage from shockwave.");

            if (damage > 0)
            {
                damageable.TakeDamage(damage);
            }
        }
    }

    private float GetDamageForDistance(float distance)
    {
        if (distance <= smallestRadius)
        {
            //100% of damage
            return weaponData.damage;
        }
        else if (distance <= middleRadius)
        {
            //75% of damage
            return weaponData.damage * 0.75f;
        }
        else if (distance <= biggestRadius)
        {
            //50% of damage
            return weaponData.damage * 0.5f;
        }

        return 0f;
    }

    public void Attack()
    {
        damageableObject.TakeDamage(weaponData.damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(transform.position, smallestRadius);
        Gizmos.DrawWireSphere(transform.position, middleRadius);
        Gizmos.DrawWireSphere(transform.position, biggestRadius);
    }
}
