using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Chunk Settings")]
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private ChunkData chunkData;
    [SerializeField] private int chunksAroundPlayer = 1;
    [SerializeField] private Vector2 ChunkSize = new Vector2(84, 70);

    [SerializeField] private Transform player;

    private Dictionary<Vector2Int, GameObject> activeTiles = new Dictionary<Vector2Int, GameObject>();

    private void Update()
    {
        Vector2 playerPos = player.position;

        //Decide on which chunk the player is currently on
        int playerTileX = Mathf.FloorToInt(playerPos.x / ChunkSize.x);
        int playerTileY = Mathf.FloorToInt(playerPos.y / ChunkSize.y);

        HashSet<Vector2Int> requiredTiles = new HashSet<Vector2Int>();

        for (int distanceX = -chunksAroundPlayer; distanceX <= chunksAroundPlayer; distanceX++)
        {
            for (int distanceY = -chunksAroundPlayer; distanceY <= chunksAroundPlayer; distanceY++)
            {
                int tileX = playerTileX + distanceX;
                int tileY = playerTileY + distanceY;
                Vector2Int coord = new(tileX, tileY);

                requiredTiles.Add(coord);

                //If the chunk is not active yet, get one from pool and activate it
                if (!activeTiles.ContainsKey(coord))
                {
                    //Add a check for as long as there are chunks in the pool
                    GameObject tile = PoolManager.Instance.DeQueuePoolObject(chunkData.chunkID);
                    tile.transform.position = new Vector3(tileX * ChunkSize.x, tileY * ChunkSize.y, 0f);
                    tile.SetActive(true);
                    activeTiles[coord] = tile;
                }
                else
                {
                    //If a chunk exists make sure it is enabled
                    activeTiles[coord].SetActive(true);
                }
            }
        }

        //Search all tiles that are no longer needed, disable them and put them back in the pool
        List<Vector2Int> toRemove = new();

        foreach (var pair in activeTiles)
        {
            if (!requiredTiles.Contains(pair.Key))
            {
                GameObject tile = pair.Value;

                tile.SetActive(false);

                //Put chunk back in the pool
                PoolManager.Instance.EnQueuePoolObject(tile);

                //Mark to remove from active list
                toRemove.Add(pair.Key);
            }
        }

        //Remove unused chunks from active tiles
        foreach (Vector2Int coord in toRemove)
        {
            activeTiles.Remove(coord);
        }
    }
}
