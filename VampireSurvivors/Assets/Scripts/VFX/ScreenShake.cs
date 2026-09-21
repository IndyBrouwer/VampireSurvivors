using Cinemachine;
using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private float impulseForce = 1f;

    public void CamShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(impulseForce);
    }
}