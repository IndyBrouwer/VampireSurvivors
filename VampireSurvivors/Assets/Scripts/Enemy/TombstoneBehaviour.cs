using UnityEngine;

public class TombstoneBehaviour : MonoBehaviour, IEnemyBehaviour
{
    //This script is for the enemies that surround the player together
    private EnemyData enemyData;

    private void Awake()
    {
        //Search for player pos
    }

    public void Initialize(EnemyData scriptableObject)
    {
        enemyData = scriptableObject;
    }

    public void Move()
    {
        //Move towards player?
    }
}