using UnityEngine;

public class EnemySpawnPosition : MonoBehaviour
{
    //Create a circle around the player

    //Set circle size so big that the circle is completely out of camera sight

    //The very outer line of this circle, will be where the enemies are being spawned

    //All enemies spawn somewhere on this circle, depending on the enemy there might be differences

    //Tombstone enemies make a full circle together, slowly closing in on the player to trap them

    //Regular enemies just spawn randomly somewhere on the line

    //Grouping enemies spawn randomly on the circle all huddled together

    [SerializeField] private Camera mainCamera;

    [Header("Spawning Settings")]
    [SerializeField] private float distance = 22.4f;

    public Vector2 GetSpawnPosition(Vector2 center)
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);

        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        return center + direction * distance;
    }
}