using UnityEngine;

public class WalkerBehaviour : MonoBehaviour, IEnemyBehaviour
{
    private EnemyData enemyData;
    private float speed;

    public void Initialize(EnemyData scriptableObject)
    {
        enemyData = scriptableObject;

        speed = enemyData.speed;
    }

    public void Move()
    {
        //Search for player pos


        //Move towards player?

    }
}