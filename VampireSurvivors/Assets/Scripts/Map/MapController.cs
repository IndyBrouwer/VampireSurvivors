using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour, ISpawnable, IDespawnable
{
    [Header("Chunk Settings")]
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private Vector2 ChunkSize = new Vector2(84, 70);
    [SerializeField] private bool deleteHiddenChunks = false;

    private Dictionary<Vector2Int, GameObject> generatedChunks = new Dictionary<Vector2Int, GameObject>();

    public void Spawn()
    {
        throw new NotImplementedException();
    }

    public void Despawn()
    {
        throw new NotImplementedException();
    }
}