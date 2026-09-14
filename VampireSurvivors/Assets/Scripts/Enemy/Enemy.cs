using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour, IDamageable, IDroppable
{
    [SerializeField] private EnemyData enemyData;

    private float maxHealth;
    private float currentHealth;

    private bool isDead = false;

    [Header("Needed Components")]
    private Sprite sprite;

    private IEnemyBehaviour enemyBehaviour;

    

    private void Awake()
    {
        if (TryGetComponent<IEnemyBehaviour>(out enemyBehaviour))
        {
            enemyBehaviour.Initialize(enemyData);
        }
        else
        {
            Debug.LogWarning("EnemyBehaviour component not found on " + gameObject.name);
        }

        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void Start()
    {
        maxHealth = enemyData.health;

        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TakeDamage(10f);
        }
    }

    public void TakeDamage(float damage)
    {
        //Damage animation

        currentHealth -= damage;

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    public void Drop()
    {
        //Decide if enemy will drop an item or not

        //Decide if it will be xp or gold

        //Drop decided item with their amount
    }

    public void Die()
    {
        //Death animation

        Drop();

        Destroy(gameObject);
    }
}