using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IDroppable, ILevelable
{
    [SerializeField] private EnemyData enemyData;
    private Transform playerPosition;

    private float maxHealth;
    private float currentHealth;

    private bool isDead = false;

    [Header("Required Components")]
    private Sprite sprite;
    private Animator animator;

    private IEnemyBehaviour enemyBehaviour;
    private Dictionary<DropData, float> dropPairs = new Dictionary<DropData, float>();

    [Header("Required Scripts")]
    private DamageEffect damageEffectScript;

    private void Awake()
    {
        if (TryGetComponent<IEnemyBehaviour>(out enemyBehaviour))
        {
            enemyBehaviour.Initialize(enemyData, playerPosition);
        }
        else
        {
            Debug.LogWarning("EnemyBehaviour component not found on " + gameObject.name);
        }

        sprite = GetComponent<SpriteRenderer>().sprite;
        animator = GetComponent<Animator>();
        damageEffectScript = GetComponent<DamageEffect>();

        SetupEnemy();
    }

    private void SetupEnemy()
    {
        gameObject.name = enemyData.enemyName;
        sprite = enemyData.sprite;

        maxHealth = enemyData.health;
        currentHealth = maxHealth;
    }

    public void LevelUp()
    {
        //With every new wave, increase Scriptable objects values by a certain percentage
    }

    public void SetValues()
    {
        //Change local vars
    }

    private void Update()
    {
        //Call the move function from the enemy behaviour script
        enemyBehaviour.Move();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Attack player using IDamageable interface
        if (collision.CompareTag("Player") && collision.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(enemyData.damage);
        }
    }

    public void TakeDamage(float damage)
    {
        //Damage vfx
        damageEffectScript.DamageColorEffect(GetComponent<SpriteRenderer>());

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
        if (Random.value <= enemyData.dropRate)
        {
            foreach (DropData drop in enemyData.possibleDrops)
            {
                if (DropType.XP == drop.dropType)
                {
                    dropPairs.Add(drop, enemyData.xpAmount);
                }
                else if (DropType.Gold == drop.dropType)
                {
                    dropPairs.Add(drop, enemyData.goldAmount);
                }
            }

            //Decide if it will be xp or gold

            //Drop decided item with their amount

            //Instantiate the item prefab at the enemy's position
            //Instantiate(drop.prefab, transform.position, Quaternion.identity);
        }
    }

    public void Die()
    {
        //Death animation || explosion/poof effect
        animator.SetTrigger("Die");

        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        //Wait for the death animation to finish
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Drop();

        //Destroy the enemy game object
        Destroy(gameObject);
    }
}