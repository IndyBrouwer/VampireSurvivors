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
            Collect(collision.gameObject);
        }
    }

    public void Collect(GameObject collector)
    {
        //Add xp amount to player
        collector.GetComponentInParent<PlayerLeveling>().AddExperience(heldAmount);

        PoolManager.Instance.EnQueuePoolObject(gameObject);
    }
}