using UnityEngine;

/// <summary>
/// This script is for the enemies that surround the player together. This script is placed on the tombstone enemy prefab.
/// </summary>
public class TombstoneBehaviour : MonoBehaviour, IEnemyBehaviour
{
    [Header("Enemy Basics")]
    private EnemyData enemyData;
    private Transform playerPosition;

    [Header("Enemy Stats")]
    private float speed;

    private float lifeSpan = 20f; //The lifespan of the tombstone enemy in seconds
    private float timeAlive;

    [Header("Required Components")]
    private Rigidbody2D rb;

    public void Initialize(EnemyData scriptableObject, Transform playerPos)
    {
        enemyData = scriptableObject;
        playerPosition = playerPos;

        speed = enemyData.speed;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= lifeSpan)
        {
            Despawn();
        }
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

    private void Despawn()
    {
        //Despawn enemy instead of it dying, so it doesnt count as kill and wont drop an item.

        PoolManager.Instance.EnQueuePoolObject(gameObject);
    }
}