using UnityEngine;

public class WalkerBehaviour : MonoBehaviour, IEnemyBehaviour
{
    [Header("Enemy Basics")]
    private EnemyData enemyData;
    public Transform playerPosition;

    [Header("Enemy Stats")]
    private float speed;

    [Header("Required Components")]
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public void Initialize(EnemyData scriptableObject, Transform playerPos, Animator enemyAnimator)
    {
        enemyData = scriptableObject;

        playerPosition = playerPos;
        speed = enemyData.speed;

        animator = enemyAnimator;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Move is already being called by enemy script
    public void Move()
    {
        //Calculate direction towards player
        Vector2 direction = (playerPosition.position - transform.position).normalized;

        //Move towards player
        rb.linearVelocity = new Vector2(direction.x, direction.y) * speed;
    }

    //Already being called by enemy script
    public void UpdateAnimation()
    {
        if (rb.linearVelocity.x > 0)
        {
            //Look Right
            spriteRenderer.flipX = false;
        }
        else if (rb.linearVelocity.x < 0)
        {
            //Look Left
            spriteRenderer.flipX = true;
        }
    }
}