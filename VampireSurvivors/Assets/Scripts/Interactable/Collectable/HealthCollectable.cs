using UnityEngine;

public class HealthCollectable : MonoBehaviour, ICollectable
{
    [SerializeField] private DropData dropData;
    private float healAmount;

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
        //Add health to player
        collector.GetComponentInParent<PlayerHealth>().Heal(healAmount);

        Destroy(gameObject);
    }
}