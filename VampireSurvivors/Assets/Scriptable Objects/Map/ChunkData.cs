using UnityEngine;

[CreateAssetMenu(fileName = "ChunkData", menuName = "Scriptable Objects/ChunkData")]
public class ChunkData : ScriptableObject
{
    public string chunkName;
    public string chunkID;
    public GameObject prefab;
}
