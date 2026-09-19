using System.Collections;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    public float damageDuration = 0.2f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public void DamageColorEffect(SpriteRenderer entityRenderer)
    {
        spriteRenderer = entityRenderer;
        originalColor = spriteRenderer.color;

        StartCoroutine(RedFlickerDuration());
    }

    private IEnumerator RedFlickerDuration()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(damageDuration);

        spriteRenderer.color = originalColor;
    }
}