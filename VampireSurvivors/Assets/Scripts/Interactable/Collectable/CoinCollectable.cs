using UnityEngine;

public class CoinCollectable : MonoBehaviour, ICollectable
{
    [SerializeField] private DropData dropData;
    private float heldAmount;

    [SerializeField] private AudioClip sfx;

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
        AudioManager.Instance.sfxManager.PlaySFX(sfx);
        
        //Add amount of coins to player wallet
        collector.GetComponentInParent<PlayerWallet>().AddCoins(heldAmount);

        PoolManager.Instance.EnQueuePoolObject(gameObject);
    }
}