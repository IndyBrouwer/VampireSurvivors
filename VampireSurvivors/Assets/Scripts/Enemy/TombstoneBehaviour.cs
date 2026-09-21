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
            spriteRenderer.flipY = true;
        }
    }
}