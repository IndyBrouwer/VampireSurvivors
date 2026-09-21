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

    public void Initialize(EnemyData scriptableObject, Transform playerPos)
    {
        enemyData = scriptableObject;

        playerPosition = playerPos;
        speed = enemyData.speed;

        rb = GetComponent<Rigidbody2D>();
    }

    //Move is already being called by enemy script
    public void Move()
    {
        //Calculate direction towards player
        Vector2 direction = (playerPosition.position - transform.position).normalized;

        //rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        //Move towards player
        rb.linearVelocity = new Vector2(direction.x, direction.y) * speed;
    }

    //Already being called by enemy script
    public void UpdateAnimation(SpriteRenderer spriteRenderer)
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