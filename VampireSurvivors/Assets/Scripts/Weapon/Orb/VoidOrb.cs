using UnityEngine;

public class VoidOrb : MonoBehaviour
{
    private WeaponData weaponData;
    private Transform player;

    [Header("Weapon Stats")]
    private float damage;
    private float speed;
    private float distance;

    private float angle;

    private bool isDespawning;

    [Header("Components")]
    private Animator orbAnimator;
    private Collider2D orbCollider;

    [Header("Interfaces")]
    private IDamageable damageableObject;

    public void Initialize(WeaponData scriptableObject, Transform playerTransform, Vector2 position)
    {
        weaponData = scriptableObject;
        player = playerTransform;
        this.transform.position = position;

        orbAnimator = GetComponent<Animator>();
        orbCollider = GetComponent<Collider2D>();

        orbCollider.enabled = true;

        isDespawning = false;
        orbAnimator.ResetTrigger("Despawn");

        Vector2 offset = position - (Vector2)player.position;

        //Calculate where on the circle around the player the orb needs to be
        //Mathf.Atan2() returns the angle in radians, not degrees.
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        transform.position = position;
    }

    public void SetValues(float power, float moveSpeed, float radius)
    {
        damage = power;
        speed = moveSpeed;
        distance = radius;
    }

    private void FixedUpdate()
    {
        if (isDespawning)
        {
            return;
        }

        //Rotate according to the weapon's upgraded speed.
        angle += speed * Time.fixedDeltaTime;

        //Keep the angle within a manageable range.
        angle = Mathf.Repeat(angle, 360f);

        UpdateOrbitPosition();
    }

    private void UpdateOrbitPosition()
    {
        //Convert the orbs angle into the format Unity needs for sin and cos functions
        //Degrees to radians
        float radians = angle * Mathf.Deg2Rad;

        Vector2 offset = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * distance;

        transform.position = (Vector2)player.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDespawning)
        {
            return;
        }

        if (collision.TryGetComponent<IDamageable>(out damageableObject))
        {
            Attack();
        }
    }

    public void Attack()
    {
        damageableObject.TakeDamage(damage);
    }

    public void Despawn()
    {
        if (isDespawning)
        {
            return;
        }

        //Disable collider?!
        orbCollider.enabled = false;

        orbAnimator.SetTrigger("Despawn");
    }
}