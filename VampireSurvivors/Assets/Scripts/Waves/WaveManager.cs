using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private List<WaveData> waves;
    [SerializeField] private Transform player;

    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    [Header("Scripts")]
    private EnemySpawnPosition spawnPositionScript;


    private void Start()
    {
        spawnPositionScript = GetComponent<EnemySpawnPosition>();
    }

    private void FixedUpdate()
    {
        if (isSpawning == false)
        {
            StartCoroutine(WaveController());
        }
    }

    public IEnumerator WaveController()
    {
        isSpawning = true;

        Debug.Log("Current Wave: " + currentWaveIndex);

        //Spawn the selected wave
        yield return StartCoroutine(SpawnEnemy(waves[currentWaveIndex]));

        //Progress the waves
        currentWaveIndex++;

        if (currentWaveIndex >= waves.Count)
        {
            currentWaveIndex = 0;
        }

        isSpawning = false;
    }

    private IEnumerator SpawnEnemy(WaveData wave)
    {
        foreach (EnemyWaveData enemy in wave.enemies)
        {
            Debug.Log("Enemy Type: " + enemy.enemyData + "." + "Amount of enemies: " + enemy.amount);

            for (int index = 0; index < enemy.amount; index++)
            {
                //Get the current center position
                Vector2 center = new Vector2(player.position.x, player.position.y);

                //If tombstone enemy, call circlespawner function. Else spawn enemy at random position
                if (enemy.enemyData.enemyID == "enemy_tombstone")
                {
                    Vector2 spawnPosition = spawnPositionScript.CircleSpawner(center, enemy.amount, index);

                    GameObject newEnemy = PoolManager.Instance.DeQueuePoolObject(enemy.enemyData.enemyID);
                    newEnemy.transform.position = spawnPosition;
                    newEnemy.transform.rotation = Quaternion.identity;
                }
                else
                {
                    Vector2 spawPosition = spawnPositionScript.GetSpawnPosition(center);

                    //Spawn enemy
                    GameObject newEnemy = PoolManager.Instance.DeQueuePoolObject(enemy.enemyData.enemyID);
                    newEnemy.transform.position = spawPosition;
                    newEnemy.transform.rotation = Quaternion.identity;

                    //Wait for spawn delay
                    yield return new WaitForSeconds(wave.spawnDelay);
                }
            }
        }

        //Wait for wave spawning delay before starting the next wave
        yield return new WaitForSeconds(wave.waveDelay);
    }
}