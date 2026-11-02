using UnityEngine;

public class DespawnDrop : MonoBehaviour
{
    [SerializeField] private float despawnTime = 20f;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= despawnTime)
        {
            PoolManager.Instance.EnQueuePoolObject(gameObject);

            timer = 0f;
        }
    }
}
