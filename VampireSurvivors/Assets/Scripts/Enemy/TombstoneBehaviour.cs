using UnityEngine;

public class TombstoneBehaviour : MonoBehaviour, IEnemyBehaviour
{
    //This script is for the enemies that surround the player together

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

    public void Move()
    {
        //Calculate direction towards player
        Vector2 direction = (playerPosition.position - transform.position).normalized;

        //Move towards player
        rb.linearVelocity = new Vector2(direction.x, direction.y) * speed;
    }
}