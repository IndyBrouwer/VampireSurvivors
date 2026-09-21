using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{
    public List<EnemyWaveData> enemies = new List<EnemyWaveData>();

    public float spawnDelay = 0.5f;
    public float waveDelay = 3f;
}