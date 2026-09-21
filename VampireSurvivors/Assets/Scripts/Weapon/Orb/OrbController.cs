using UnityEngine;

public class OrbController : MonoBehaviour, ISpawnable, IDespawnable
{
    /// <summary>
    /// This script is used to trigger the orb spawning and despawning. The orb can only spawn if the player actually has the weapon in their inventory.
    /// </summary>

    [SerializeField] private GameObject orbWeapon;

    public void Spawn()
    {
        //Spawn the orb
        orbWeapon.SetActive(true);
    }

    public void Despawn()
    {
        //Despawn the orb
        orbWeapon.SetActive(false);
    }
}