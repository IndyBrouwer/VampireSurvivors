using UnityEngine;

public class XPCollectable : MonoBehaviour, ICollectable
{
    [SerializeField] private DropData dropData;
    private float heldAmount;

    public void Initialize(float droppedAmount)
    {
        heldAmount = droppedAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collector"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        //Add xp amount to player


        PoolManager.Instance.EnQueuePoolObject(gameObject);
    }
}
