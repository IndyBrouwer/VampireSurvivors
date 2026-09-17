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

    [Header("Other Scripts")]
    [SerializeField] private GameOverUI gameOverUIScript;
    [SerializeField] private DamageEffect damageEffectScript;
    [SerializeField] private ScreenShake screenShakeScript;

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
        //Damage vfx
        damageEffectScript.DamageColorEffect(GetComponent<SpriteRenderer>());

        currentHealth -= damage;
        
        //Ensure that currentHealth does not go below 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //Update UI
        UpdateUI();

        if (currentHealth <= 0)
        {
            //Disable slider fill
            healthSlider.fillRect.gameObject.SetActive(false);

            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player has died.");

        //Shake screen effect
        screenShakeScript.CamShake();

        //Trigger death animation
        animator.SetTrigger("Die");

        //Trigger game over screen (UI)
        gameOverUIScript.ShowGameOverUI();
    }
}