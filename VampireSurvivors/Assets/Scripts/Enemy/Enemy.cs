using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IDroppable, ILevelable
{
    [SerializeField] private EnemyData enemyData;

    private float maxHealth;
    private float currentHealth;
    private float damage;
    private float xp;

    private bool isDead = false;

    [Header("Required Components")]
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Collider2D enemyCollider;

    private IEnemyBehaviour enemyBehaviour;
    private ICollectable collectable;
    private Dictionary<DropData, float> dropPairs = new Dictionary<DropData, float>();

    [SerializeField] private AudioClip[] hitSFX;

    [Header("Required Scripts")]
    private DamageEffect damageEffectScript;
    private KillCounter killCounterScript;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        damageEffectScript = GetComponent<DamageEffect>();

        SetupEnemy();
    }

    private void OnEnable()
    {
        //Reset sprite color when needed
        if (spriteRenderer.color == Color.red)
        {
            spriteRenderer.color = Color.white;
        }

        ResetEnemy();
    }

    public void InitializeEnemy(Transform playerPosition, KillCounter script)
    {
        if (TryGetComponent<IEnemyBehaviour>(out enemyBehaviour))
        {
            enemyBehaviour.Initialize(enemyData, playerPosition, animator);
        }
        else
        {
            Debug.LogWarning("EnemyBehaviour component not found on " + gameObject.name);
        }

        killCounterScript = script;
    }

    private void SetupEnemy()
    {
        gameObject.name = enemyData.enemyName;
        spriteRenderer.sprite = enemyData.sprite;

        maxHealth = enemyData.health;
        currentHealth = maxHealth;
    }

    public void LevelUp(int wave)
    {
        //With every new wave, increase Scriptable objects values by a certain percentage
        float multiplier = 1f + (wave * 0.2f);

        maxHealth = enemyData.health * multiplier;
        damage = enemyData.damage * multiplier;
        xp = enemyData.xpAmount * multiplier;

        Mathf.RoundToInt(xp);
    }

    private void Update()
    {
        //Call the move function from the enemy behaviour script
        enemyBehaviour.Move();

        enemyBehaviour.UpdateAnimation();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Attack player using IDamageable interface
        if (collision.collider.CompareTag("Player") && collision.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(enemyData.damage);
        }
    }

    public void TakeDamage(float damage)
    {
        //Damage vfx
        damageEffectScript.DamageColorEffect(GetComponent<SpriteRenderer>());

        PickSFX();

        currentHealth -= damage;

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;

            Die();
        }
    }

    private void PickSFX()
    {
        int randomPick = Random.Range(0, hitSFX.Length);

        AudioManager.Instance.sfxManager.PlaySFX(hitSFX[randomPick]);
    }

    public void Drop()
    {
        //Check if the enemy should drop an item based on its drop rate
        if (UnityEngine.Random.value > enemyData.dropRate)
        {
            return;
        }

        //Calculate the total drop rate of all possible drops
        float totalDropRate = 0f;

        foreach (DropData drop in enemyData.possibleDrops)
        {
            totalDropRate += drop.dropRate;
        }

        //Pick a drop based on the drop rates of each possible drop
        float randomValue = UnityEngine.Random.value * totalDropRate;

        foreach (DropData drop in enemyData.possibleDrops)
        {
            randomValue -= drop.dropRate;

            if (randomValue <= 0f)
            {
                DropItem(drop);
                return;
            }
        }
    }

    private void DropItem(DropData drop)
    {
        float amount = 0;

        switch (drop.dropType)
        {
            case DropType.XP:
                amount = enemyData.xpAmount;
                break;

            case DropType.Gold:
                amount = enemyData.goldAmount;
                break;

            case DropType.Health:
                amount = enemyData.healthAmount;
                break;
        }

        //Round amount to nearest whole number if not 0, so it can be an int, (especially important for gold).
        int intAmount = Mathf.RoundToInt(amount);
        if (intAmount <= 0)
        {
            //Set to 1 if the rounded amount is less than or equal to 0
            intAmount = 1; 
        }

        GameObject droppedItem = PoolManager.Instance.DeQueuePoolObject(drop.dropID);
        droppedItem.transform.position = this.transform.position;

        //Set amount of drop
        if (droppedItem.TryGetComponent<ICollectable>(out collectable))
        {
            collectable.Initialize(amount);
        }
    }

    public void Die()
    {
        enemyCollider.enabled = false;

        //Add kill to kill counter
        killCounterScript.IncreaseKillCount();

        //Death animation || explosion/poof effect
        animator.SetTrigger("Die");

        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        Drop();

        //Wait for the death animation to finish
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        //Set object inactive to reuse for another enemy
        PoolManager.Instance.EnQueuePoolObject(gameObject);
    }

    private void ResetEnemy()
    {
        isDead = false;
        currentHealth = maxHealth;

        enemyCollider.enabled = true;
    }
}