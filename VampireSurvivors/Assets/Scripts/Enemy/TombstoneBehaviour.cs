using System.Collections;
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
    private bool isDespawning;

    private Vector2 direction;
    private Vector2 lastMoveDirection;

    [Header("Required Components")]
    private Rigidbody2D rb;
    private Animator animator;

    public void Initialize(EnemyData scriptableObject, Transform playerPos, Animator anim)
    {
        enemyData = scriptableObject;
        playerPosition = playerPos;

        speed = enemyData.speed;
        animator = anim;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= lifeSpan && !isDespawning)
        {
            Despawn();
        }

        UpdateAnimation();
    }

    //Move is already being called by enemy script
    public void Move()
    {
        if (isDespawning)
        {
            return;
        }

        //Calculate direction towards player
        direction = (playerPosition.position - transform.position).normalized;

        if ((direction.x == 0 && direction.y == 0) && (direction.x != 0 || direction.y != 0))
        {
            lastMoveDirection = direction;
        }

        direction.Normalize();

        //Move towards player
        rb.linearVelocity = new Vector2(direction.x, direction.y) * speed;
    }

    //Already being called by enemy script
    public void UpdateAnimation()
    {
        //Update required anim vars
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);

        animator.SetFloat("MoveMagnitude", direction.magnitude);

        animator.SetFloat("LastMoveX", lastMoveDirection.x);
        animator.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    private void Despawn()
    {
        isDespawning = true;

        animator.SetTrigger("Die");

        //Stop enemy from moving
        rb.linearVelocity = new Vector2(0f, 0f);

        StartCoroutine(WaitDeathAnim());
    }

    private IEnumerator WaitDeathAnim()
    {
        //Could swap the one second out for animation duration
        yield return new WaitForSeconds(1f);

        timeAlive = 0;

        isDespawning = false;

        //Just despawn enemy instead of it actually dying, so it doesnt count as kill and wont drop an item.
        PoolManager.Instance.EnQueuePoolObject(gameObject);
    }
}