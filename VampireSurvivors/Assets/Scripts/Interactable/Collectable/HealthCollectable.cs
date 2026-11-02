using UnityEngine;

public class HealthCollectable : MonoBehaviour, ICollectable
{
    [SerializeField] private DropData dropData;
    private float healAmount;

    [SerializeField] private AudioClip[] sfx;

    public void Initialize(float droppedAmount)
    {
        healAmount = droppedAmount;
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
        PickSFX();

        //Add health to player
        collector.GetComponentInParent<PlayerHealth>().Heal(healAmount);

        PoolManager.Instance.EnQueuePoolObject(gameObject);
    }

    private void PickSFX()
    {
        int randomPick = Random.Range(0, sfx.Length);

        AudioManager.Instance.sfxManager.PlaySFX(sfx[randomPick]);
    }
}