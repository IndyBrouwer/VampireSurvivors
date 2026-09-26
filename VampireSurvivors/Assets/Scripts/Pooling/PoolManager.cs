using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        [Header("Pool Object")]
        public string objectName;
        public string objectID;
        public GameObject prefab;
        public int amount; //Amount to spawn when the game starts to constantly use for managing the waves
    }

    public static PoolManager Instance;

    public List<ObjectPoolItem> poolObjects = new List<ObjectPoolItem>();
    public Dictionary<string, Queue<GameObject>> objectsDictionary;

    [Header("Enemy setup")]
    [SerializeField] private Transform player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        objectsDictionary = new Dictionary<string, Queue<GameObject>>();

        //Spawn all enemies in poolItems with for each loop
        foreach (ObjectPoolItem item in poolObjects)
        {
            Queue<GameObject> objectsPool = new Queue<GameObject>();

            for (int index = 0; index < item.amount; index++)
            {
                GameObject poolObject = Instantiate(item.prefab, transform);

                //Send player pos to enemy scripts
                if (poolObject.TryGetComponent<Enemy>(out Enemy enemyScript))
                {
                    enemyScript.InitializeEnemy(player);
                }

                //Set object active false
                poolObject.SetActive(false);

                objectsPool.Enqueue(poolObject);
            }

            objectsDictionary.Add(item.objectID, objectsPool);
        }
    }

    //For putting objects active
    public GameObject DeQueuePoolObject(string id, Vector2 position)
    {
        //Check list for the id match
        if (!objectsDictionary.ContainsKey(id))
        {
            return null;
        }

        GameObject poolObject = objectsDictionary[id].Dequeue();

        //Set position
        poolObject.transform.position = position;

        poolObject.SetActive(true);

        objectsDictionary[id].Enqueue(poolObject);
       
        return poolObject;
    }

    //For putting objects inactive
    public void EnQueuePoolObject(GameObject poolObject)
    {
        //Set object inactive
        poolObject.SetActive(false);
    }
}