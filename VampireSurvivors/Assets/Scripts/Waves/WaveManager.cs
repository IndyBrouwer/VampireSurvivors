using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<WaveData> waves;

    //Create copy of waves list, name it currentwaves.
    //Wave spawning will loop through this list again and again to create endless waves while not needing to create a ton of waves by hand.

    private int currentWaveIndex = 0;


    private void Start()
    {
        WaveController();
    }

    public void WaveController()
    {
        foreach (WaveData wave in waves)
        {
            //Spawn the selected wave
            StartCoroutine(SpawnEnemy(wave));

            //Progress the waves
            currentWaveIndex++;

            if (currentWaveIndex >= waves.Count)
            {
                currentWaveIndex = 0;
            }
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
                PoolManager.Instance.DeQueuePoolObject(enemy.enemyData.enemyID);    
                
                //Set enemy spawn location

                //Wait for spawn delay
                yield return new WaitForSeconds(wave.spawnDelay);
            }
        }

        StartCoroutine(WaveDelay(wave));
    }

    private IEnumerator WaveDelay(WaveData wave)
    {
        yield return new WaitForSeconds(wave.waveDelay);
    }
}