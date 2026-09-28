using UnityEngine;

public class VoidOrb : MonoBehaviour
{
    private WeaponData weaponData;
    [SerializeField] private GameObject player;

    [Header("Orbit Settings")]
    [SerializeField] private float orbitOffset = 2f;

    [Header("Interfaces")]
    private IDamageable damageableObject;

    private void Start()
    {
        this.transform.position = player.transform.position + new Vector3(orbitOffset, 0, 0);
    }

    public void Initialize(WeaponData scriptableObject)
    {
        weaponData = scriptableObject;
    }

    private void FixedUpdate()
    {
        transform.RotateAround(player.transform.position, Vector3.forward, weaponData.moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out damageableObject) && !collision.CompareTag("Player"))
        {
            Attack();
        }
        else
        {
            Debug.LogWarning("IDamageable component not found on " + gameObject.name);
        }
    }

    public void Attack()
    {
        damageableObject.TakeDamage(weaponData.damage);
    }
}