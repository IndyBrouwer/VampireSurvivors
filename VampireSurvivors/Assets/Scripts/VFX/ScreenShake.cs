using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public IEnumerator CamShake()
    {
        Vector3 startPosition = transform.position;

        float shakeAmount = 0.15f;
        float duration = 0.5f;

        float passedTime = 0f;

        while (passedTime < duration)
        {
            passedTime += Time.deltaTime;

            float offsetX = Random.Range(-shakeAmount, shakeAmount);
            float offsetY = Random.Range(-shakeAmount, shakeAmount);

            transform.position = startPosition + new Vector3(offsetX, offsetY, 0);

            yield return null;
        }

        transform.position = startPosition;
    }
}