using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    [SerializeField] private Slider healthSlider;

    [Header("Components")]
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
    }

    private void UpdateUI()
    {
        healthSlider.value = currentHealth;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        //Ensure that currentHealth does not exceed maxHealth
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //Update UI
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        //Ensure that currentHealth does not go below 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //Update UI
        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player has died.");
    }
}