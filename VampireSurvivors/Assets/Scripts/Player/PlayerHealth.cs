using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable, IRegenerable
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    private bool isDead = false;

    [Header("Damage Settings")]
    [SerializeField] private float damageCooldown = 0.2f;
    private bool canTakeDamage = true;

    [Header("UI")]
    [SerializeField] private Slider healthSlider;

    [Header("Required Components")]
    private Animator animator;
    private CinemachineImpulseSource impulseSource;

    [Header("Required Scripts")]
    [SerializeField] private GameOverUI gameOverUIScript;
    [SerializeField] private ScreenShake screenShakeScript;
    private DamageEffect damageEffectScript;

    private void Start()
    {
        animator = GetComponent<Animator>();
        damageEffectScript = GetComponent<DamageEffect>();
        impulseSource = GetComponent<CinemachineImpulseSource>();

        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
    }

    private void UpdateUI()
    {
        healthSlider.value = currentHealth;
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;

        //Ensure that currentHealth does not exceed maxHealth
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //Update UI
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage)
        {
            return;
        }
        
        canTakeDamage = false;

        //Damage vfx
        damageEffectScript.DamageColorEffect(GetComponent<SpriteRenderer>());

        //Apply damage
        currentHealth -= damage;
        
        //Ensure that currentHealth does not go below 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //Update UI
        UpdateUI();

        if (currentHealth <= 0)
        {
            if (isDead)
            {
                return;
            }

            isDead = true;

            //Disable slider fill
            healthSlider.fillRect.gameObject.SetActive(false);

            Die();
        }

        //Start cooldown coroutine
        StartCoroutine(WaitDamageCooldown());
    }

    public void Die()
    {
        Debug.Log("Player has died.");

        //Trigger death animation
        animator.SetTrigger("Die");

        //Wait for player to lie flat?!

        //Shake screen effect
        screenShakeScript.CamShake(impulseSource);

        //Trigger game over screen (UI)
        gameOverUIScript.ShowGameOverUI();
    }

    private IEnumerator WaitDamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldown);

        canTakeDamage = true;
    }
}