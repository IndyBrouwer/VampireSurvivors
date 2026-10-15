using UnityEngine;

public class CoinCollectable : MonoBehaviour, ICollectable
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
        //Add amount of coins to player wallet
        collector.GetComponentInParent<PlayerWallet>().AddCoins(heldAmount);

        PoolManager.Instance.EnQueuePoolObject(gameObject);
    }
}