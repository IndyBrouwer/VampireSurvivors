using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IEnemyBehaviour
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
        //Move towards player?
    }
}
