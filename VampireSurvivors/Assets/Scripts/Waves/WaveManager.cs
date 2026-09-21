using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<WaveData> waves;

    //Create copy of waves list, name it currentwaves.
    //Wave spawning will loop through this list again and again to create endless waves while not needing to create a ton of waves by hand.

    private int currentWaveIndex = 0;

    [Header("Enemy setup")]
    [SerializeField] private Transform playerPosition;

    private void Start()
    {
        foreach (WaveData wave in waves)
        {
            StartCoroutine(SpawnEnemy(wave));
        }
    }

    private IEnumerator SpawnEnemy(WaveData wave)
    {
        foreach (EnemyWaveData enemy in wave.enemies)
        {
            Debug.Log("Enemy Type: " + enemy.enemyData + "." + "Amount of enemies: " + enemy.amount);

            for (int index = 0; index < enemy.amount; index++)
            {
                //Spawn enemy
                GameObject spawnedEnemy = Instantiate(enemy.enemyData.prefab);

                //Setup enemy
                Enemy enemyScript = spawnedEnemy.GetComponent<Enemy>();
                enemyScript.InitializeEnemy(playerPosition);

                //Wait for spawn delay
                yield return new WaitForSeconds(wave.spawnDelay);
            }
        }
    }
}